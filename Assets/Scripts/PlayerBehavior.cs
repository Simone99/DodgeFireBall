using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 startTouchPosition;
    public Animator walkAnim;
    public GameObject explosion;
    public float playerSpeed;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        rb.linearVelocity = Vector2.down * playerSpeed;
        explosion.SetActive(false);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.touchCount > 0 && !MyFunctions.gamePaused())
        // {
        //     int i;
        //     bool touchUIButtons = false;
        //     Touch touch = Input.GetTouch(0);

        //     for(i = 0; i<Input.touchCount;i++){
        //         touch = Input.GetTouch(i);
        //         if(checkOverlap(touch.fingerId)){
        //             touchUIButtons = true;
        //             break;
        //         }
        //     }

        //     if(!touchUIButtons){
        //         // Handle finger movements based on TouchPhase
        //         switch (touch.phase)
        //         {
        //             //When a touch has first been detected, change the message and record the starting position
        //             case TouchPhase.Began:
        //                 // Record initial touch position.
        //                 startTouchPosition = touch.position;
        //                 break;

        //             //Determine if the touch is a moving touch
        //             case TouchPhase.Moved:
        //                 // Determine direction by comparing the current touch position with the initial one
        //                 rb.linearVelocity = touch.position - startTouchPosition;
        //                 rb.linearVelocity = rb.linearVelocity.normalized * playerSpeed;
        //                 ChangeAnimation(Vector2.SignedAngle(Vector2.right, rb.linearVelocity));
        //                 break;

        //             case TouchPhase.Ended:
        //                 // Report that the touch has ended when it ends
        //                 break;
        //         }
        //     }
        // }
    }

    void LateUpdate(){
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Fireball"){
            explosion.transform.position = collision.GetContact(0).point;
            explosion.GetComponent<ExplosionBehavior>().setCollisionFireball(collision.gameObject);
            explosion.SetActive(true);
            MyFunctions.PauseGame();
        }
    }

    void ChangeAnimation(float angle){
        if(angle >= -45 && angle < 45){
            walkAnim.Play("EastWalk");
        }else if(angle >= 45 && angle < 135){
            walkAnim.Play("NorthWalk");
        }else if(angle >= 135 || angle < -135){
            walkAnim.Play("WestWalk");
        }else if(angle <-45 && angle >= -135){
            walkAnim.Play("SouthWalk");
        }
    }

    private bool checkOverlap(int id){
        return EventSystem.current.IsPointerOverGameObject(id);
    }

    public void OnMove(InputAction.CallbackContext context) {
        if (!MyFunctions.gamePaused()){
            Vector2 direction = context.ReadValue<Vector2>();
            rb.linearVelocity = direction;
            rb.linearVelocity = rb.linearVelocity.normalized * playerSpeed;
            ChangeAnimation(Vector2.SignedAngle(Vector2.right, rb.linearVelocity));
        }
    }

}
