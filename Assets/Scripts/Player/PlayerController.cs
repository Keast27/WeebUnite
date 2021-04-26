using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public enum weapons
    {
        foamSword,
        wand
    }

    private GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRend;
    public float speed;
    public float velocity;
    public int health;
    Vector2 movement;
    public weapons selectedWeapon;

    public GameObject projectile;
    public float chargeDelta = 0.5F;

    public float nextCharge = 3.5F;
    private GameObject newProjectile;
    public float chargeTime = 0.0F;
    [SerializeField] private bool charged = false;
<<<<<<< Updated upstream:Assets/Scripts/Player/PlayerController.cs
    bool faceRight = true;
    Powerup PU;
=======
    public Powerup PU;
>>>>>>> Stashed changes:Assets/Scripts/PlayerController.cs

    public bool isStunned;
    private float stunTimer;

    int dir = 1;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        isStunned = false;
        stunTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStunned)
        {
            PU = gameObject.GetComponent<Powerup>();
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetButtonDown("Fire2"))
        {
            if (PU.powerups.Count > 0)
            {
                if (PU.powerups.Count > 0)
                {
                    PU.powerups[0].Use();
                    PU.powerups.RemoveAt(0);
                }
            }

            if (Input.GetButton("Fire3") || Input.GetButton("Fire1"))
            {
                chargeTime = chargeTime + Time.deltaTime;

                if (chargeTime > nextCharge && !charged)
                {
                    nextCharge = chargeTime + chargeDelta;
                    Debug.Log("CHARGED");
                    charged = true;
                }
            }
            spriteRend.color = Color.white;

            if (Input.GetButtonUp("Fire3") || Input.GetButtonUp("Fire1"))
            {

                Debug.Log("Hit!!");
                nextCharge = 3.5f;
                chargeTime = 0.0F;
                weaponMoves();
                charged = false;
            }

        }

        //stun escape!
        if (isStunned)
        {
            stunTimer += Time.deltaTime;
            if(stunTimer >= 1)
            {
                isStunned = false;
            }
        }

    }

    private void FixedUpdate()
    {
        move();
        flip();
    }

    private void weaponMoves()
    {
        switch (selectedWeapon)
        {
            case weapons.foamSword:
                if (charged)
                {
                    Debug.Log("Charged Sword Bonk");

                }
                else
                {
                    Debug.Log("Normal Sword Bop");
                }

                break;
            case weapons.wand:
                if (charged)
                {
                    Debug.Log("PEWPEWPEW");
                    int theta = 0;
                    for (int i = 0; i < 6; i++)
                    {
                        GameObject bullet = Instantiate(projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), new Quaternion(0, 0, 0, 0));

                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * Mathf.Cos(theta), bulletSpeed * Mathf.Sin(theta));
                        theta += 45;
                    }
                }
                else
                {
                    Debug.Log("pew");
                    GameObject bullet = Instantiate(projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), new Quaternion(0, 0, 0, 0));
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(dir * bulletSpeed, 0);
                }
                break;
        }

    }

    void flip()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            dir = -1;
            if (faceRight)
            {
                transform.Rotate(0, 180, 0);
                faceRight = false;
            }
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            dir = 1;
            if (!faceRight)
            {
                transform.Rotate(0, 180, 0);
                faceRight = true;
            }
        }
    }
    void move()
    {
        velocity = speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement * velocity);
    }
}
