using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : Powerup
{
    // Start is called before the first frame update
    void Start()
    {
        name = "Ship";
        used = false;
        infoText = "Let the wind fill your sails and maybe your ship never sink! Speed Up Temporarily!";
        SpriteRenderer temp = gameObject.GetComponent<SpriteRenderer>();
        mySprite = temp.sprite;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Use()
    {
        if (used == false)
        {
            //MenuScript.instance.playerScript.speed *= 1.5f;

            StartCoroutine(UseEffects());
        }
    }

    IEnumerator UseEffects()
    {
        if (used == false)
        {
            MenuScript.instance.playerScript.speed *= 1.5f;

            yield return new WaitForSeconds(useTime); //effects Only Last So Long

            MenuScript.instance.playerScript.speed /= 1.5f;

            Destroy(gameObject);
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            BoxCollider2D box = gameObject.GetComponent<BoxCollider2D>();
            Rigidbody2D body = gameObject.GetComponent<Rigidbody2D>();
            SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();

            Destroy(box);
            Destroy(body);
            Destroy(sprite);

            Powerup.instance.powerUpUI[Powerup.instance.powerups.Count].gameObject.SetActive(true);

            //SpriteRenderer temp = gameObject.GetComponent<SpriteRenderer>();
            Powerup.instance.powerUpUI[Powerup.instance.powerups.Count].image.sprite = mySprite;
            Powerup.instance.powerUpUISprites.Add(mySprite);

            if (!Powerup.instance.shipFirstTime)
            {
                StartCoroutine(MenuScript.instance.DisplayPowerUpInfo(mySprite, infoText));
                Powerup.instance.shipFirstTime = true;
            }

            Powerup.instance.powerups.Add(gameObject);

            if (!MenuScript.instance.instructions.isActiveAndEnabled)
            {
                MenuScript.instance.instructions.gameObject.SetActive(true);
            }
        }
    }
}
