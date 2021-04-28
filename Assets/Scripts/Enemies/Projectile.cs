using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject target;
    public PlayerController player;

    //projectile's movement vectors
    private Vector3 position;
    public Vector3 direction;

    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float accelerationRate = 1.05f;
    [SerializeField] private int damage = 3;

    // Start is called before the first frame update
    void Start()
    {
        //initialize position vector
        position = transform.position;
    }

    public void Init(LayerMask layer, Vector2 direction)
    {
        //Physics2D.IgnoreLayerCollision(layer, gameObject.layer);
        this.direction = direction;
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        // You could just do this with Destroy(this, float time)
        //StartCoroutine(KillProjectile());

        target = GameObject.FindGameObjectWithTag("Player");
        player = target.GetComponent<PlayerController>();

        //move towards target
        position += direction * speed * Time.deltaTime;
        transform.position = position;

        //increase speed
        if (speed <= maxSpeed)
        {
            speed *= accelerationRate;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();

        //if (damagable != null)
        //damagable.DamageAndKnockback(damage, transform.position);

        if (other.gameObject.tag == "Player")
        {
            player.health -= damage;
            Destroy(gameObject);
        }

    }

    private IEnumerator KillProjectile()
    {
        //destroy the bullet if it doesn't collide with something for 3 seconds
        yield return new WaitForSecondsRealtime(1);
        Destroy(this.gameObject);
    }
}
