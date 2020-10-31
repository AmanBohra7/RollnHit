using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupScore : MonoBehaviour
{
    
    public TextMeshProUGUI popscoreText;

    private GameManger gameManagerInstance;

    void Start()
    {
        gameManagerInstance = GameManger.instance;
        Debug.Log("Heres the popup!");
        PopTheScore();
    }

    private void PopTheScore(){
        popscoreText.text = "X"+gameManagerInstance.GetStreak();
        LeanTween.moveY(gameObject.GetComponent<RectTransform>(), 4.5f, .5f).setEaseOutCirc();
        LeanTween.scale(gameObject.GetComponent<RectTransform>(), gameObject.GetComponent<RectTransform>().localScale*1.5f, .5f)
                    .setOnComplete(DestroyMe);
    }

    private void DestroyMe(){
        Destroy(gameObject);
    }
}
