using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBoi : Enemy
{
    public bool shockwave = false;
    public bool summonArmy = false;
    public bool musicNotes = false;

    public GameObject stan;

    public float timeBetweenAttacks = 5;
    public float timeBetweenBullets = 2;

    public float x1;
    public float x2;

    public float y;

    [SerializeField] private Projectile projectile;

    //[SerializeField] private float fireRate = 0.75f;
    //[SerializeField] private float fireCooldown = 0.75f;
    public float timer;
    private float attackTimer;
    public int maxNotes = 3;
    public int notesFired = 0;


    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        //Kill Mr. Boss
        if (health <= 0)
        {
            MenuScript.instance.bossDefeated = true;
            Destroy(gameObject);
        }

        //Update Timer if not in attack
        if (!musicNotes && !shockwave && !summonArmy)
        {
            timer += Time.deltaTime;
        }

        //Select Attack
        if (timer >= timeBetweenAttacks)
        {
            timer = 0;

            int temp = Random.Range(1, 4);

            switch (temp)
            {
                case 1:
                    musicNotes = true;
                    break;
                case 2:
                    shockwave = true;
                    break;
                case 3:
                    summonArmy = true;
                    break;
                default:
                    break;
            }
        }

        //Handle Active Attacks
        if (musicNotes)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= timeBetweenBullets)
            {
                MusicNotes();
                attackTimer = 0f;
            }

            if (notesFired >= maxNotes)
            {
                musicNotes = false;
                notesFired = 0;
            }

        }
        if (shockwave)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            shockwave = false;
        }
        if(summonArmy)
        {
            SummonArmy();
            summonArmy = false;
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Damage();
        }

        if (other.gameObject.tag == "Bullet")
        {
            //Destroy(gameObject);
            health--;
            Destroy(other.gameObject);
        }
    }

    public void MusicNotes()
    {
        Vector2 projectileDirection = target.transform.position - transform.position;
        Projectile newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

        // Ignores collision between all objects in layer
        newProjectile.Init(gameObject.layer, projectileDirection.normalized);

        notesFired++;
    }

    //protected override void Attack()
    //{
    //    timer += Time.deltaTime;
    //
    //    if (timer >= fireRate)
    //    {
    //        //canMove = false;
    //        timer = 0;
    //        MusicNotes();
    //
    //        Debug.Log("Enemy shoots");
    //        //if (!InAttackRange())
    //        //    canMove = true;
    //
    //        StopCoroutine(FireCooldown());
    //        StartCoroutine(FireCooldown());
    //}
    //
    //}

    // Temp. Will change when animations are in
    //IEnumerator FireCooldown()
    //{
    //    yield return new WaitForSeconds(fireCooldown);
    //    canMove = true;
    //}

    public void SummonArmy()
    {
        float tempX = EnemySpawn.instance.bossRoom.width / 4;
        float tempY = EnemySpawn.instance.bossRoom.height / 4;

        Instantiate(EnemySpawn.instance.stan, new Vector3(EnemySpawn.instance.bossRoom.GetRoomCenter().x + tempX, EnemySpawn.instance.bossRoom.GetRoomCenter().y + tempY, 0), Quaternion.identity);
        Instantiate(EnemySpawn.instance.stan, new Vector3(EnemySpawn.instance.bossRoom.GetRoomCenter().x + (tempX / 2), EnemySpawn.instance.bossRoom.GetRoomCenter().y + tempY, 0), Quaternion.identity);
        Instantiate(EnemySpawn.instance.stan, new Vector3(EnemySpawn.instance.bossRoom.GetRoomCenter().x - tempX, EnemySpawn.instance.bossRoom.GetRoomCenter().y + tempY, 0), Quaternion.identity);
        Instantiate(EnemySpawn.instance.stan, new Vector3(EnemySpawn.instance.bossRoom.GetRoomCenter().x - (tempX / 2), EnemySpawn.instance.bossRoom.GetRoomCenter().y + tempY, 0), Quaternion.identity);

        //Instantiate(stan, new Vector3(x1, y, 0), Quaternion.identity);
        //Instantiate(stan, new Vector3(x2, y, 0), Quaternion.identity);
        //Instantiate(stan, new Vector3(-x1, y, 0), Quaternion.identity);
        //Instantiate(stan, new Vector3(-x2, y, 0), Quaternion.identity);

        EnemySpawn.instance.enemiesSpawned += 4;
    }
}
