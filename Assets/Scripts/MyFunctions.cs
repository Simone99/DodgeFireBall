using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyFunctions : MonoBehaviour
{

    public static float multiplier = 1.45f;

    public static Vector2 Rotate(Vector2 v, float degrees) {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
        
        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
     }

    public static float Bounce(Collision2D collision, Rigidbody2D rb){
        ContactPoint2D cp = collision.GetContact(0);
        Vector2 tangent = Vector2.Perpendicular(cp.normal);
        float angle = Vector2.SignedAngle(tangent, rb.linearVelocity);
        float angleRotation = 2*(180f - Mathf.Abs(angle));
        rb.linearVelocity = MyFunctions.Rotate(rb.linearVelocity, angleRotation);
        return angleRotation;
    }

    public static void PauseGame ()
    {
        Time.timeScale = 0;
    }

    public static void ResumeGame(){
        Time.timeScale = 1;
    }

    public static void ResetGame(){
        SceneManager.LoadScene("SampleScene");
        AudioListener.pause = false;
        ResumeGame();
    }
    
    public static bool gamePaused(){
        return Time.timeScale == 0;
    }

}
