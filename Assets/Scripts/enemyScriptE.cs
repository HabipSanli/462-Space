using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class enemyScriptE : MonoBehaviour
{
    private int lives = 2;
    [SerializeField] private float coef = 2.3f;
    private float minX, maxX;
    private float direction;
    private float valY;
    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject power1;
    [SerializeField] private GameObject power2;
    private int trigger;
    private GameObject canvas;
    private GameObject source;
    private bool isDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        isDestroyed = false;
        source = GameObject.Find("audioS");
        setFrameBoundaries();
        direction = Random.Range(0f, 3f);
        if (direction > 1.5f)
        {
            direction = 1f;
        }
        else
        {
            direction = -1f;
        }
        trigger = 497;
        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDestroyed)
        {
            moveEnemy();
            fireEnemy();
        }
    }
    private void fireEnemy()
    {
        if (Random.Range(0, 500) > trigger)
        {
            GameObject fire = Instantiate(laser, transform.position, Quaternion.identity);
            fire.transform.Rotate(new Vector3(1,0,0),180f);
            fire.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -2.5f);
        }
    }
    private void moveEnemy()
    {
        if (transform.position.x + (direction * coef  * Time.deltaTime) <= minX || transform.position.x + (direction * coef * Time.deltaTime) >= maxX)
        {
            direction *= -1f;
            transform.position = new Vector3(transform.position.x + (direction * coef * Time.deltaTime),
                transform.position.y - 1f, 0);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + (direction * coef * Time.deltaTime), transform.position.y, 0);
        }
    }
    private void setFrameBoundaries()
    {
        Camera mainCamera = Camera.main;
        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + 2f;
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 2f;
    }
    private void addPoints()
    {
        PlayerPrefs.SetInt("scorePlayer", (PlayerPrefs.GetInt("scorePlayer") + 2));
        canvas.GetComponentsInChildren<Text>()[1].text = "Score : " + (PlayerPrefs.GetInt("scorePlayer")).ToString();
    }

    void destroySelf()
    {
        Destroy(this.gameObject);
    }
    public void collisionUpdate()
    {
        lives--;
        if (lives == 1)
            trigger--;
        if (lives < 1)
        {
            if (!isDestroyed)
            {
                source.GetComponent<sounds>().playDestroyed();
                float luck = Random.Range(0f, 10f);
                if (luck > 8.5f)
                {
                    if (luck > 9.5f)
                    {
                        GameObject k = Instantiate(power1, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        GameObject k = Instantiate(power2, transform.position, Quaternion.identity);
                    }
                }

                addPoints();
                isDestroyed = true;
                this.GetComponent<Animator>().SetBool("animatorDestroy", true);
                Invoke("destroySelf", 0.15f);
            }
        }
    }
}
