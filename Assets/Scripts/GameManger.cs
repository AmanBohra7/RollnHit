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

    public bool gameOver = false;

    private int streak  = 1;
    private int score = 0;

    public TextMeshProUGUI scorePro;
    public TextMeshProUGUI counterPro;

    private int countCollection = 0;
    private string lastHit = "  ";
    
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
    }

    void Update(){
        if(!gameOver) timeLeft -= Time.deltaTime;
        if(timeLeft <= 0 && gameOver == false ){
             GameOver();
         }
        if(!gameOver)   counterPro.text = ((int)timeLeft).ToString();
        scorePro.text = score.ToString();
    }

    public void GamePaused(){ 
        audioSource.Pause();
    }

    public void GameUnPaused(){
        audioSource.Play();
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

    public void MuteGame() { audioSource.volume = 0f;}

    public void UnmuteGame() { audioSource.volume = 1f; }

    public void InstantiatePopupScore(Transform trans){
        GameObject instObject = Instantiate(popupStreak,trans.position,popupStreak.transform.rotation);
        instObject.GetComponent<PopupScore>().PopTheScore((float)streak);
    }

    private void GameOver(){
        endgameObject.GetComponent<EndGame>().UpdateScore(score,(int)timeLeft);
        rollingSphere.SetActive(false);
        StartCoroutine(decrementPower());
        gameOver = true;
        menuInstance.TriggerEndMenu();
    }

    IEnumerator decrementPower(){
        while(timeLeft > 0){
            yield return new WaitForSeconds(.17f);
            timeLeft -= 1;
            counterPro.text = ((int)timeLeft).ToString();
        }
    }

}
