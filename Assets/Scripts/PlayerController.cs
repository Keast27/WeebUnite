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

    int dir = 1;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


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

    private void FixedUpdate()
    {
        move();
        /*
        if (Input.GetButtonUp("Fire3") || Input.GetButtonUp("Fire1"))
        {
            Debug.Log("I hope I'm good at programming");
            spriteRend.color = Color.cyan;
            weaponMoves();
        }
        */


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
    void move()
    {
        velocity = speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement * velocity);
    }
}
