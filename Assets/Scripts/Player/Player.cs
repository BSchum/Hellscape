using SDG.Unity.Scripts;
using System.Collections;
using System.Collections.Generic;
using SDG.Platform.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Motor))]
public class Player : MonoBehaviour, IDamagable
{
    [SerializeField]
    private float life = 30;

    private Animator animator;
    private Motor motor;
    [SerializeField]
    private Sword sword;
    public Sword Sword { get { return sword; } private set { sword = value; } }

    public Bag Bag { get; private set; } = new Bag();
    private Chest _chest;
    public Stats stats;

    bool _isGrounded = true;
    bool _isOnSlope = false;

    public delegate void OnStatUpdate(Stats stats);
    public event OnStatUpdate OnStatUpdateEvent;

    private void Awake()
    {
        //PlayerContext.instance.player = this.gameObject;
        animator = this.GetComponentInChildren<Animator>();
        motor = this.GetComponent<Motor>();
        Bag.OnAddItemEvent += Use;
        LoadAllStats();
    }

    void Use(Item item)
    {
        stats += item.bonusStats;
        LoadAllStats();
        UpdateStatsUI();
    }
    void LoadAllStats()
    {
        motor.speed = stats.Speed;
        Sword.Power = stats.Power;
    }
    void Update()
    {
        _isOnSlope = OnSlope();

        if (_isOnSlope)
        {
            motor.Move(Vector3.down * 15 * Time.deltaTime);
        }
        if (_isGrounded || _isOnSlope)
        {
            motor.Move(new Vector3(Input.GetAxisRaw(Constants.Inputs.PLAYER_HORIZONTAL), 0, Input.GetAxisRaw(Constants.Inputs.PLAYER_VERTICAL)));
        }
        
        motor.LookAtMouse();

        if (Input.GetButtonDown(Constants.Inputs.PLAYER_HIT) && Sword.AttackEnabled)
        {
            Sword.Attack();
        }

        if (Input.GetButtonDown(Constants.Inputs.PLAYER_INTERACT) && _chest != null)
        {
            InteractWithChest();
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
        stats.TakeDamage(amount);
        if(stats.Health <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("On est mort!");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        }
        OnStatUpdateEvent(stats);
    }

    #region Triggers
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.Tags.CHEST_TAG)
        {
            _chest = other.GetComponent<Chest>();
        }        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == Constants.Tags.CHEST_TAG)
        {
            _chest = null;
        }
    }
    #endregion
}
