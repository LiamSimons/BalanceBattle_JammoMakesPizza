using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryLog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameData.completedPizzas = 0;
            GameData.angryLogHit = true;
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
