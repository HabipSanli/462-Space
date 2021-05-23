using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class scoreShow : MonoBehaviour
{
    private GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        canvas.GetComponentsInChildren<Text>()[0].text = "SCORE \n" + PlayerPrefs.GetInt("scorePlayer");
        //canvas.GetComponentInChildren<Text>().text = "SCORE \n" + PlayerPrefs.GetInt("scorePlayer");
        Invoke("mainMenu", 5);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
