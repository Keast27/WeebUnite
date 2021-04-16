using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 -> Needs Bottom Door
    //2 -> Needs Top Door
    //3 -> Needs Left Door
    //4 -> Needs Right Door

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    //public float waitTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, waitTime); //Destroy the Spawn Points After For Optimization Purposes
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", .5f);
    }

    void Spawn()
    {
        if (spawned == false)
        {

            if (openingDirection == 1)
            {
                //Need to Spawn a Room With a Bottom Door
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                spawned = true;
            }
            else if (openingDirection == 2)
            {
                //Need to Spawn a Room With a Top Door
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                spawned = true;
            }
            else if (openingDirection == 3)
            {
                //Need to Spawn a Room With a Left Door
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                spawned = true;
            }
            else if (openingDirection == 4)
            {
                //Need to Spawn a Room With a Right Door
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                spawned = true;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                //Spawn Walls Blocking Off Any Openings
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
            }
            spawned = true;
        }
    }
}
