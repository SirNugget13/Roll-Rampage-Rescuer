using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public GameObject selectedObject;
    public float RotationMultiplier = 5;
    Vector3 offset;
    public bool RotateObject = false;

    void Update()
    {
        if(RotateObject)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
                if (targetObject)
                {
                    //targetObject.GetComponent<Rigidbody2D>().freezeRotation = true;
                    selectedObject = targetObject.transform.gameObject;
                    offset = selectedObject.transform.position - mousePosition;

                    if (Input.GetKey("A"))
                    {
                        Debug.Log("Yo");
                        //targetObject.GetComponent<Rigidbody2D>().freezeRotation = false;
                        selectedObject.transform.Rotate(0, 0, selectedObject.transform.rotation.z - RotationMultiplier);
                        //targetObject.GetComponent<Rigidbody2D>().freezeRotation = true;
                    }

                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        //targetObject.GetComponent<Rigidbody2D>().freezeRotation = false;
                        selectedObject.transform.Rotate(0, 0, selectedObject.transform.rotation.z + RotationMultiplier);
                        //targetObject.GetComponent<Rigidbody2D>().freezeRotation = true;
                    }

                }
            }
            if (selectedObject)
            {
                selectedObject.transform.position = mousePosition + offset;
            }
            if (Input.GetMouseButtonUp(0) && selectedObject)
            {
                selectedObject.GetComponent<Rigidbody2D>().freezeRotation = false;
                selectedObject = null;
            }
        }
    }
}

