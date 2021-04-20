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

    void Awake()
    {
        instance = this;
    }

    public void SpawnEnemies(Room room)
    {
        if (room.visited || room.name.Contains("Start") == true){ return; }
        else { room.visited = true; }

        difficulty++;

        if (difficulty % roomsUntilDifficultyIncrease == 0){numOfEnemies++;}

        if (numOfEnemies > enemyCap) {numOfEnemies = enemyCap;}
        else if (numOfEnemies < 1){ numOfEnemies = 1; }

        for (int i = 0; i < numOfEnemies; i++)
        {
            int temp = Random.Range(1, 3);

            float tempX = Random.Range((room.x * room.width) + (room.width / 2) - 1, (room.x * room.width) - (room.width / 2) + 1);
            float tempY = Random.Range((room.y * room.height) + (room.height / 2) - 1, (room.y * room.height) - (room.height / 2) + 1);

            switch (temp)
            {
                case 1:
                    Instantiate(catGirl, new Vector3(tempX, tempY, 0), Quaternion.identity);
                    enemiesSpawned++;
                    break;
                case 2:
                    Instantiate(fanArtist, new Vector3(tempX, tempY, 0), Quaternion.identity);
                    enemiesSpawned++;
                    break;
                default:
                    break;
            }
        }
    }

}
