using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManger : MonoBehaviour
{

    public static GameManger instance;

    private MenuController menuInstance;

    public GameObject popupStreak;
    public GameObject endgameObject;

    public float timeLeft = 61.0f;

    private bool gameOver = false;

    private int streak  = 1;
    private int score = 0;

    public TextMeshProUGUI scorePro;
    public TextMeshProUGUI counterPro;

    private int countCollection = 0;

    private string lastHit = "  ";
    
    public bool gamePaused = false;

    private GameObject rollingSphere;
    private AudioSource audioSource;

    void Awake(){
        if(instance) Destroy(this);
        else instance = this;
    }

    void Start(){  
        rollingSphere = GameObject.FindWithTag("Player"); 
        audioSource = gameObject.GetComponent<AudioSource>(); 
        menuInstance = MenuController.instance;
        menuInstance.OpenInfoMenu();
    }

    void Update(){
        
        if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == false){
            menuInstance.Menu("Pause","open");
            StartCoroutine(waitAndPause());
            audioSource.Pause();
            gamePaused = true;
        }else if(Input.GetKeyDown(KeyCode.Escape) && gamePaused == true){
            menuInstance.Menu("Pause","close");
            Time.timeScale = 1f;
            gamePaused = false;
            audioSource.Play();
        }

        if(!gameOver) timeLeft -= Time.deltaTime;
        if(timeLeft <= 0 && gameOver == false ){
             GameOver();
         }
        if(!gameOver)   counterPro.text = ((int)timeLeft).ToString();
        scorePro.text = score.ToString();
    }

    IEnumerator waitAndPause()
    {
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 0f;
    }

    public void GamePaused(){ 
        audioSource.Pause();
        StartCoroutine(waitAndPause());
        gamePaused = true;
    }

    public void GameUnPaused(){
        audioSource.Play();
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public string GetStreak(){ return streak.ToString(); }

    public void AddScore(int point , string color){
        if( lastHit == color ){
            streak += 1;
            
        }else{
            streak = 1;
            lastHit = color;
        }
        score += streak * point;
        IncrementCountCollection();
    }

    public void IncrementCountCollection(){ 
        countCollection += 1; 
        timeLeft -= streak;
        if(countCollection == 10){
            GameOver();
        }
    }

    public void InstantiatePopupScore(Transform trans){
        Instantiate(popupStreak,trans.position,popupStreak.transform.rotation);
    }

    private void GameOver(){
        Debug.Log("GameOver");
        menuInstance.Menu("End","open");
        endgameObject.GetComponent<EndGame>().UpdateScore(score,(int)timeLeft);
        rollingSphere.SetActive(false);
        StartCoroutine(decrementPower());
        gameOver = true;
    }

    IEnumerator decrementPower(){
        while(timeLeft > 0){
            yield return new WaitForSeconds(.2f);
            timeLeft -= 1;
            counterPro.text = ((int)timeLeft).ToString();
        }
    }

}
