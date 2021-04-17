using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contains Basic Variables For Doors
public class Door : MonoBehaviour
{
    public enum DoorType
    {
        left, right, top, bottom
    };

    public DoorType doorType;

}

