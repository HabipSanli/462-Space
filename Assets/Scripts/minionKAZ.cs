using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minionKAZ : MonoBehaviour
{
    [SerializeField] private int lives = 10000;
    [SerializeField] private float coef = 2.5f;
    private float minX, maxX;
    private float direction;
    private float valY;
    [SerializeField]private GameObject laser;
    
    // Start is called before the first frame update
    void Start()
    {
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

        Invoke("destroySelf", 5);
    }

    // Update is called once per frame
    void Update()
    {
        moveEnemy();
        fireEnemy();
    }
    private void fireEnemy()
    {
        if (Random.Range(0, 500) > 498)
        {
            GameObject fire1 = Instantiate(laser, transform.position, Quaternion.identity);
            GameObject fire2 = Instantiate(laser, transform.position, Quaternion.identity);
            fire1.transform.Rotate(new Vector3(1,0,0),180f);
            fire2.transform.Rotate(new Vector3(1,0,0),180f);
            fire1.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.3f, -2.5f);
            fire2.GetComponent<Rigidbody2D>().velocity = new Vector2(0.3f, -2.5f);
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

    void destroySelf()
    {
        Destroy(this.gameObject);
    }
    
    public void collisionUpdate()
    {
        lives--;
        if(lives < 1)
            Destroy(this.gameObject);
    }
}
