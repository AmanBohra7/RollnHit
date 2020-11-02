using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupScore : MonoBehaviour
{
    
    public TextMeshProUGUI popscoreText;

    private GameManger gameManagerInstance;

    void Start(){
        gameManagerInstance = GameManger.instance;

    }

    public void PopTheScore(float x ){
        popscoreText.text = "X"+(int)x;
        LeanTween.moveY(gameObject.GetComponent<RectTransform>(), 4.5f, .5f).setEaseOutCirc();
        LeanTween.scale(gameObject.GetComponent<RectTransform>(), gameObject.GetComponent<RectTransform>().localScale*x, .5f)
                    .setOnComplete(DestroyMe);
    }
    
    private void DestroyMe(){
        Destroy(gameObject);
    }
}
