using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private CanvasGroup GameOverGroup;
    
    public Man Man;
    public float FadeTime = 1;
    //public GameObject MainMenuButton;

    private bool GOver = false;

    private void Start()
    {
        GameOverGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Man.IsDead)
        {
            GOver = true;
            GameOverGroup.gameObject.SetActive(true);
        }

        if(GOver)
        {
            GameOverGroup.alpha = Mathf.Lerp(GameOverGroup.alpha, 1, FadeTime * Time.deltaTime);
            //MainMenuButton.SetActive(true);
        }
    }
}
