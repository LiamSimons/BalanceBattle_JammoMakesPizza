using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField]
    GameObject prefabPizza;

    int children = 1;
    Quaternion startRotation;
    public static float rotationBarFromHelmet = 0.0f;
    BoxCollider box_Collider;
    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
        box_Collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.angryLogHit)
        {
            foreach(Transform child in transform)
            {
                if(child.tag == "Ingredient")
                {
                    Destroy(child.gameObject);
                }
                else if(child.tag == "Pizza")
                {
                    Destroy(child.gameObject);
                }
            }
            Vector3 newBoxSize = box_Collider.size;
            newBoxSize.y = newBoxSize.y + 0.001f * GameData.completedPizzas;
            box_Collider.size = newBoxSize;
            GameData.angryLogHit = false;
        }
        // check if all ingredients of the pizza are there to complete one
        if (GameData.completePizza == true)
        {
            //Debug.Log("COMPLETE PIZZA");
            foreach (Transform child in transform)
            {
                //Debug.Log("Child tag: " + child.tag);
                if (child.tag == "Ingredient")
                {
                    Destroy(child.gameObject) ;
                }
            }
            GameData.completePizza = false;
            // instantiate complete pizza
            Debug.Log("Making new Pizza");
            GameObject completedPizza = Instantiate(prefabPizza);
            Debug.Log("Pizza instantiated: " + completedPizza);
            Vector3 newRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
            Vector3 stackPosition = new Vector3(0, -0.003f*GameData.completedPizzas, 0);
            completedPizza.transform.parent = gameObject.transform;
            completedPizza.transform.localPosition = stackPosition;
            completedPizza.transform.eulerAngles = newRotation;

            Vector3 newBoxSize = box_Collider.size;
            newBoxSize.y = newBoxSize.y + 0.001f * GameData.completedPizzas;
            box_Collider.size = newBoxSize;
        }
        if (transform.childCount != children)
        {
            //Debug.Log(transform.childCount);
            //Quaternion rotationBar = transform.rotation;
            //Debug.Log("CHILDREN" + transform.childCount);
            int amountLeft = 0;
            int amountRight = 0;
            foreach (Transform child in transform)
            {
                if (!(child.gameObject.tag == "Pizza"))
                {
                    if (child.gameObject.transform.position.x < transform.position.x)
                    {
                        //Debug.Log("LEFT");
                        //rotationBar.z -= 20 * 2 * Mathf.PI / 180;
                        //transform.Rotate(0, 0, 20);
                        amountLeft++;
                    }
                    else if (child.gameObject.transform.position.x == transform.position.x)
                    {
                        //Debug.Log("MIDDLE");
                    }
                    else
                    {
                        //Debug.Log("RIGHT");
                        //rotationBar.z += 20;
                        amountRight++;
                    }
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
            //Debug.Log("rotationBarFromHelm: " + rotationBarFromHelmet);
            // transform.rotation = oldRotation;//Quaternion.Euler(0, 0, 10*rotationBarFromHelm 
        }
    }
}
