using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanArtist : Enemy
{
    [SerializeField] private Projectile projectile;

    [SerializeField] protected float fireRate = 0.75f;
    [SerializeField] protected float fireCooldown = 0.75f;
    private float timer;
    private float attackTimer;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuScript.instance.pause)
        {
            return;
        }
        base.Update();

        attackTimer += Time.deltaTime;

        if(attackTimer >= 2)
        {
            ShootProjectile();
            attackTimer = 0f;
        }
    }

    protected override void Attack()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            canMove = false;
            timer = 0;
            ShootProjectile();

            Debug.Log("Enemy shoots");
            //if (!InAttackRange())
            //    canMove = true;

            StopCoroutine(FireCooldown());
            StartCoroutine(FireCooldown());
        }

    }

    public void ShootProjectile()
    {
        Vector2 projectileDirection = target.transform.position - transform.position;
        //Instantiate(projectile, transform.position, Quaternion.identity);

        Projectile newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

        // Ignores collision between self and projectile
        //Physics2D.IgnoreCollision(newProjectile.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        // Ignores collision between all objects in layer
        newProjectile.Init(gameObject.layer, projectileDirection.normalized);

        //newProjectile.direction = projectileDirection.normalized;
    }

    // Temp. Will change when animations are in
    IEnumerator FireCooldown()
    {
        yield return new WaitForSeconds(fireCooldown);
        canMove = true;
    }
}
