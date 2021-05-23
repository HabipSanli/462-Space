using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame()
    {
        SceneManager.LoadScene("level1");
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void rules()
    {
        SceneManager.LoadScene("rules");
    }
    public void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
