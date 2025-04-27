using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameplayManager : MonoBehaviour
{
    public void PauseResume(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Started){
            if(!FireBallSpawner.gameOver){
                if(MyFunctions.gamePaused()){
                    AudioListener.pause = false;
                    MyFunctions.ResumeGame();
                }else{
                    AudioListener.pause = true;
                    MyFunctions.PauseGame();
                }
            }else{
                MyFunctions.ResetGame();
            }
        }
    }
}
