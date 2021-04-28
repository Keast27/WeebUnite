using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn instance;

    public int enemyCap;
    public int numOfEnemies = 0;
    public int roomsUntilDifficultyIncrease;
    public int difficulty;

    public int enemiesSpawned = 0;

    public GameObject catGirl;
    public GameObject fanArtist;
    public GameObject stan;
    public GameObject bossBoi;

    public bool bossSpawned = false;

    public Room bossRoom = null;

    void Awake()
    {
        instance = this;
    }

    public void SpawnEnemies(Room room)
    {
        //Don't Spawn Anything in Start
        if (room.visited || room.name.Contains("Start") == true){ return; }
        //Spawn Boss in Boss Room
        else if (room.name.Contains("End") == true)
        {
            bossRoom = room;
            room.visited = true;
            Instantiate(bossBoi, new Vector3(room.GetRoomCenter().x, room.GetRoomCenter().y + (room.height / 4), 0), Quaternion.identity);
            enemiesSpawned++;
            bossSpawned = true;
            return;
        }
        else { room.visited = true; }

        difficulty++;

        if (difficulty % roomsUntilDifficultyIncrease == 0){numOfEnemies++;}

        if (numOfEnemies > enemyCap) {numOfEnemies = enemyCap;}
        else if (numOfEnemies < 1){ numOfEnemies = 1; }

        for (int i = 0; i < numOfEnemies; i++)
        {
            int temp = Random.Range(1, 4);

            float tempX = Random.Range((room.x * room.width) + (room.width / 2) - 1, (room.x * room.width) - (room.width / 2) + 1);
            float tempY = Random.Range((room.y * room.height) + (room.height / 2) - 1, (room.y * room.height) - (room.height / 2) + 1);

            switch (temp)
            {
                case 1:
                    Instantiate(catGirl, new Vector3(tempX, tempY, 0), Quaternion.identity);
                    enemiesSpawned++;
                    break;
                case 2:
                case 3:
                    Instantiate(fanArtist, new Vector3(tempX, tempY, 0), Quaternion.identity);
                    enemiesSpawned++;
                    break;
                default:
                    break;
            }
        }
    }

}
