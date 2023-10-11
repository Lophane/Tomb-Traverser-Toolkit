using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BasicEnemy : MonoBehaviour
{
    [Header("Stats")]
    public int health;

    [Header("References")]
    public EnemyAI enemyAI;
    public Transform player;

    private NavMeshAgent agent;


    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerCharacter").transform;

        if (this.tag == "Boss")
        {
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(player.position, path))
            {
                // Check if the path is complete and has no obstacles
                if (path.status == NavMeshPathStatus.PathComplete)
                {
                    Debug.Log("Entity has a clear path to the player.");
                    // No obstacles detected, path is clear
                }
                else
                {
                    Debug.Log("Obstacle detected between entity and player.");
                    // There is an obstacle in the way or the path is incomplete
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0 && this.tag == "Enemy")
        {
            Invoke("Death", 2f);
            enemyAI.dead = true;
        }

        else if (health <= 0 && this.tag != "Boss")
        {
            Death();
        }
            
        else if (health <= 0 && this.tag == "Boss")
        {
            enemyAI.dead = true;
            Invoke("EndGame", 2f);
        }

    }

    void Death()
    {
        Destroy(gameObject);
    }

    void EndGame()
    {
        GameManager.BossDefeted = true;
        GameManager.GameActive = false;
        Death();
    }

}
