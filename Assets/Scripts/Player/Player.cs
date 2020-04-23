using SDG.Unity.Scripts;
using System.Collections;
using System.Collections.Generic;
using SDG.Platform.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Motor))]
public class Player : MonoBehaviour, IDamagable
{
    private Animator animator;
    private Motor motor;
    [SerializeField]
    Sword sword;
    public Sword Sword { get { return sword; } }

    public Bag Bag { get; private set; } = new Bag();
    public Stats stats;
    public PlayerContext playerContext;
    public LayerMask moveLayerMask;
    [SerializeField] private float _invicibilityDuration;
    private float _lastInvicibility = 0.0f;
    float _lastDash;
    bool _isDashing;
    private float attackSpeed = 0.5f;
    private float lastAttack = 0.0f;
    bool _isGrounded = true;
    bool _canMove = true;
    private Chest _chest;
    

    private KeyBindData binds;

    public delegate void OnStatUpdate(Stats stats);
    public event OnStatUpdate OnStatUpdateEvent;

    private void Awake()
    {
        playerContext.player = this.gameObject;
        animator = this.GetComponentInChildren<Animator>();
        motor = this.GetComponent<Motor>();
        Bag.OnAddItemEvent += Use;
        foreach (var talent in playerContext.playerData.activeTalents)
        {
            stats += talent.stats;
        }
        LoadAllStats();
        binds = SaveSystem.LoadData<KeyBindData>(SaveSystem.Data.Inputs);
        if (binds == null)
        {
            binds = new KeyBindData();
        }
    }

    void Use(Item item)
    {
        stats += item.bonusStats;
        UpdateStatsUI();
    }
    void LoadAllStats()
    {
        motor.speed = stats.Speed;
        sword.power = stats.Power;
    }
    // Update is called once per frame
    void Update()
    {
        motor.LookAtMouse();
        if (Input.GetKeyDown(binds.attack) && lastAttack + attackSpeed <= Time.time)
        {
            lastAttack = Time.time;
            animator.SetTrigger("sword hit");
        }

        if (Input.GetKeyDown(binds.interact) && _chest != null)
        {
            InteractWithChest();
        }
    }
    void FixedUpdate()
    {
        Physics.Raycast(this.transform.position + Vector3.up, -Vector3.up, out RaycastHit raycastHit, 2, moveLayerMask);
     
        if(raycastHit.transform != null)
            _isGrounded = raycastHit.transform.tag == Constants.Tags.FLOOR_TAG || raycastHit.collider.tag == Constants.Tags.BRIDGE_TAG;
        else
            _isGrounded = false;
        float horizontal = 0, vertical = 0;
        horizontal += Input.GetKey(binds.moveLeft) ? -1 : 0;
        horizontal += Input.GetKey(binds.moveRight) ? 1 : 0;
        vertical += Input.GetKey(binds.moveForward) ? 1 : 0;
        vertical += Input.GetKey(binds.moveBackward) ? -1 : 0;

        animator.SetFloat("MoveX", horizontal);
        animator.SetFloat("MoveY", vertical);

        if (_isGrounded && _canMove)
        {
            motor.Move(new Vector3(horizontal, 0, vertical));
        }
    }

    public void UpdateStatsUI()
    {
        OnStatUpdateEvent(stats);
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 3))
        {
            if (hit.normal != Vector3.up)
            {
                return true;
            }
        }
        return false;
    }
    private void InteractWithChest()
    {
        if (_chest.IsOpened)
        {
            if (!_chest.IsLooted)
            {
                Item item = _chest.GetItem();
                Bag.AddItem(item);
            }
        }
        else
        {
            _chest.Open();
        }
    }

    public void TakeDamage(uint amount)
    {
        if(_lastInvicibility + _invicibilityDuration <= Time.time)
        {
            _lastInvicibility = Time.time;
            StartCoroutine(Blink());
            stats.TakeDamage(amount);
            if (stats.Health <= 0)
            {
                _canMove = false;
                animator.SetTrigger("Die");
                Destroy(gameObject, 2f);
            }
            OnStatUpdateEvent(stats);

        }

    }

    private IEnumerator Blink()
    {
        while(_lastInvicibility + _invicibilityDuration >= Time.time)
        {
            foreach (SkinnedMeshRenderer renderer in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                renderer.enabled = !renderer.enabled;
            }

            yield return new WaitForSeconds(0.02f);
        }

        foreach (SkinnedMeshRenderer renderer in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            renderer.enabled = true;
        }
    }

    public void ActivateSwordCollider()
    {
        sword.GetComponent<BoxCollider>().enabled = true;
    }

    public void DesactivateSwordCollider()
    {
        sword.GetComponent<BoxCollider>().enabled = false;
    }

    #region Triggers
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.Tags.CHEST_TAG)
        {
            _chest = other.GetComponent<Chest>();
            _chest.ToggleUI(true);
        }        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == Constants.Tags.CHEST_TAG)
        {
            _chest.ToggleUI(false);
            _chest = null;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == Constants.Tags.FLOOR_TAG || collision.gameObject.tag == Constants.Tags.BRIDGE_TAG)
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == Constants.Tags.FLOOR_TAG || collision.gameObject.tag == Constants.Tags.BRIDGE_TAG)
        {
            _isGrounded = false;
        }
    }
    #endregion
}
