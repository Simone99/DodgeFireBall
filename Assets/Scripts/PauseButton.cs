using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public Text buttonText;

    public void Pause(){
        if(MyFunctions.gamePaused()){
            buttonText.text = "Pause";
            AudioListener.pause = false;
            MyFunctions.ResumeGame();
        }else{
            buttonText.text = "Resume";
            AudioListener.pause = true;
            MyFunctions.PauseGame();
        }
    }
}
