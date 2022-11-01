using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public AudioSource FluffingADuck;
    public AudioSource FluffingAGoose;

    private int rand;

    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(1, 21);
        if(rand == 1)
        {
            FluffingAGoose.Play();     
        }
        else
        {
            FluffingADuck.Play();
        }
    }

    /*

    // Update is called once per frame
    void Update()
    {
        if(!FadeOut)
        {
            if (duck)
            {
                FluffingADuck.volume = Mathf.Lerp(FluffingADuck.volume, 1, 2 * Time.deltaTime);
            }
            else
            {
                FluffingAGoose.volume = Mathf.Lerp(FluffingAGoose.volume, 1, 2 * Time.deltaTime);
            }
        }
        else
        {
            if (duck)
            {
                FluffingADuck.volume = Mathf.Lerp(FluffingADuck.volume, 0, 1 * Time.deltaTime);
            }
            else
            {
                FluffingAGoose.volume = Mathf.Lerp(FluffingAGoose.volume, 0, 1 * Time.deltaTime);
            }
        }
    }
    */
}
