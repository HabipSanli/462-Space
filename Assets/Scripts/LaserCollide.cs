using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserCollide : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("enemy"))
        {
            Destroy(GameObject.Find("player"));
            SceneManager.LoadScene("GameOver");
        }
        Destroy(other.gameObject);
    }
}
