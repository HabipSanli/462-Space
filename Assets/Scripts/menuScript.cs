using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuScript : MonoBehaviour
{
    public int score = 0;
    [SerializeField] private int startingLives = 3;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        PlayerPrefs.SetInt("scorePlayer", score);
        PlayerPrefs.SetInt("livesPlayer", startingLives);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
