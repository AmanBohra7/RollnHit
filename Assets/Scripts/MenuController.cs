using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    private GameManger managerInstance;

    public GameObject endGame;
    public GameObject pauseGame;
    public GameObject infoGame;

    [HideInInspector]
    public bool gamePaused = false, menuAwake = false ;
    [HideInInspector]
    public bool infoMenuAwake = false, pauseMenuAwake = false, endMenuAwake = false;
    [HideInInspector]
    public bool gameEnd = false;

    void Awake(){
        if(instance) Destroy(this);
        else instance = this;
        managerInstance = GameManger.instance;
    }

    void Start(){ 
        TriggerInfoMenu();
    }

    void Update(){
        if(!managerInstance.gameOver && Input.GetKeyDown(KeyCode.Escape) ){
            TriggerPauseMenu();
        }
    }

    IEnumerator waitAndPause()
    {
        managerInstance.GamePaused();
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 0f;
    }

    public void OpenMenu(GameObject obj){
        menuAwake = true;
        LeanTween.moveY(obj.GetComponent<RectTransform>(), 5f, .3f);
        LeanTween.scale(obj.GetComponent<RectTransform>(), obj.GetComponent<RectTransform>().localScale*3f, .6f)
                                .setEaseOutBack();
    }

    public void CloseMenu(GameObject obj){
        LeanTween.moveY(obj.GetComponent<RectTransform>(), -7.13f, .5f);    
        LeanTween.scale(obj.GetComponent<RectTransform>(),new Vector3(0,0,0), .2f);
        LeanTween.scale(obj.GetComponent<RectTransform>(),new Vector3(0.004f,0.004f,0.004f), .1f).setDelay(.3f);
        menuAwake = false;
        managerInstance.GameUnPaused();
        Time.timeScale = 1.0f;
    }

    public void TriggerInfoMenu(){
       if(infoMenuAwake == false && menuAwake == false && !managerInstance.gameOver){
           OpenMenu(infoGame);
           infoMenuAwake = true;
           StartCoroutine(waitAndPause());
       }else{
           if(infoMenuAwake == true && menuAwake == true && !managerInstance.gameOver){
               CloseMenu(infoGame);
               infoMenuAwake = false;
           }
       }
    }

    public void TriggerPauseMenu(){
        if(pauseMenuAwake == false && menuAwake == false && !managerInstance.gameOver){
            OpenMenu(pauseGame);
            pauseMenuAwake = true;
            StartCoroutine(waitAndPause());
        }else{
            if(pauseMenuAwake == true && menuAwake == true && !managerInstance.gameOver ){
                CloseMenu(pauseGame);
                pauseMenuAwake = false;
            }
        }
    }

    public void TriggerEndMenu(){
        if(managerInstance.gameOver){
            OpenMenu(endGame);
            menuAwake = true;
        }
    }

}
