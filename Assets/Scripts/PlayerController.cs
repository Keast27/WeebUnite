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
    private weapons selectedWeapon = weapons.foamSword;

    public GameObject projectile;
    public float fireDelta = 0.5F;

    public float nextFire = 3.5F;
    private GameObject newProjectile;
    public float myTime = 0.0F;
    [SerializeField]  private bool charged = false;

    enum weapons
    {
        foamSword,
        wand
    }

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
                    
                }
                else
                {
                    Debug.Log("pew");
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
