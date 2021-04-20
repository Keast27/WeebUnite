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

    void Start()
    {
        base.Start();
        pounceFinish = true;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    //protected override void AggroMovement()
    //{
    //    if (pounceFinish)
    //    {
    //        pounceTimer += Time.deltaTime;
    //
    //        // Jump every second
    //        if (pounceTimer > 1)
    //        {
    //            // Get a random number and then jump anywhere from 50 to 75 % to the player
    //            float ran_jump = Random.Range(.5f, 1.0f);
    //            pouncePos = ran_jump * (new Vector3(target.transform.position.x, target.transform.position.y + 0.35f, target.transform.position.z) - transform.position) + transform.position;
    //            // Hoping is finished, set timer to 0
    //            pounceFinish = false;
    //            //animator.SetBool("hopfinish", false);
    //            pounceTimer = 0;
    //
    //            // Activate the tween once which will restart the jumping at the end
    //            LeanTween.move(gameObject, pouncePos, pounceTime).setEase(LeanTweenType.easeInOutCubic).setOnComplete(() =>
    //            {
    //                pounceFinish = true;
    //                //animator.SetBool("hopfinish", true);
    //            });
    //        }
    //    }
    //}
}
