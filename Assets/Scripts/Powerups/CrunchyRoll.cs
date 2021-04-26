using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrunchyRoll : Powerup
{
    PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {

        name = "Crunchy Roll";
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
            pc.health += pc.health/3;
        }
    }
}
