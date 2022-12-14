using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public GameObject[] ObjectsToPlace;
    public GameObject SelectedObject;

    public AudioClip SpringPlace;
    public AudioClip BrickPlace;
    public AudioClip MetalPlace;
    public AudioClip EraserSound;

    public AudioSource ASource;

    public bool GameStarted = false;

    public int BrickNum = 0;
    public int MetalNum = 0;
    public int SpringNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        SelectedObject = ObjectsToPlace[0];
    }

    public void PlaceObject()
    {
        if (GameStarted)
        {
            return;
        }
        
        if(SelectedObject != ObjectsToPlace[3])
        {
            if(SelectedObject == ObjectsToPlace[0] && BrickNum > 0)
            {
                ASource.PlayOneShot(BrickPlace);
                Instantiate(SelectedObject, Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 1)), Quaternion.identity);
                BrickNum--;
            }

            if (SelectedObject == ObjectsToPlace[1] && MetalNum > 0)
            {
                ASource.PlayOneShot(MetalPlace);
                Instantiate(SelectedObject, Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 1)), Quaternion.identity);
                MetalNum--;
            }

            if (SelectedObject == ObjectsToPlace[2] && SpringNum > 0)
            {
                ASource.PlayOneShot(SpringPlace);
                Instantiate(SelectedObject, Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 1)), Quaternion.identity);
                SpringNum--;
            }

            if(SelectedObject == ObjectsToPlace[4])
            {
                return;
            }
        }

        if (SelectedObject == ObjectsToPlace[3])
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if(hit.collider == null)
            {
                return;
            }
            
            if (hit.collider.name.Contains("Brick"))
            {
                ASource.PlayOneShot(EraserSound);
                Destroy(hit.collider.gameObject);
                BrickNum++;
            }
            else if(hit.collider.name.Contains("Metal"))
            {
                ASource.PlayOneShot(EraserSound);
                Destroy(hit.collider.gameObject);
                MetalNum++;
            }
            else if (hit.collider.name.Contains("Spring"))
            {
                ASource.PlayOneShot(EraserSound);
                Destroy(hit.collider.gameObject);
                SpringNum++;
            }
        }
    }
}
