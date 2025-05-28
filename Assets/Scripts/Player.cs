using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour

{
    public GameObject nextLevel;
    public GameObject gameOverText;
    public CharacterController controller;

    public Transform cam;
    Animator animator;
    public AudioSource audioSource;


    public float rollSpeed;
    public float rollTime;

    public float speed = 6;
    public bool isRolling = false;

    public Vector3 moveDir;



    float turnSmoothVelocity;

    public float turnSmoothTime = 0.1f;

    //Healthbar
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    private Enemy enemy;


    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Start()
    {
        enemy = LevelController.enemy;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()

    {
        enemy = LevelController.enemy;
        Debug.Log(enemy.name);
        //Walking inputs.

        float horizontal = Input.GetAxisRaw("Horizontal");

        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        //Animations

        if (Input.GetKeyDown(KeyCode.LeftShift))

        {
            if (isRolling == false)
            {
                isRolling = true;
                animator.SetTrigger("isRolling");
                StartCoroutine(Roll());

            }



        }


        if (horizontal != 0 || vertical != 0)
        {
            // Avoid overlapping sound
            if (!audioSource.isPlaying) audioSource.Play();
            animator.SetBool("isWalking", true);
        }

        else

        {

            animator.SetBool("isWalking", false);

        }
        // If our characters direction changes we need to change to axis. we are usnig atan2 function to make this happen.
        if (direction.magnitude >= 0.1f)

        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);



            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);

        }

        if (currentHealth <= 0)
        {
            Die();
        }
        if (enemy.currentHealth <= 0)
        {
            Cheer();
            if (enemy.name.Equals("Enemy 3(Clone)"))
            {
                nextLevel.SetActive(false);
                gameOverText.SetActive(true);
            }  


        }
    }


    IEnumerator Roll()
    {
        float startTime = Time.time;
        while (Time.time < startTime + rollTime)
        {
            controller.Move(moveDir * rollSpeed * Time.deltaTime);
            yield return null;

        }

        yield return new WaitForSeconds(.5f);
        isRolling = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void Die()
    {


        animator.SetBool("isDied", true);
        GetComponent<Player>().enabled = false;
        GameObject.Find("Spear2H_Basic").SetActive(false);


    }
    private void Cheer()
    {
        animator.SetBool("Win", true);
        GameObject.Find("Player/Armature/Root_M/Spine1_M/Spine2_M/Chest_M/Scapula_R/Shoulder_R/Elbow_R/Wrist_R/jointItemR").SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        nextLevel.SetActive(true);

    }







}