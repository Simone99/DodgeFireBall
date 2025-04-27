using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    public GameObject player;
    public GameObject collisionFireball;


    public void setCollisionFireball(GameObject fireball){
        collisionFireball = fireball;
    }

    public void destroyObjects(){
        Object.Destroy(player);
        Object.Destroy(collisionFireball);
    }

    public void setGameOver(){
        Camera.main.GetComponent<FireBallSpawner>().setGameOver(true);
        AudioListener.pause = true;
    }
}
