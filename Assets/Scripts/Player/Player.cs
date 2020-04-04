using SDG.Unity.Scripts;
using System.Collections;
using System.Collections.Generic;
using SDG.Platform.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

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
    [Header("Dash Configuration")]
    [SerializeField] private float _dashCastTime;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashCooldown;
    [SerializeField] private float _dashForce;
    float _lastDash;
    bool _isDashing;
    private float attackSpeed = 0.5f;
    private float lastAttack = 0.0f;
    bool _isGrounded = true;
    bool _isOnSlope = false;
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
        Sword.OnSoulUpdateEvent += AddSoulStats;
        LoadAllStats();
        binds = SaveSystem.LoadData<KeyBindData>(SaveSystem.Data.Inputs);
        if (binds == null)
        {
            binds = new KeyBindData();
        }
    }

    private void AddSoulStats(Soul soul)
    {
        stats += soul.bonusStats;
        LoadAllStats();
        UpdateStatsUI();
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
        sword.stats.Power = stats.Power;
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = 0, vertical = 0;

        _isOnSlope = OnSlope();

        if (_isOnSlope)
        {
            motor.Move(Vector3.down * 15 * Time.deltaTime);
        }
        if (_isGrounded || _isOnSlope && _canMove)
        {
            horizontal += Input.GetKey(binds.moveLeft) ? -1 : 0;
            horizontal += Input.GetKey(binds.moveRight) ? 1 : 0;
            vertical += Input.GetKey(binds.moveForward) ? 1 : 0;
            vertical += Input.GetKey(binds.moveBackward) ? -1 : 0;

            motor.Move(new Vector3(horizontal, 0, vertical));
        }
        
        motor.LookAtMouse();

        if (Input.GetKeyDown(binds.attack) && lastAttack + attackSpeed <= Time.time)
        {
            lastAttack = Time.time;
            Debug.Log("Lance lattack");
            animator.SetTrigger("sword hit");
        }

        if (Input.GetKeyDown(binds.interact) && _chest != null)
        {
            InteractWithChest();
        }

        if (Input.GetKeyDown(KeyCode.Space) && _lastDash + _dashCooldown <= Time.time && _canMove && _isGrounded)
        {
            _lastDash = Time.time;
            StartCoroutine(Dash(new Vector3(horizontal, 0, vertical)));
        }
    }
    private IEnumerator Dash(Vector3 direction)
    {
        Debug.Log("Dash");
        _isDashing = true;
        var rb = GetComponent<Rigidbody>();
        _canMove = false;
        yield return new WaitForSeconds(_dashCastTime);
        rb.AddForce(direction * _dashForce * 100);
        yield return new WaitForSeconds(_dashDuration);
        rb.velocity = Vector3.zero;
        _canMove = true;
        _isDashing = true;
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
        if (_isDashing) return;
        stats.TakeDamage(amount);
        if(stats.Health <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("On est mort!");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
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
