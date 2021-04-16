using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public float distance = 1f;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
    }

    public void MoveUp()
    {
        transform.position += new Vector3(0, distance, 0);
    }
    public void MoveDown()
    {
        transform.position += new Vector3(0, -distance, 0);
    }
    public void MoveLeft()
    {
        transform.position += new Vector3(-distance, 0, 0);
    }
    public void MoveRight()
    {
        transform.position += new Vector3(distance, 0, 0);
    }
}
