using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float speed;
    private float velocity;
    public int health;
    Vector2 movement;

    enum weapons
    {
        foamSword,
        wand
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    
    }

    private void FixedUpdate()
    {
        move();
    }

    void move()
    {
        velocity = speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement * velocity);
    }
}
