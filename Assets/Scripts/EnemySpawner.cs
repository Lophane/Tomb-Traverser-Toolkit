using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [Header("Prefabs")]
    public Transform player;
    public GameObject enemyPrefab;

    [Header("Spawner stats")]
    public float spawnInterval = 10f;
    private float spawnTimer = 10f;
    public bool spawned = false;
    public float countdownClock = 0f;

    private void Start()
    {
        player = GameObject.Find("PlayerCharacter").transform;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) >= 30f && spawnTimer <= 0f)
        {
            spawnTimer = spawnInterval;
            SpawnEnemy();            
        }
        else if (spawnTimer >= 0f && spawned == false)
        {
            spawnTimer -= Time.deltaTime;
        }
        //Debug.Log(spawned);

        if (spawnInterval >= 6f)
        {
            countdownClock += Time.deltaTime;

            if(countdownClock >= 60f)
            {
                spawnInterval -= 1f;
                countdownClock = 0f;
                //Debug.Log(spawnInterval);
            }

        }

    }

    public void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        EnemyMovement enemyScript = newEnemy.GetComponent<EnemyMovement>();
        //enemyScript.playerTarget = player;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Enemy"){
            spawned = true;
            //Debug.Log("You're in my space");
        }                  
                    
    }
    private void OnTriggerExit(Collider other)
    {
        spawned = false;
    }
}
