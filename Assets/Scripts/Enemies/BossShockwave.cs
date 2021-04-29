using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShockwave : MonoBehaviour
{
    //public PlayerController player;

    public float speed = .5f;

    public BossBoi bossScript;

    // Start is called before the first frame update
    void Start()
    {
        bossScript = GetComponentInParent<BossBoi>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x >= 5)
        {
            ResetSize();
        }

        transform.localScale = new Vector3(transform.localScale.x + speed, transform.localScale.y + speed, transform.localScale.z);

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            bossScript.player.health--;
            bossScript.player.isStunned = true;
            bossScript.player.speed = 0f;
            bossScript.stunTimer = 1f;
            ResetSize();
        }

    }

    private void ResetSize()
    {
        transform.localScale = new Vector3(1, 1, 1);
        gameObject.SetActive(false);
    }
}
