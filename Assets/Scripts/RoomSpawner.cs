using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 --> need Bottom door
    // 2 --> need Top door
    // 3 --> need Right door
    // 4 --> need Left door

    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;

    public float waitTime = 10f;

    private void Start()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", .2f);
        Invoke("Reset", 2f);
        Debug.Log(templates.maxSizeX);
        Debug.Log(templates.maxSizeZ);
        Debug.Log(templates.maxRooms);
    }

    /*void Spawn()
    {

        if (spawned == false)
        {

            if (transform.position.x >= 140f || transform.position.x <= -140f || transform.position.z >= 140f || transform.position.z <= -140f || templates.rooms.Count >= 100f)
            {
                Instantiate(templates.closedRooms, transform.position, Quaternion.identity);
                spawned = true;
            }
            else
            {
                if (openingDirection == 1)
                {
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                }
                else if (openingDirection == 2)
                {
                    rand = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                }
                else if (openingDirection == 3)
                {
                    rand = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);

                }
                else if (openingDirection == 4)
                {
                    rand = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                }
                else if (openingDirection == 5)
                {
                    rand = Random.Range(0, templates.splitRooms.Length);
                    Instantiate(templates.splitRooms[rand], transform.position, templates.splitRooms[rand].transform.rotation);
                }

                spawned = true;

            }

        }
        
    }*/

    void Spawn()
    {
        if (spawned)
            Debug.Log("I'm Pooped");
            return;

        if (transform.position.x >= templates.maxSizeX || transform.position.x <= -templates.maxSizeX || transform.position.z >= templates.maxSizeZ || transform.position.z <= -templates.maxSizeZ || templates.rooms.Count >= templates.maxRooms)
        {
            Instantiate(templates.closedRooms, transform.position, Quaternion.identity);
            spawned = true;
        }
        else
        {
            List<GameObject> availableRooms = new List<GameObject>();

            if (openingDirection == 1)
                availableRooms.AddRange(templates.bottomRooms);
            else if (openingDirection == 2)
                availableRooms.AddRange(templates.topRooms);
            else if (openingDirection == 3)
                availableRooms.AddRange(templates.rightRooms);
            else if (openingDirection == 4)
                availableRooms.AddRange(templates.leftRooms);
            else if (openingDirection == 5)
                availableRooms.AddRange(templates.splitRooms);

            GameObject roomToSpawn = availableRooms[Random.Range(0, availableRooms.Count)];
            Instantiate(roomToSpawn, transform.position, roomToSpawn.transform.rotation);

            spawned = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("This town ain't big enought for the both of us");
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedRooms, transform.position, Quaternion.identity);
                Debug.Log("This town ain't big enough for the both of us!");
            }
            spawned = true;
        }
    }

    private void Reset()
    {
        if (templates.rooms.Count <= 25f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
