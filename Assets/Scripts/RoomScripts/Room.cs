using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float width;
    public float height;
    public int x;
    public int y;

    private bool updatedDoors = false;

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    //public Door leftWall;
    //public Door rightWall;
    //public Door topWall;
    //public Door bottomWall;

    public GameObject sideWall;
    public GameObject topWall;

    public List<Door> doors = new List<Door>();

    public bool visited = false;

    private bool locked = false;

    //Basic Constructor
    public Room(int x, int y)
    {
        x = x;
        y = y;
    }


    // Start is called before the first frame update
    void Start()
    {
        //If There is No Room Controller Break
        if (RoomController.instance == null)
        {
            Debug.Log("There is no Room Controller in this Scene");
            return;
        }

        //Get All Doors for The Room and Assign it to the Matching Door Variable
        Door[] ds = GetComponentsInChildren<Door>();
        foreach (Door d in ds)
        {
            doors.Add(d);
            switch (d.doorType)
            {
                case Door.DoorType.left:
                    leftDoor = d;
                    break;
                case Door.DoorType.right:
                    rightDoor = d;
                    break;
                case Door.DoorType.top:
                    topDoor = d;
                    break;
                case Door.DoorType.bottom:
                    bottomDoor = d;
                    break;
                default:
                    break;
            }
        }

        //Register the Room's Values
        RoomController.instance.RegisterRoom(this);
    }

    void Update()
    {
        //Since the Boss Spawns Later Than the Other Rooms, Its Doors Are Not Always Checked in Time
        //So This Ensures that They Are Checked
        if (name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }

        if (EnemySpawn.instance.enemiesSpawned > 0 && locked == false)
        {
            locked = true;
            StartCoroutine(LockDoors());
        }
        else if (EnemySpawn.instance.enemiesSpawned <= 0 && locked == true)
        {
            UnlockDoors();
        }
    }

    //Removes Doors that Don't Lead to Other Rooms and Replace with a Wall
    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.left:
                    if (GetLeft() == null)
                    {
                        GameObject temp = door.transform.GetChild(0).gameObject;
                        temp.gameObject.SetActive(true);
                        temp.transform.parent = null;
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.right:
                    if (GetRight() == null)
                    {
                        GameObject temp = door.transform.GetChild(0).gameObject;
                        temp.gameObject.SetActive(true);
                        temp.transform.parent = null;
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.top:
                    if (GetTop() == null)
                    {
                        GameObject temp = door.transform.GetChild(0).gameObject;
                        temp.gameObject.SetActive(true);
                        temp.transform.parent = null;
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.bottom:
                    if (GetBottom() == null)
                    {
                        GameObject temp = door.transform.GetChild(0).gameObject;
                        temp.gameObject.SetActive(true);
                        temp.transform.parent = null;
                        door.gameObject.SetActive(false);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    //Check for Adjacent Rooms
    public Room GetRight()
    {
        if (RoomController.instance.DoesRoomExist(x + 1, y))
        {
            return RoomController.instance.FindRoom(x + 1, y);
        }

        return null;
    }
    public Room GetLeft()
    {
        if (RoomController.instance.DoesRoomExist(x - 1, y))
        {
            return RoomController.instance.FindRoom(x - 1, y);
        }

        return null;
    }
    public Room GetTop()
    {
        if (RoomController.instance.DoesRoomExist(x, y + 1))
        {
            return RoomController.instance.FindRoom(x, y + 1);
        }

        return null;
    }
    public Room GetBottom()
    {
        if (RoomController.instance.DoesRoomExist(x, y - 1))
        {
            return RoomController.instance.FindRoom(x, y - 1);
        }

        return null;
    }

    //Helpful Gizmos For Testing
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    //Return Center of Room
    public Vector3 GetRoomCenter()
    {
        return new Vector3(x * width, y * height);
    }

    //Handle Player Collison
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }

    //Lock Doors
    IEnumerator LockDoors()
    {
        yield return new WaitForSeconds(.5f);

        locked = true;

        if (GetRight() != null) {
            GameObject temp = rightDoor.transform.GetChild(0).gameObject;
            //temp.transform.localScale = new Vector3(temp.transform.localScale.x + 1, temp.transform.localScale.y + 1, temp.transform.localScale.z);
            temp.gameObject.SetActive(true);
        }

        if (GetLeft() != null) {
            GameObject temp = leftDoor.transform.GetChild(0).gameObject;
            //temp.transform.localScale = new Vector3(temp.transform.localScale.x + 1, temp.transform.localScale.y + 1, temp.transform.localScale.z);
            temp.gameObject.SetActive(true);
        }

        if (GetTop() != null) {
            GameObject temp = topDoor.transform.GetChild(0).gameObject;
            //temp.transform.localScale = new Vector3(temp.transform.localScale.x + 1, temp.transform.localScale.y + 1, temp.transform.localScale.z);
            temp.gameObject.SetActive(true);
        }

        if (GetBottom() != null) {
            GameObject temp = bottomDoor.transform.GetChild(0).gameObject;
            //temp.transform.localScale = new Vector3(temp.transform.localScale.x + 1, temp.transform.localScale.y + 1, temp.transform.localScale.z);
            temp.gameObject.SetActive(true);
        }


    }

    //Unlock Doors
    public void UnlockDoors()
    {
        locked = false;

        if (GetRight() != null)
        {
            GameObject temp = rightDoor.transform.GetChild(0).gameObject;
            //temp.transform.localScale = new Vector3(temp.transform.localScale.x - 1, temp.transform.localScale.y - 1, temp.transform.localScale.z);
            temp.gameObject.SetActive(false);
        }

        if (GetLeft() != null)
        {
            GameObject temp = leftDoor.transform.GetChild(0).gameObject;
            //temp.transform.localScale = new Vector3(temp.transform.localScale.x - 1, temp.transform.localScale.y - 1, temp.transform.localScale.z);
            temp.gameObject.SetActive(false);
        }

        if (GetTop() != null)
        {
            GameObject temp = topDoor.transform.GetChild(0).gameObject;
            //temp.transform.localScale = new Vector3(temp.transform.localScale.x - 1, temp.transform.localScale.y - 1, temp.transform.localScale.z);
            temp.gameObject.SetActive(false);
        }

        if (GetBottom() != null)
        {
            GameObject temp = bottomDoor.transform.GetChild(0).gameObject;
            //temp.transform.localScale = new Vector3(temp.transform.localScale.x - 1, temp.transform.localScale.y - 1, temp.transform.localScale.z);
            temp.gameObject.SetActive(false);
        }
    }
}
