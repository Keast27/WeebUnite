using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerup: MonoBehaviour
{
    public string name;
    public bool used;
    public string infoText;
    public Sprite mySprite;
    public List<GameObject> powerups = new List<GameObject>();
    public static Powerup instance;

    public GameObject weHat;
    public GameObject ship;
    public GameObject crunchyRoll;

    public PlayerController pc;

    public List<Button> powerUpUI = new List<Button>();
    public List<Sprite> powerUpUISprites = new List<Sprite>();
    public List<RawImage> indexBackground = new List<RawImage>();

    public bool weHatFirstTime = false;
    public bool crunchyRollFirstTime = false;
    public bool shipFirstTime = false;

    public int selectedIndex = 0;

    public float useTime = 5;

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
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        if (powerups.Count <= 0)
        {
            for (int i = 0; i < indexBackground.Count; i++)
            {
                indexBackground[i].gameObject.SetActive(false);
            }
        }

        //Cycle Through The Avaliable PowerUps
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (powerups.Count >= 1)
            {
                indexBackground[selectedIndex].gameObject.SetActive(false);

                selectedIndex = 0;

                indexBackground[selectedIndex].gameObject.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (powerups.Count >= 2)
            {
                indexBackground[selectedIndex].gameObject.SetActive(false);

                selectedIndex = 1;

                indexBackground[selectedIndex].gameObject.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (powerups.Count >= 3)
            {
                indexBackground[selectedIndex].gameObject.SetActive(false);

                selectedIndex = 2;

                indexBackground[selectedIndex].gameObject.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (powerups.Count >= 4)
            {
                indexBackground[selectedIndex].gameObject.SetActive(false);

                selectedIndex = 3;

                indexBackground[selectedIndex].gameObject.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (powerups.Count >= 5)
            {
                indexBackground[selectedIndex].gameObject.SetActive(false);

                selectedIndex = 4;

                indexBackground[selectedIndex].gameObject.SetActive(true);
            }
        }
    }

    public virtual void Use()
    {

    }

    public void SpawnNew(Vector3 enemyPos)
    {
        int rand = Random.Range(1, 10);
        switch(rand)
        {
            case 1:
            case 2:
                GameObject tempCrunchyroll = Instantiate(crunchyRoll, enemyPos, Quaternion.identity);//new CrunchyRoll();
                //powerups.Add(tempCrunchyroll);
                break;
            case 3:
                GameObject tempShip = Instantiate(ship, enemyPos, Quaternion.identity);
                //powerups.Add(tempShip);
                break;
            case 4:
                GameObject tempWeHat = Instantiate(weHat, enemyPos, Quaternion.identity);
                //powerups.Add(tempWeHat);
                break;
            default:
                break;
        }
    }

}
