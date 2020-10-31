using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpwan : MonoBehaviour
{
    public GameObject gameControllerObject;

    public GameObject spwanArea;

    public GameObject blueCube;
    public GameObject redCube;

    private Vector3 center;
    private Vector3 size;
    private Vector3 lstPose;

    private List<Transform> houses = new List<Transform>();
    private bool isGood = true;
    private int spwanCount = 0;
    private int spwanAmount = 10;
    private float distance = 5.0f;
    private int maxTry = 100;
    private int currentCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        // center = gameControllerObject.transform.position;
        center = spwanArea.transform.position;
        Debug.Log("Size: "+spwanArea.GetComponent<Collider>().bounds.size.x);
        // size   = new Vector3(5.5f,0.0f,8f);
        size = new Vector3(
            spwanArea.GetComponent<Collider>().bounds.size.x / 2,
            0.0f,
            spwanArea.GetComponent<Collider>().bounds.size.z / 2
        );
        lstPose = new Vector3(0f,spwanArea.transform.position.y,0f);
        SpwanObjects();
    }

    void SpwanObjects(){
        while(spwanCount < spwanAmount && currentCount < maxTry ){
            float rdm_x = Random.Range(center.x-size.x  , center.x+size.x );
            float rdm_z = Random.Range(center.z-size.z  , center.z+size.z );
            //Debug.Log(rdm_x);
            Vector3 newPose = new Vector3(rdm_x,center.y,rdm_z);
            currentCount += 1;
            foreach(Transform i in houses){
                //Debug.Log("Try: "+currentCount+" distance: "+Mathf.Abs(i.position.x - newPose.x));
                if( Mathf.Abs(i.position.x - newPose.x) < 2.0f && Mathf.Abs(i.position.z - newPose.z) < 2.0f ){
                    isGood = false;
                    currentCount += 1;
                    break;
                }else isGood = true;
            }

            if(isGood){
                spwanCount += 1;
                currentCount += 1;
                GameObject newObject;
                if(spwanCount <= 5){
                    newObject = Instantiate(redCube,newPose,redCube.transform.rotation);
                }
                else{
                    newObject = Instantiate(blueCube,newPose,blueCube.transform.rotation);
                }

                houses.Add(newObject.transform);
            }

        }
    }
}


/*
Vector3 pos = center + new Vector3(Random.Range(-size.x/2,size.x),
                                                Random.Range(-size.y/2,size.y),
                                                Random.Range(-size.z/2,size.z));

        if(Vector3.Distance(pos,lstPose) < 5) return;

        lstPose = pos;

        Instantiate(obj,pos,Quaternion.identity);
*/