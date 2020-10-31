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
    public bool gamePaused = false;
    [HideInInspector]
    public bool menuAwake = false;
    [HideInInspector]
    public bool infoMenuAwake = false;

    void Awake(){
        if(instance) Destroy(this);
        else instance = this;
        managerInstance = GameManger.instance; 
    }

    void Update(){
        menuAwake = managerInstance.gamePaused;
    }

    void Start(){ 
        managerInstance = GameManger.instance; 
    }

    public void Menu(string menuName,string action){
        if(menuName == "End"){
            if(action == "open") OpenMenu(endGame); 
            else CloseMenu(endGame);
        }
        else if(menuName == "Pause"){
            if(action == "open") OpenMenu(pauseGame);
            else CloseMenu(pauseGame);
        }else{
            if(action == "open") OpenMenu(infoGame);
            else CloseMenu(infoGame);
        }
    }

    public void OpenMenu(GameObject obj){
        LeanTween.moveY(obj.GetComponent<RectTransform>(), 5f, .3f);
        LeanTween.scale(obj.GetComponent<RectTransform>(), obj.GetComponent<RectTransform>().localScale*3f, .6f).setEaseOutBack();
    }

    public void CloseMenu(GameObject obj){
        LeanTween.moveY(obj.GetComponent<RectTransform>(), -7.13f, .5f);    
        LeanTween.scale(obj.GetComponent<RectTransform>(),new Vector3(0,0,0), .2f);
        LeanTween.scale(obj.GetComponent<RectTransform>(),new Vector3(0.004f,0.004f,0.004f), .1f).setDelay(.3f);
    }

    public void OpenInfoMenu(){
        if(menuAwake == false && infoMenuAwake == false){
            OpenMenu(infoGame);
            managerInstance.GamePaused();
            infoMenuAwake = true;
        }else{
            if(infoMenuAwake == true){
                CloseMenu(infoGame);
                managerInstance.GameUnPaused();
                infoMenuAwake = false;
            }
        }        
    }


}
