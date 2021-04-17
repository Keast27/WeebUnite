using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup
{
    public string name;
    public bool used;
    public string Name
    {
        get { return name; }
    }
    public bool Used
    {
        get { return used; }
        set { used = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Use();
}
