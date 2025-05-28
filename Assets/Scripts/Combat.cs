using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Animator animator;
    private Enemy enemy;
    // An AudioSource is attached to a GameObject for playing back sounds in a 3D environment.
    public AudioSource audioSource;

    private void Start()
    {
        enemy = LevelController.enemy;
    }

    void Update()
    {
        enemy = LevelController.enemy;
        Attack();
    }

    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            // If audio is not playing otherwise overlaps
            // After sound play, animator will be executed
            if (!audioSource.isPlaying)  audioSource.Play();
            animator.SetTrigger("isAttacking");
        }
    }

    private void OnTriggerEnter(Collider other)
    {  
            enemy.TakeDamage(20);
    }
}
