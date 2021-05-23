using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class controller : MonoBehaviour
{
    private GameObject canvas;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        index = SceneManager.GetActiveScene().buildIndex;
        canvas = GameObject.Find("Canvas");
        canvas.GetComponentsInChildren<Text>()[0].text = "Lives : " + (PlayerPrefs.GetInt("livesPlayer")).ToString();
        canvas.GetComponentsInChildren<Text>()[1].text = "Score : " + (PlayerPrefs.GetInt("scorePlayer")).ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
         {
             GameObject[] enemies;
             enemies = GameObject.FindGameObjectsWithTag("Finish");
             if (enemies.Length < 1)
             {
                 SceneManager.LoadScene(index + 1);
             }
         }
}
