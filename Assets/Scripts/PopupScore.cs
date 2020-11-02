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
        // PopTheScore(1f);
    }

    public void PopTheScore(float x ){
        popscoreText.text = "X"+(int)x;
        LeanTween.moveY(gameObject.GetComponent<RectTransform>(), 4.5f, .5f).setEaseOutCirc();
        LeanTween.scale(gameObject.GetComponent<RectTransform>(), gameObject.GetComponent<RectTransform>().localScale*x, .5f)
                    .setOnComplete(DestroyMe);
    }

    public void TestFunction(){
        Debug.Log("Test function called!");
    }

    private void DestroyMe(){
        Destroy(gameObject);
    }
}
