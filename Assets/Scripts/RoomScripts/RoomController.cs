using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

//Basic Information For Each Room
public class  RoomInfo
{
    public string name;
    public int x;
    public int y;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;
    string currentWorldName = "Basement";
    RoomInfo currentLoadRoomData;
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    public List<Room> loadedRooms = new List<Room>();
    bool isLoadingRoom = false;
    Room currentRoom;

    bool spawnedBossRoom = false;
    bool updatedRooms = false;

    void Awake()
    {
        instance = this;
    }

    //void Start()
    //{
    //    LoadRoom("Start", 0, 0);
    //    LoadRoom("Empty", 1, 0);
    //    LoadRoom("Empty", -1, 0);
    //    LoadRoom("Empty", 0, 1);
    //    LoadRoom("Empty", 0, -1);
    //}

    private void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        //Break if Room is Currently Being Loaded
        if (isLoadingRoom){return;}

        //If All Rooms Have Been Spawned...
        if (loadRoomQueue.Count == 0)
        {
            //And Boss Room Has Not Been Spawned, Spawn the Boss Room
            if (!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            //And Boss Room Has Been Spawned but Doors Have Not Been Updated, Update the Doors and Walls
            else if (spawnedBossRoom && !updatedRooms)
            {
                GameObject[] replacmentWalls = GameObject.FindGameObjectsWithTag("ReplacementWall");

                foreach (GameObject rw in replacmentWalls)
                {
                    rw.SetActive(false);
                }

                foreach (Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                updatedRooms = true;
            }
            return;
        }

        //Load The Next Queued Room 
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    //Spawn Boss Room
    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        yield return new WaitForSeconds(.5f); //Ensure that All Rooms are Finished Spawning
        if (loadRoomQueue.Count == 0)
        {
            //SpawnBossBasedOnLastRoom();
            SpawnBossBasedOnDistance();
        }
    }

    //Add The New Room to The Load Queue
    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x,y))
        {
            return;
        }

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.x = x;
        newRoomData.y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    //Load up the Scene of the Room Type in the Given Position
    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while(loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    //Once a Room is Loaded, Register it (Keep Track of its Values)
    public void RegisterRoom(Room room)
    {
        //Check if a Room Already Exists Here
        if (!DoesRoomExist(currentLoadRoomData.x, currentLoadRoomData.y))
        {

            room.transform.position = new Vector3(currentLoadRoomData.x * room.width, currentLoadRoomData.y * room.height, 0);

            room.x = currentLoadRoomData.x;
            room.y = currentLoadRoomData.y;
            room.name = currentWorldName + "." + currentLoadRoomData.name + " " + room.x + ", " + room.y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if (loadedRooms.Count == 0)
            {
                CameraController.instance.currRoom = room;
            }

            loadedRooms.Add(room);
        }
        else
        {
            //If There is a Room Do Not Add This Room
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }

    //Check if a room exists
    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.x == x && item.y == y) != null;
    }

    //Return Room at Position
    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.x == x && item.y == y);
    }

    //Handle Player Switching Rooms
    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currRoom = room;
        currentRoom = room;
        EnemySpawn.instance.SpawnEnemies(room);
    }

    public void Reset()
    {

        currentLoadRoomData = null;
        loadRoomQueue = new Queue<RoomInfo>();
        loadedRooms = new List<Room>();
        isLoadingRoom = false;
        currentRoom = null;

        spawnedBossRoom = false;
        updatedRooms = false;
    }

    private void SpawnBossBasedOnLastRoom()
    {
        //Load Boss Room in the Location of the Last Added Room and Delete the Empty Room Placed There
        Room bossRoom = loadedRooms[loadedRooms.Count - 1];
        int tempx = bossRoom.x;
        int tempy = bossRoom.y;
        //Room tempRoom = new Room(bossRoom.x, bossRoom.y);
        Destroy(bossRoom.gameObject);
        var roomToRemove = loadedRooms.Single(r => r.x == tempx && r.y == tempy);
        loadedRooms.Remove(roomToRemove);
        LoadRoom("End", tempx, tempy);
    }

    private void SpawnBossBasedOnDistance()
    {
        float dist = 0;
        Room tempRoom = null;

        //Find Furthest Room From Start
        foreach (Room r in loadedRooms)
        {
            float temp = Mathf.Sqrt(Mathf.Pow(r.x, 2) + Mathf.Pow(r.y, 2));

            if (temp >= dist)
            {
                dist = temp;
                tempRoom = r;
            }
        }

        //Spawn Boss in This Room's Location
        Room bossRoom = tempRoom;
        int tempx = bossRoom.x;
        int tempy = bossRoom.y;
        //Room tempRoom = new Room(bossRoom.x, bossRoom.y);
        Destroy(bossRoom.gameObject);
        var roomToRemove = loadedRooms.Single(r => r.x == tempx && r.y == tempy);
        loadedRooms.Remove(roomToRemove);
        LoadRoom("End", tempx, tempy);
    }



}
