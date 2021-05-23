using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        movePowerUp();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(this.gameObject.name.Contains("fire"))
                other.gameObject.GetComponent<Player>().firepowerUp();
            else
            {
                other.gameObject.GetComponent<Player>().healthUp();
            }
            Destroy(this.gameObject);
        }
    }
    private void movePowerUp()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - (Time.deltaTime * 1f), 0);
    }
}
