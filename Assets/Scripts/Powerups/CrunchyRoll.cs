using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrunchyRoll : Powerup
{
    // Start is called before the first frame update
    void Start()
    {

        name = "Crunchy Roll";
        used = false;
        infoText = "A little crunch never hurt anybody! Health Up!";
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
            StartCoroutine(UseEffects());
            //MenuScript.instance.playerScript.health += 1;
            //MenuScript.instance.HandleHealth();
        }
    }

    IEnumerator UseEffects()
    {
        if (used == false)
        {
            if (MenuScript.instance.playerScript.health < 6)
            {
                MenuScript.instance.playerScript.health += 1;
                MenuScript.instance.HandleHealth();
            }

            yield return new WaitForSeconds(useTime); //effects Only Last So Long

            Destroy(gameObject);
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Powerup.instance.powerups.Count <= 5)
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

                if (!Powerup.instance.crunchyRollFirstTime)
                {
                    StartCoroutine(MenuScript.instance.DisplayPowerUpInfo(mySprite, infoText));
                    Powerup.instance.crunchyRollFirstTime = true;
                }

                Powerup.instance.powerups.Add(gameObject);
            }

            if (!MenuScript.instance.instructions.isActiveAndEnabled)
            {
                MenuScript.instance.instructions.gameObject.SetActive(true);
            }
        }
    }

}
