using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : Powerup
{
    PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        name = "Ship";
        used = false;
    }

    // Update is called once per frame
    void Update()
    {
        pc = gameObject.GetComponent<PlayerController>();
    }

    public override void Use()
    {
        if (used == false)
        {
            pc.velocity *= 2;
        }
    }
}
