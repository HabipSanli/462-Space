using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class scriptKAZ : MonoBehaviour
{
    [SerializeField] private int lives = 100;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float coef = 3f;
    private float minX, maxX;
    private float direction;
    private float valY;
    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject pulse;
    private Vector2[] pulseVal;
    private GameObject source;
    
    // Start is called before the first frame update
    void Start()
    {
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
        pulseVal = new Vector2[8];
        pulseVal[0] = new Vector2(-2.5f, 0f);
        pulseVal[1] = new Vector2(-2.25f, -1.08f);
        pulseVal[2] = new Vector2(-1.56f, -1.95f);
        pulseVal[3] = new Vector2(-0.55f, -2.43f);
        pulseVal[4] = new Vector2(0.55f, -2.43f);
        pulseVal[5] = new Vector2(1.56f, -1.95f);
        pulseVal[6] = new Vector2(2.25f, -1.08f);
        pulseVal[7] = new Vector2(2.5f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        moveEnemy();
        fireEnemy();
    }
    private void fireEnemy()
    {
        if (Random.Range(0, 500) > 497)
        {
            GameObject fire1 = Instantiate(laser, transform.position, Quaternion.identity);
            GameObject fire2 = Instantiate(laser, transform.position, Quaternion.identity);
            GameObject fire3 = Instantiate(laser, transform.position, Quaternion.identity);
            fire1.transform.Rotate(new Vector3(1,0,0),180f);
            fire2.transform.Rotate(new Vector3(1,0,0),180f);
            fire3.transform.Rotate(new Vector3(1,0,0),180f);
            fire1.GetComponent<Rigidbody2D>().velocity = new Vector2(0.3f, -3.5f);
            fire2.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.3f, -3.5f);
            fire3.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -3.5f);
        }

        if (Random.Range(0f, 500f) >= 499f)
        {
            GameObject[] power = new GameObject[8];
            for (int i = 0; i < 8; i++)
            {
                power[i] = Instantiate(pulse, transform.position, Quaternion.identity);
                //power[i].transform.Rotate(new Vector3(1,0,0), -1f * (90f + (i * 25.71f)));
                power[i].GetComponent<Rigidbody2D>().velocity = pulseVal[i];
            }
        }
    }
    private void moveEnemy()
    {
        if (transform.position.x + (direction * coef  * Time.deltaTime) <= minX || transform.position.x + (direction * coef * Time.deltaTime) >= maxX)
        {
            direction *= -1f;
            transform.position = new Vector3(transform.position.x + (direction * coef * Time.deltaTime),
                transform.position.y, 0);
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
    private void calcScore()
    {
        PlayerPrefs.SetInt("scorePlayer", PlayerPrefs.GetInt("scorePlayer") + (PlayerPrefs.GetInt("livesPlayer") * 15));
    }
    public void collisionUpdate()
    {
        this.lives--;
        Debug.Log((lives));
        if (this.lives < 1)
        {
            source.GetComponent<sounds>().playDestroyed();
            calcScore();
            Destroy(this.gameObject);
        }

        if (this.lives % 35 == 0 && lives != 0)
        {
            GameObject enemy1 = Instantiate(enemyPrefab, new Vector3(-2.62f, 3.3f, 0), Quaternion.identity);
            GameObject enemy2 = Instantiate(enemyPrefab, new Vector3(-0f, 3.3f, 0), Quaternion.identity);
            GameObject enemy3 = Instantiate(enemyPrefab, new Vector3(2.62f, 3.3f, 0), Quaternion.identity);
        }
    }
    
    
}
