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

    public Room(int x, int y)
    {
        x = x;
        y = y;
    }

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    public GameObject sideWall;
    public GameObject topWall;

    public List<Door> doors = new List<Door>();

    // Start is called before the first frame update
    void Start()
    {
        if (RoomController.instance == null)
        {
            Debug.Log("Wrong Scene");
            return;
        }

        //Get All Doors for The Room
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

        RoomController.instance.RegisterRoom(this);
    }

    void Update()
    {
        if (name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
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

    //Helpful Gizmos
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
}
