using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup: MonoBehaviour
{
    public string name;
    public bool used;
    public List<Powerup> powerups = new List<Powerup>();
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

    public virtual void Use()
    {

    }

    public void SpawnNew()
    {
        int rand = Random.Range(1, 12);
        switch(rand)
        {
            case 1:
                Powerup crunchyroll = new CrunchyRoll();
                powerups.Add(crunchyroll);
                break;
            case 2:
                Powerup ship = new Ship();
                powerups.Add(ship);
                break;
            case 3:
                Powerup wehat = new Wehat();
                powerups.Add(wehat);
                break;
        }
    }
}
