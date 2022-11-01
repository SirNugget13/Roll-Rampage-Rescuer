using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderManagerMainMenu : MonoBehaviour
{
    public GameObject Boulder;
    public Transform BoulderSpawnPos;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Boulder")
        {
            Instantiate(Boulder, BoulderSpawnPos.position, Quaternion.identity);
        }

        Destroy(collision.gameObject);
    }
}
