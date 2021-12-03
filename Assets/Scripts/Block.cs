using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Block : MonoBehaviour
{
    List<ContactPoint> pointx = new List<ContactPoint>();
    private bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (!HUD.playing) Destroy(gameObject);
        else
        {
            Vector3 cheatZ = transform.position;
            cheatZ.z = 0;
            transform.position = cheatZ;
        // Quaternion cheatRot = transform.rotation;
        // cheatRot.x = 45;
        // cheatRot.y = 45;
        // transform.rotation = cheatRot;
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public float FindMaxValue<T>(List<T> list, Converter<T, float> projection)
    {
        if (list.Count == 0)
        {
            throw new InvalidOperationException("Empty list");
        }
        float maxValue = float.MinValue;
        foreach (T item in list)
        {
            float value = projection(item);
            if (value > maxValue)
            {
                maxValue = value;
            }
        }
        return maxValue;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
            HUD.loseHeart();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!finished)
        {
            float positionX = transform.position.x;
            float rotationZ = transform.rotation.z;
            float sizeX = GetComponent<Collider>().bounds.size.x;
            bool leftHit = false;
            bool rightHit = false;
            float leftX = 0;
            float rightX = 0;
            float avX;
            foreach (ContactPoint contact in collision.contacts)
            {
                //Debug.Log(contact.thisCollider.name + " hit " + contact.otherCollider.name +
                //    " at [x = " + contact.point.x + "; y = " + contact.point.y + "; z = " + contact.point.z + "]");
                if (contact.point.x < (positionX - 0.3 * sizeX * Mathf.Cos(rotationZ)))
                {
                    leftHit = true;
                    leftX = contact.point.x;
                }
                else if (contact.point.x > (positionX + 0.3 * sizeX * Mathf.Cos(rotationZ)))
                {
                    rightHit = true;
                    rightX = contact.point.x;
                }
            }
            avX = (leftX + rightX) / 2;
            if (leftHit && rightHit)
            {
                finished = true;
                float oldRotation = collision.transform.rotation.z;
                float localBlockY = transform.position.y - collision.transform.position.y;
                
                Quaternion temp = transform.rotation;
                transform.parent = collision.gameObject.transform;
                //Debug.Log(transform.localToWorldMatrix.ToString());
                Destroy(GetComponent<Rigidbody>());
                HUD.addScoreText();
            }
        }
    }
}
