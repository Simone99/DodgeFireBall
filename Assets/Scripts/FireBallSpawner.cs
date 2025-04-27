using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FireBallSpawner : MonoBehaviour
{
    public GameObject fireball;
    public float startTime;
    public Vector2 screenBounds;
    public SpriteRenderer spriteImage;
    public int fireballCount;
    public Text fireballCountText;
    private int highScore;
    public Text highScoreText;
    public GameObject explosion;
    public GameObject newRecordText;
    public Text tryAgainText;
    public static bool gameOver;
    private List<GameObject> fireballList = new List<GameObject>();
    public float velocityMultiplier;
    public float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        fireballCount = 1;
        int tmp = PlayerPrefs.GetInt("HighScore", -1);
        if(tmp != -1){
            highScore = tmp;
        }else{
            highScore = 1;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        highScoreText.text = "High Score: " + highScore.ToString();
        newRecordText.SetActive(false);
        tryAgainText.text = "";
        gameOver = false;
        fireballList.Add(fireball);
    }

    // Update is called once per frame
    void Update()
    {
        if(!MyFunctions.gamePaused()){
            if(Time.time - startTime >= spawnTime){
                startTime = Time.time;
                if(fireballCount % 10 == 0){
                    velocityMultiplier *= MyFunctions.multiplier;
                }
                fireballList.Add(spawnNewFireBall());
                /*Rigidbody2D tmp = fireballList[fireballCount].GetComponent<Rigidbody2D>();
                for(int i = 0; i < nTimesSpeededUp; i++){
                    tmp.velocity *= velocityMultiplier;
                }*/
                fireballCount++;
                fireballCountText.text = fireballCount.ToString();
            }
        }else if(gameOver){
            if(fireballCount > highScore){
                PlayerPrefs.SetInt("HighScore", fireballCount);
                newRecordText.SetActive(true);
            }else{
                // tryAgainText.text = "Try again!\nTouch the screen to retry";
                tryAgainText.text = "Try again!";
            }
            if (Input.touchCount > 0){
                SceneManager.LoadScene("SampleScene");
                MyFunctions.ResumeGame();
                AudioListener.pause = false;
            }
        }
        
    }

    private GameObject spawnNewFireBall(){
        Vector2 tmp;
        do{
           tmp = new Vector2(Random.Range(-screenBounds.x + spriteImage.bounds.extents.x, screenBounds.x - spriteImage.bounds.extents.x), Random.Range(-screenBounds.y + spriteImage.bounds.extents.y, screenBounds.y - spriteImage.bounds.extents.y));
        }while(Physics2D.OverlapPoint(tmp) != null);
        GameObject g = Instantiate(fireball, tmp, Quaternion.Euler(0, 0, 0));
        //Rigidbody2D tmpRigidBody = g.GetComponent<Rigidbody2D>();
        /*for(int i = 0; i < nTimesSpeededUp; i++){
            print("I entered the cycle");
            tmpRigidBody.velocity *= velocityMultiplier;
        }*/
        return g;
     }

     public void setGameOver(bool g){
         gameOver = g;
     }
}
