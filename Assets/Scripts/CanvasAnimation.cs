using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnimation : MonoBehaviour
{
   
    void Start()
    {
        LeanTween.moveY(gameObject.GetComponent<RectTransform>(), 5f, .5f).setEaseOutCirc();
        LeanTween.scale(gameObject.GetComponent<RectTransform>(), gameObject.GetComponent<RectTransform>().localScale*3f, .5f);
    }

    public void CloseMenu(){
        LeanTween.moveY(gameObject.GetComponent<RectTransform>(), -3.6721f, .5f).setEaseOutCirc();    
        LeanTween.scale(gameObject.GetComponent<RectTransform>(), new Vector3(0.04f,0.04f,0.04f), .5f);
    }
}
