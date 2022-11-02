using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderManagerMainMenu : MonoBehaviour
{
    public GameObject Boulder;
    public GameObject BrickWall;
    public Transform BoulderSpawnPos;
    public Transform BrickSpawnPos1;
    public Transform BrickSpawnPos2;
    public Transform BrickSpawnPos3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Boulder")
        {
            Instantiate(Boulder, BoulderSpawnPos.position, Quaternion.identity);
            Instantiate(BrickWall, BrickSpawnPos1.position, Quaternion.identity);
            Instantiate(BrickWall, BrickSpawnPos2.position, Quaternion.identity);
            Instantiate(BrickWall, BrickSpawnPos3.position, Quaternion.identity);
        }

        Destroy(collision.gameObject);
    }
}
