using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject[] splitRooms;

    public GameObject closedRooms;

    public List<GameObject> rooms;
    public List<GameObject> actionRooms;

    public float waitTime;
    private bool spawnedBoss = false;
    private bool spawnedSpawner1 = false;
    private bool spawnedSpawner2 = false;
    private bool spawnedSpawner3 = false;
    public GameObject boss;
    public GameObject enemySpawner;

    private void Update()
    {
        if(waitTime <= 0 && spawnedBoss == false)
        {
            Instantiate(boss, actionRooms[actionRooms.Count - 1].transform.position, Quaternion.identity);
            spawnedBoss = true;
        }
        else if (waitTime >= 0)
        {
            waitTime -= Time.deltaTime;
        }

        if (waitTime <= 0 && spawnedSpawner1 == false)
        {
            Instantiate(enemySpawner, actionRooms[actionRooms.Count - 2].transform.position, Quaternion.identity);
            spawnedSpawner1 = true;
        }
        if (waitTime <= 0 && spawnedSpawner2 == false)
        {
            Instantiate(enemySpawner, actionRooms[actionRooms.Count / 3].transform.position, Quaternion.identity);
            spawnedSpawner2 = true;
        }
        if (waitTime <= 0 && spawnedSpawner3 == false)
        {
            Instantiate(enemySpawner, actionRooms[(actionRooms.Count / 3) * 2].transform.position, Quaternion.identity);
            spawnedSpawner3 = true;
        }

    }

}
