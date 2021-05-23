using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.name.Equals("player") && !other.gameObject.name.Contains("laser") && !other.gameObject.name.Contains("ealth") && !other.gameObject.name.Contains("firepower"))
        {
            if (!other.gameObject.name.Contains("ollid"))
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
            Destroy(this.gameObject);
        }
    }
}
