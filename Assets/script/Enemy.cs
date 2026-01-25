using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public GameObject gizmosContainer;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;

    [Header("Damage")]
    public Transform attackPoint;
    public LayerMask playerLayer;
    public float attackRange = 0.5f;
    public int attackDamage = 1;
    public float attackRate = 2f;
    public float nextAttackTime = 0f;
    public int maxHealth = 12;
    int currentHealth;
    public Player playerMovment;

    [Header("Knockback")]
    public float EnemyKBForce;
    public float EnemyKBCounter;
    public float EnemyKBTotalTime;
    public bool EnemyKnockFromRight;
    private Vector3 lastDamageSource;
    private float lastDamageTime = -1f;
    public float damageCooldown = 0.5f;
    private float gizmosYOffset = 0f;
    
    [Header("Fall Speed")]
    public float fallMultiplier = 10f;

 void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isRunning", true);
        currentHealth = maxHealth;
        EnemyKBCounter = 0;
       
        if(gizmosContainer != null)
        {
            gizmosYOffset = gizmosContainer.transform.position.y - transform.position.y;
        }
    }   

    void Update()
    {
        
        if(gizmosContainer != null)
        {
            Vector3 gizmosPos = gizmosContainer.transform.position;
            gizmosPos.y = transform.position.y + gizmosYOffset;
            gizmosContainer.transform.position = gizmosPos;
        }
        
        Vector2 point = currentPoint.position - transform.position;
        
        if(EnemyKBCounter <= 0)
        {
            if(currentPoint == pointB.transform)
            {
                rb.linearVelocity = new Vector2(speed, 0);
            }else
            {
                rb.linearVelocity = new Vector2(-speed, 0);
            }
        }
        else
        {
            if(EnemyKnockFromRight){
                rb.linearVelocity = new Vector2(-EnemyKBForce, rb.linearVelocity.y);
            }
            if(!EnemyKnockFromRight){
                rb.linearVelocity = new Vector2(EnemyKBForce, rb.linearVelocity.y);
            }
            EnemyKBCounter -= Time.deltaTime;
        }

        if(Vector2.Distance(transform.position, currentPoint.position) <0.5f && currentPoint == pointB.transform)
        {
            flip();
            currentPoint = pointA.transform;
        }
        if(Vector2.Distance(transform.position, currentPoint.position) <0.5f && currentPoint == pointA.transform)
        {
            flip();
            currentPoint = pointB.transform;
        }

       
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
        }

    }

    public void TakeDamage(int damage, Vector3 damageSource)
    {
        if (Time.time - lastDamageTime < damageCooldown)
            return;
            
        lastDamageTime = Time.time;
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
        lastDamageSource = damageSource;
        KB();
    }

    void Die()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Animator>().enabled = false;
        this.enabled = false;
    }
    

private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    void KB()
    {
        EnemyKBCounter = EnemyKBTotalTime;

        if (transform.position.x <= lastDamageSource.x)
        {
            EnemyKnockFromRight = true;
        }
        if(transform.position.x > lastDamageSource.x)
        {
            EnemyKnockFromRight = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       
        if(Time.time >= nextAttackTime){
            other.GetComponent<Player>().TakeDamage_Player(attackDamage, transform.position);
            nextAttackTime = Time.time + 1f / attackRate;
        }

    }
}