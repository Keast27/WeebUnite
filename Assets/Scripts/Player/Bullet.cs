using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    //   public int dir = 1;
    //  private Rigidbody2D rb;
    //   float bulletSpeed = 10;
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector2(dir * bulletSpeed , 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Door") && collision.gameObject.tag !="HitBox")
        {
            Debug.Log("Shot " + collision.name);
            Destroy(gameObject);
        }
    }
}
