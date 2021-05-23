using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] private float coefSpeed = 9f;
    [SerializeField] private float boundaryBuffer = 1.7f;
    [SerializeField] private float speedLaser = 8f;
    [SerializeField] private GameObject prefabLaser;
    private float minX, minY, maxX, maxY;
    private Coroutine coRoutine;
    private bool power, flag;
    private GameObject canvas;
    [SerializeField] private AudioClip[] aud = new AudioClip[2];
    private GameObject source;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        setFrameBoundaries();
        power = false;
        flag = false;
        source = GameObject.Find("audioS");
    }
    // Update is called once per frame
    void Update()
    {
        movePlayer();
        fireLaser();
    }
    private void movePlayer()
    {
        var valueX = Mathf.Clamp(transform.position.x, minX, maxX) + (Input.GetAxis("Horizontal") * Time.deltaTime * coefSpeed);
        var valueY = Mathf.Clamp(transform.position.y, minY, maxY) + (Input.GetAxis("Vertical") * Time.deltaTime * coefSpeed);
        transform.position = new Vector2(valueX, valueY);
    }
    private void fireLaser()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            flag = true;
            if (!power)
                coRoutine = StartCoroutine(rapidFire());
            else
                coRoutine = StartCoroutine(rapidFire2());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            flag = false;
            StopCoroutine(coRoutine);
        }
    }
    IEnumerator rapidFire()
    {
        while (true)
        {
            GameObject laser1 = Instantiate(prefabLaser, new Vector3(transform.position.x, transform.position.y + 0.6f, 0), Quaternion.identity);
            laser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speedLaser);
            source.GetComponent<sounds>().playLaserSound(0);
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }
    IEnumerator rapidFire2()
    {
        while (true)
        {
            GameObject laser1 = Instantiate(prefabLaser, new Vector3(transform.position.x - 0.3f, transform.position.y + 0.6f, 0), Quaternion.identity);
            GameObject laser2 = Instantiate(prefabLaser, new Vector3(transform.position.x + 0.3f, transform.position.y + 0.6f, 0), Quaternion.identity);
            laser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speedLaser);
            laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speedLaser);
            source.GetComponent<sounds>().playLaserSound(1);
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }
    private void setFrameBoundaries()
    {
        Camera mainCamera = Camera.main;
        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + boundaryBuffer;
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - boundaryBuffer;
        minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + boundaryBuffer;
        maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - boundaryBuffer;
    }
    public void firepowerUp()
    {
        if (power)
        {
            if (flag)
            {
                Invoke("resetRate", 5);
            }
            else
            {
                StopAllCoroutines();
                fireLaser();
                Invoke("resetRate", 5);
            }
        }
        else
        {
            power = true;
            if (flag)
            {
                StopAllCoroutines();
                coRoutine = StartCoroutine(rapidFire2());
                Invoke("resetRate", 5);
            }
            else
            {
                StopAllCoroutines();
                fireLaser();
                Invoke("resetRate", 5);
            }
        }
    }
    void resetRate()
    {
        power = false;
        StopAllCoroutines();
        if (flag)
            coRoutine = StartCoroutine(rapidFire());
    }
    public void healthUp()
    {
        PlayerPrefs.SetInt("livesPlayer", PlayerPrefs.GetInt("livesPlayer") + 1);
        canvas.GetComponentInChildren<Text>().text = "Lives : " + PlayerPrefs.GetInt("livesPlayer").ToString();
    }
    private void decreaseScore()
    {
        int score = PlayerPrefs.GetInt("scorePlayer");
        if (score <= 3)
        {
            score = 0;
            PlayerPrefs.SetInt("scorePlayer", score);
            canvas.GetComponentsInChildren<Text>()[1].text = "Score : " + PlayerPrefs.GetInt("scorePlayer");
        }
        else
        {
            score -= 3;
            PlayerPrefs.SetInt("scorePlayer", score);
            canvas.GetComponentsInChildren<Text>()[1].text = "Score : " + PlayerPrefs.GetInt("scorePlayer");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            if(other.gameObject.name.Contains("laser"))
                Destroy(other.gameObject);
            else
            {
                String s = other.gameObject.name;
                if (s.Contains("enemyE"))
                {
                    other.gameObject.GetComponent<enemyScriptE>().collisionUpdate();
                }
                else
                {
                    if (s.Contains("enemy2"))
                    {
                        other.gameObject.GetComponent<enemyScript2>().collisionUpdate();
                    }
                    else
                    {
                        if (s.Contains("enemyS"))
                        {
                            other.gameObject.GetComponent<enemyScriptS>().collisionUpdate();
                        }
                        else
                        {
                            if (s.Contains("enemyB"))
                            {
                                other.gameObject.GetComponent<enemyScriptB>().collisionUpdate();
                            }
                            else
                            {
                                if (s.Contains("Boss"))
                                {
                                    other.gameObject.GetComponent<scriptKAZ>().collisionUpdate();
                                }
                            }
                        }
                    }
                }
            }
            PlayerPrefs.SetInt("livesPlayer", PlayerPrefs.GetInt("livesPlayer") - 1);
            int liveP = PlayerPrefs.GetInt("livesPlayer");
            canvas.GetComponentsInChildren<Text>()[0].text = "Lives : " + liveP.ToString();
            decreaseScore();
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Finish"))
            {
                if(obj.gameObject.name.Contains("laser") && obj.gameObject.transform.position.y < -5)
                    Destroy(obj.gameObject);
            }
            if (liveP < 1)
            {
                SceneManager.LoadScene("GameOver");
            }
            source.GetComponent<sounds>().playPlayerDestroyed();
            transform.position = new Vector3(0, -8, -1);
        }
    }       
}
