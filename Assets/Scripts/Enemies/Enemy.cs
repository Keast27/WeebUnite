using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //base properties
    [SerializeField] protected float health;
    public float maxHealth;
    protected Animator animator;
    protected Rigidbody2D rigidBody;
    protected SpriteRenderer spriteRenderer;

    protected Color32 onHitColor = Color.red;
    protected float hitColorDuration = 0.07f;

    [SerializeField] protected bool canMove = true;

    // Is this entity facing right by default
    [SerializeField] protected bool currFacingRight = true;

    public Action onDeath;

    protected enum State
    {
        Passive, // Idle/Walking around
        Aggro, // Moving towards target
        Attacking // Attacking target
    }

    private Vector3 spawnLocal;
    [Header("Base Enemy Properties")]
    public GameObject target;
    [SerializeField] protected State currentState;

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float previousX;

    [SerializeField] protected bool faceRight = false;
    [SerializeField] protected bool attackAble = true;

    // Movement() is a custom property that can be added to any object
    // Used for passive movement
    //[Movement(), SerializeField] protected Movement.Type moveType;

    public bool CanMove
    {
        get => canMove;
        set
        {
            canMove = value;

            rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

            if (!canMove)
            {
                rigidBody.velocity = Vector2.zero;
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    // Use this when adjusting health
    // Will kill entity when health is zero
    public float Health
    {
        get { return health; }
        set
        {
            health = value;

            if (health <= 0)
                Die();
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = maxHealth;

        spawnLocal = transform.position;
        previousX = transform.position.x;
    }
    //protected virtual void Move() { }
    protected virtual void Attack() { }
    public virtual void Damage(float damageAmt)
    {
        Health -= damageAmt;

        //Debug.Log("DAMAGE DONE TO: "  + gameObject.name);

        StopCoroutine(FlashDamageColor());
        StartCoroutine(FlashDamageColor());
    }

    public virtual void Die() { onDeath?.Invoke(); }

    public void Hit(float damageAmt, Vector3 attackerPosition)
    {
        Damage(damageAmt);

    }

    public virtual void Flip()
    {
        float currentX = transform.position.x;
        bool flip = false;

        float vDirection = currentX - previousX;

        if (vDirection < 0) //If the enemy is going left
        {
            if (currFacingRight)
                flip = true;
        }
        else if (vDirection > 0)//If the enemy is going right
        {
            if (!currFacingRight)
                flip = true;
        }

        if (flip)
            Flip();

        previousX = transform.position.x;
    }

    private IEnumerator FlashDamageColor()
    {
        Color32 originalColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = onHitColor;
        yield return new WaitForSeconds(hitColorDuration);
        GetComponent<SpriteRenderer>().color = originalColor;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Move();
        Flip();
    }

    protected bool InAttackRange()
    {
        if (!target)
            return false;

        Vector2 distance = target.transform.position - transform.position;

        return distance.magnitude <= attackRange;
    }


}
