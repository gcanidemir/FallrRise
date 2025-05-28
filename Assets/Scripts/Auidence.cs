using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auidence : MonoBehaviour
{
    Animator animator;
    private Enemy enemy;
    public Player player;
    private bool flag = false;
    // An AudioSource is attached to a GameObject for playing back sounds in a 3D environment
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GameObject.Find("Audience").GetComponent<AudioSource>();
        enemy = LevelController.enemy;
    }

    // Update is called once per frame
    void Update()
    {
        enemy = LevelController.enemy;
        if (player.currentHealth <= 0 || enemy.currentHealth <=0)
        {
            if (!flag)
                audioSource.Play();
            flag = true;
            animator.SetBool("Cheering", true);
        }
        else
        {
            audioSource.Stop();
            flag = false;
            animator.SetBool("Cheering", false);
        }
        
    }
}
