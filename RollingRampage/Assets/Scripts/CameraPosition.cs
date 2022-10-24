using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraPosition : MonoBehaviour
{
    public bool Level5 = false;

    public GameObject Boulder;
    public Transform EndPoint;
    public float TransitionPos;
    public float MoveFromBoulderSpeed;
    public float MoveToBoulderSpeed;
    public float CamFinalSize;
    public float CamSizeTime;
    public float BoulderCamSize = 12;

    private Camera cam;
    private bool followFollowBoulder;

    public bool followBoulder = false;
    private Vector3 placeholder;

    private void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(followBoulder)
        {
            placeholder = Boulder.transform.position;
            placeholder.z = -10;

            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, BoulderCamSize, CamSizeTime * Time.deltaTime);
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, placeholder, MoveToBoulderSpeed * Time.deltaTime);

            this.Wait(1.7f, () =>
            {
                followFollowBoulder = true;
            });

            if(followFollowBoulder)
            {
                gameObject.transform.position = placeholder;
            }

            if (Boulder.gameObject.transform.position.x > TransitionPos && !Level5)
            {
                followBoulder = false;
                followFollowBoulder = false;
            }
            
            if(Level5 && Boulder.gameObject.transform.position.y < TransitionPos)
            {
                followBoulder = false;
                followFollowBoulder = false;
            }
        }
        else
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, CamFinalSize, CamSizeTime * Time.deltaTime);
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, EndPoint.position, MoveFromBoulderSpeed * Time.deltaTime);
        }
    }
}
