using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public GameObject selectedObject;
    public float RotationMultiplier = 6;
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
                
                if (targetObject != null && targetObject.gameObject.tag != "Man")
                {
                    targetObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                    targetObject.GetComponent<Rigidbody2D>().freezeRotation = true;
                    selectedObject = targetObject.transform.gameObject;
                    offset = selectedObject.transform.position - mousePosition;
                }

                if(targetObject != null && targetObject.gameObject.tag == "Spring")
                {
                    targetObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    targetObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                    targetObject.GetComponent<Rigidbody2D>().freezeRotation = true;
                    selectedObject = targetObject.transform.gameObject;
                    offset = selectedObject.transform.position - mousePosition;
                }
            }
            if (selectedObject)
            {
                selectedObject.transform.position = mousePosition + offset;
            }
            if (Input.GetMouseButtonUp(0) && selectedObject)
            {
                if(selectedObject.gameObject.tag == "Spring")
                {
                    selectedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                }
                
                selectedObject.GetComponent<Rigidbody2D>().freezeRotation = false;
                selectedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                selectedObject = null;
            }
        }

        if(selectedObject)
        {
            if(selectedObject.gameObject.tag != "Spring")
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    selectedObject.GetComponent<Rigidbody2D>().freezeRotation = false;
                    selectedObject.transform.Rotate(0, 0, selectedObject.transform.rotation.z - RotationMultiplier);
                    selectedObject.GetComponent<Rigidbody2D>().freezeRotation = true;
                }

                if (Input.GetKeyDown(KeyCode.A))
                {
                    selectedObject.GetComponent<Rigidbody2D>().freezeRotation = false;
                    selectedObject.transform.Rotate(0, 0, selectedObject.transform.rotation.z + RotationMultiplier);
                    selectedObject.GetComponent<Rigidbody2D>().freezeRotation = true;
                }
            }
        }
    }
}

