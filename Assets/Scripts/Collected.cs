using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collected : MonoBehaviour
{
    private GameManger mangerInstance;

    public ParticleSystem blueBreak;
    public ParticleSystem redBreak; 

    void Start(){
        mangerInstance = GameManger.instance;
    }
  
    private void OnCollisionEnter(Collision other){
        if(gameObject.tag == "RED"){
            Instantiate(redBreak,
                new Vector3(
                    gameObject.transform.position.x,
                    gameObject.transform.position.y + 1f,
                    gameObject.transform.position.z
                ),
                redBreak.transform.rotation);
            mangerInstance.AddScore(15 , "RED"); 
        }else{
            Instantiate(blueBreak,
                new Vector3(
                    gameObject.transform.position.x,
                    gameObject.transform.position.y + 1f,
                    gameObject.transform.position.z
                )
                ,blueBreak.transform.rotation);
            mangerInstance.AddScore(20 , "BLUE");
        }
        mangerInstance.InstantiatePopupScore(gameObject.transform);
        Destroy(gameObject);
    }

}
