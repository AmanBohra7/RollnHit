using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    public void OnStartPressed(){
        SceneManager.LoadScene("Main");       
    }

    public void OnQuitPressed(){
        Application.Quit();
    }
}
