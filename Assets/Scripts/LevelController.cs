using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public GameObject[] enemyArray;
    public int currentLevel = 1;
    public static Enemy enemy;
    public GameObject nextLevel;
    public Animator animator;
    public Player player;

    void Awake()
    {

        Instantiate(enemyArray[0]);
        if (GameObject.Find("Enemy(Clone)") != null)
            enemy = GameObject.Find("Enemy(Clone)").GetComponent<Enemy>();
        if (GameObject.Find("Enemy 2(Clone)") != null)
            enemy = GameObject.Find("Enemy 2(Clone)").GetComponent<Enemy>();
        if (GameObject.Find("Enemy 3(Clone)") != null)
            enemy = GameObject.Find("Enemy 3(Clone)").GetComponent<Enemy>();
        currentLevel++;

    }

    public void NextLevel()
    {
        Destroy(enemy.gameObject);
        if (currentLevel == 2)
            Instantiate(enemyArray[1]);
        if (currentLevel == 3)
            Instantiate(enemyArray[2]);
        nextLevel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        player.transform.position = new Vector3(0, 0, 0);
        player.currentHealth = player.maxHealth;
        player.healthBar.SetHealth(player.maxHealth);
        animator.SetBool("Win", false);
        GameObject.Find("Player/Armature/Root_M/Spine1_M/Spine2_M/Chest_M/Scapula_R/Shoulder_R/Elbow_R/Wrist_R/jointItemR").SetActive(true);
        if (GameObject.Find("Enemy(Clone)") != null)
            enemy = GameObject.Find("Enemy(Clone)").GetComponent<Enemy>();
        if (GameObject.Find("Enemy 2(Clone)") != null)
        {
            enemy = GameObject.Find("Enemy 2(Clone)").GetComponent<Enemy>();
            enemy.initEnemy();
        }
        if (GameObject.Find("Enemy 3(Clone)") != null)
        {
            enemy = GameObject.Find("Enemy 3(Clone)").GetComponent<Enemy>();
            enemy.initEnemy();
        }

        currentLevel++;

    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
