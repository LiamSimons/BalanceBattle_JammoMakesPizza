using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    int children = 1;
    Quaternion startRotation;
    public static float rotationBarFromHelmet = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount != children)
        {
            //Debug.Log(transform.childCount);
            //Quaternion rotationBar = transform.rotation;
            //Debug.Log("CHILDREN" + transform.childCount);
            int amountLeft = 0;
            int amountRight = 0;
            foreach (Transform child in transform)
            {
                if (child.gameObject.transform.position.x < transform.position.x)
                {
                    Debug.Log("LEFT");
                    //rotationBar.z -= 20 * 2 * Mathf.PI / 180;
                    //transform.Rotate(0, 0, 20);
                    amountLeft++;
                }
                else if(child.gameObject.transform.position.x == transform.position.x){
                    Debug.Log("MIDDLE");
                }
                else
                {
                    Debug.Log("RIGHT");
                    //rotationBar.z += 20;
                    amountRight++;
                }
            }
            children = transform.childCount;

            if(amountLeft == 0)
            {
                if(amountRight == 0)
                {
                    rotationBarFromHelmet = 0;
                }
                else
                {
                    rotationBarFromHelmet = -amountRight;
                }
            }
            else
            {
                if(amountRight == 0)
                {
                    rotationBarFromHelmet = amountLeft;
                }
                else
                {
                    rotationBarFromHelmet = (amountLeft / amountRight) - 1;
                }
            }
            // Quaternion oldRotation = transform.rotation;

            transform.rotation =  startRotation*Quaternion.Euler(Vector3.back * -10*rotationBarFromHelmet);

            // oldRotation.Set(oldRotation.x, oldRotation.y+10*rotationBarFromHelm*Mathf.PI / 180, oldRotation.z, oldRotation.w);

            // oldRotation.y += 10*rotationBarFromHelm * Mathf.PI / 180;
            Debug.Log("rotationBarFromHelm: " + rotationBarFromHelmet);
            // transform.rotation = oldRotation;//Quaternion.Euler(0, 0, 10*rotationBarFromHelm);
        }
    }
}
