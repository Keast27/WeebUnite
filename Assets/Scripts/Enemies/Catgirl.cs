using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catgirl : Enemy
{
    // Start is called before the first frame update

    [SerializeField] private float pounceTime;
    private bool pounceFinish;
    public float pounceTimer;
    private Vector2 pouncePos;
    public GameObject childObject;
    public BoxCollider2D thisCollider;
    public BoxCollider2D childCollider;
    public BoxCollider2D childeCollider;
    //public Vector2 colliderBaseSize;
    public float colliderTimer = 2f;

    public float stunTimer = 2f;
    public float playerBaseSpeed;
    private bool gotSpeed = false;

    void Start()
    {
        base.Start();
        pounceFinish = true;
        pounceTime = 0.75f;

        thisCollider = this.GetComponent<BoxCollider2D>();
        childObject = transform.GetChild(0).gameObject;
        childeCollider = childObject.GetComponent<BoxCollider2D>();
        //colliderBaseSize = collider.size;
    }
    // Update is called once per frame
    void Update()
    {
        base.Update();
        stunTimer += Time.deltaTime;
        colliderTimer += Time.deltaTime;
        thisCollider.enabled = true;

        if (!gotSpeed)
        {
            GetPlayerSpeed();
        }

        if(stunTimer <= 0.5f)
        {
            //player.speed = 0f;
        }
        else
        {
            player.speed = playerBaseSpeed;
        }

        if(colliderTimer >= 0.25f)
        {
            childeCollider.enabled = false;
        }

    }

    
    protected override void AggroMovement()
    {
        if (pounceFinish)
        {
            
            pounceTimer += Time.deltaTime;

            // Jump every second
            if (pounceTimer > 1)
            {
                moveSpeed = 5;
                // Get a random number and then jump anywhere from 50 to 75 % to the player
                float ran_jump = Random.Range(.5f, 1.0f);
                pouncePos = ran_jump * (new Vector3(target.transform.position.x, target.transform.position.y + 0.35f, target.transform.position.z) - transform.position) + transform.position;
                // Hoping is finished, set timer to 0
                pounceFinish = false;
                //animator.SetBool("hopfinish", false);
                pounceTimer = 0;

                // Activate the tween once which will restart the jumping at the end
                LeanTween.move(gameObject, pouncePos, pounceTime).setEase(LeanTweenType.easeInOutCubic).setOnComplete(() =>
                {
                    moveSpeed = 0;
                    pounceFinish = true;
                    Attack();

                    //animator.SetBool("hopfinish", true);
                });
            }
        }
    }

    protected override void Attack()
    {
        childeCollider.enabled = true;
        colliderTimer = 0f;
    }

    public override void Damage()
    {
        //base.Damage();
        stunTimer = 0f;
    }

    private void GetPlayerSpeed()
    {
        playerBaseSpeed = player.speed;
        gotSpeed = true;
    }
}
