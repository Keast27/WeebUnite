using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;

    private void Start()
    {
        GenerateDungeon();
    }

    //Generate Dungeon
    public void GenerateDungeon()
    {
        //Reset Values
        RoomController.instance.Reset();

        //Genrate Room Locations Based on the Data Passed Throught he Inspector
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }

    //Spawns the Rooms in the Given Locations
    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomController.instance.LoadRoom("Start", 0, 0);

        foreach (Vector2Int roomLocation in rooms)
        {
                RoomController.instance.LoadRoom("Empty", roomLocation.x, roomLocation.y);
        }
    }
}
