using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI totalScore;
    private int score;

    private AudioSource audioSource;

    public void UpdateScore(int score,int timeLeft){
        this.score = score;
        StartCoroutine(updateWithLeftTime(timeLeft));
    }

    IEnumerator updateWithLeftTime(int time){
        while(time > 0){
            yield return new WaitForSeconds(.2f);
            animateScore();
            score += 1;
            time -= 1;
            audioSource.Play();
        }

    }

    void Start() { 
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void animateScore(){
        LeanTween.scale(totalScore.GetComponent<RectTransform>(), totalScore.GetComponent<RectTransform>().localScale*1.5f, .1f);
        LeanTween.scale(totalScore.GetComponent<RectTransform>(), new Vector3(1,1,1), .1f);
    }

    void Update(){
        totalScore.text = score.ToString();
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void RestartGame(){
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
