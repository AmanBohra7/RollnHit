using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBallController : MonoBehaviour
{
    private Rigidbody rb ;
    public float rollingSpeed = 1.8f;
    public bool gamePaused = false;
    public Joystick joystick;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate(){

        // Vector3 moveForwad = new Vector3(-rollingSpeed,0.0f,0.0f);
        // rb.AddForce(moveForwad);

        if(Application.platform == RuntimePlatform.WindowsEditor){
            Debug.Log("Widows Build!");
        }

        if(Application.platform == RuntimePlatform.Android){
            Debug.Log("Android BUID");
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(-moveVertical,0.0f, moveHorizontal);
        rb.AddForce(movement * 10);

    }

}
