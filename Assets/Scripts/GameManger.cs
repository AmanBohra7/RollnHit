using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManger : MonoBehaviour
{

    public static GameManger instance;

    public GameObject popupScorePrefab;
    public GameObject endgameObject;

    public float timeLeft = 60.0f;

    private bool gameOver = false;

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
    }

    void Update(){
        if(!gameOver) timeLeft -= Time.deltaTime;
        if(timeLeft <= 0){
             GameOver();
         }
        if(!gameOver)   counterPro.text = ((int)timeLeft).ToString();
        scorePro.text = score.ToString();
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
        Instantiate(popupScorePrefab,trans.position,popupScorePrefab.transform.rotation);
    }

    private void GameOver(){
        Debug.Log("GameOver");
        endgameObject.SetActive(true);
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
