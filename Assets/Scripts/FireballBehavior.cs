using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehavior : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform tr;
    private FireBallSpawner spawner;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 playerPosition = GameObject.Find("player").transform.position;
        spawner = Camera.main.GetComponent<FireBallSpawner>();
        speed = spawner.velocityMultiplier;
        rb.linearVelocity = (Vector2)tr.position - playerPosition; //So the new spawned fireballs don't hit the player as soon as they are generated
        rb.linearVelocity = rb.linearVelocity.normalized * speed;
        tr.Rotate(0, 0, Vector2.SignedAngle(new Vector2(-1, -1), rb.linearVelocity));
    }

    void LateUpdate(){
        if(spawner.fireballCount % 10 == 0 && spawner.startTime == Time.time){
            rb.linearVelocity *= MyFunctions.multiplier;
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "MainCamera" || collision.gameObject.tag == "Fireball")
        {
            tr.Rotate(0, 0, MyFunctions.Bounce(collision, rb));
        }
    }
}
