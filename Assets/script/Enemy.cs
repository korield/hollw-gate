using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
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

 void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isRunning", true);
        currentHealth = maxHealth;
    }   

    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, 0);
        }else
        {
            rb.linearVelocity = new Vector2(-speed, 0);
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

    }

    public void TakeDamage(int damage, Vector2 damageSource)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if(Time.time >= nextAttackTime){
            other.GetComponent<Player>().TakeDamage_Player(attackDamage);
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
}