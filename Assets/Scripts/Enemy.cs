using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public GameObject enemyWeapon;
    public int currentHealth;
    public float attackCooldown = 1;
    public HealthBar healthBar;
    public NavMeshAgent agent;
    private Transform player;
    Animator animator;
     // Audio source for weapon sound on attack
    public AudioSource weaponSound;
    // Audio source for walking at chase
     public AudioSource walkingSound;
    public int damage;


    private Player mc;

    //Attack
    bool alreadyAttacked = false;

    public LayerMask Target;
    // Start is called before the first frame update

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
 
    void Start()
    {
        initEnemy();
    }

    public void initEnemy()
    {
        healthBar = GameObject.Find("AI Healthbar").GetComponent<HealthBar>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        mc = GameObject.Find("Player").GetComponent<Player>();

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Target);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Target);


        if (!playerInSightRange && !playerInAttackRange)
        {
            animator.SetBool("isWalking", false);
            agent.SetDestination(this.transform.position);


        }

        if (playerInSightRange && !playerInAttackRange)
        {
            Chase();

        }
        if (playerInSightRange && playerInAttackRange)
        {
            Attack();
        }
        if (currentHealth <= 0)
        {
            Die();
        }
        if (mc.currentHealth <= 0)
        {
            Cheer();
        }


    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void Chase()
    {
        // Avoid overlapping of sound
        if (!walkingSound.isPlaying) walkingSound.Play();
        animator.SetBool("isWalking", true);
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        if (!alreadyAttacked)
        {
            animator.SetBool("isWalking", false);
            agent.SetDestination(transform.position);
            transform.LookAt(player);
            // In atack, weapon sound will play
            weaponSound.Play();
            animator.SetTrigger("isEnemyAttacking");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);

        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void Die()
    {
      
            animator.SetBool("isDied", true);
            GetComponent<Enemy>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
            enemyWeapon.SetActive(false);
    }
    private void Cheer()
    {
        alreadyAttacked = true;
        enemyWeapon.SetActive(false);
        animator.SetBool("Win", true);

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.name =="Player" )
        {
            mc.TakeDamage(damage);
        }

    }


}
