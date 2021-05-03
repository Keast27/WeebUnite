using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wehat : Powerup
{
    // Start is called before the first frame update
    void Start()
    {
        name = "Wehat";
        used = false;
        infoText = "It is said that the mystical creature known as the Wehat can help you to overthrow even the toughest enemies. Charge Time Down Temporarily!";
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
            //MenuScript.instance.playerScript.nextCharge -= 1f;
            //MenuScript.instance.playerScript.previousNextCharge -= 1f;

            StartCoroutine(UseEffects());
        }

    }

    IEnumerator UseEffects()
    {
        if (used == false)
        {
            MenuScript.instance.playerScript.nextCharge -= 1f;
            MenuScript.instance.playerScript.previousNextCharge -= 1f;

            yield return new WaitForSeconds(useTime); //effects Only Last So Long

            MenuScript.instance.playerScript.nextCharge += 1f;
            MenuScript.instance.playerScript.previousNextCharge += 1f;

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

            if (!Powerup.instance.weHatFirstTime)
            {
                StartCoroutine(MenuScript.instance.DisplayPowerUpInfo(mySprite, infoText));
                Powerup.instance.weHatFirstTime = true;
            }

            Powerup.instance.powerups.Add(gameObject);

            if (!MenuScript.instance.instructions.isActiveAndEnabled)
            {
                MenuScript.instance.instructions.gameObject.SetActive(true);
            }
        }
    }
}
