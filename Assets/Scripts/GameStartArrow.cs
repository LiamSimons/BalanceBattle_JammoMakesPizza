using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartArrow : MonoBehaviour
{
    GameObject jammo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        jammo = GameObject.FindWithTag("Player");
        if (jammo.transform.position.x >= 0.3f)
        {
            HUD.StartGame();
            Destroy(gameObject);
        }
    }
}
