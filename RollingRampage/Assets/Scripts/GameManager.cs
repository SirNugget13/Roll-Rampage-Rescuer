using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public bool start = false;

    [SerializeField] private CanvasGroup WinScreenGroup;

    //Boulder Variables
    public Transform BoulderStartingPoint;
    public GameObject Boulder;
    private bool BoulderIsSpawned = false;
    private GameObject BoulderCast;

    //Camera Variables
    public float SetBoulderCamSize;
    public CameraPosition CamPos;
    public Transform FinalCamPos;
    public float FinalCamSize;
    
    //UI Element Variables
    public GameObject SpringNumText;
    public GameObject BrickNumText;
    public GameObject MetalNumText;
    private bool GameWinScreenUse = false;
    public GameObject MainMenuButton;
    public GameObject PlaceSpaceObj;

    public GameObject[] HighlightBoxes;
    public Transform HotBarOffScreen;
    public GameObject Hotbar;

    //Placing Object Variables
    public ObjectPlacer ObjectPlacer;
    private GameObject[] ObjectList;
    public ObjectRotator ObjectRotator;

    //Player
    public Man Man;
    private bool GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        CamPos.CamFinalSize = FinalCamSize;
        CamPos.EndPoint = FinalCamPos;

        HighlightBoxer(1);

        UpdateObjectNumbers();

        ObjectList = ObjectPlacer.ObjectsToPlace;
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            if (!BoulderIsSpawned)
            {
                SpawnBoulder();
                BoulderIsSpawned = true;
            }
        }

        if(GameWinScreenUse)
        {
            WinScreenGroup.alpha = Mathf.Lerp(WinScreenGroup.alpha, 1, 1 * Time.deltaTime);
        }

        if(!GameOver)
        {
            if(Man.IsDead)
            {
                GameOver = true;
            }
        }
    }

    public void StartGameWin()
    {
        this.Wait(20f, () =>
        {
            if(!GameOver)
            {
                GameWinScreenUse = true;
                WinScreenGroup.gameObject.SetActive(true);
                MainMenuButton.SetActive(true);
                Debug.Log("GAMEWIN");
            }
        });
    }

    public void SpawnBoulder()
    {
        BoulderCast = (GameObject)Instantiate(Boulder, BoulderStartingPoint.position, Quaternion.identity);
        CamPos.Boulder = BoulderCast;
        CamPos.followBoulder = true;
        StartGameWin();
    }

    public void SpringButton()
    {
        PlaceSpaceObj.SetActive(true);
        HighlightBoxer(0);
        ObjectPlacer.SelectedObject = ObjectList[2];
        ObjectRotator.RotateObject = false;
    }

    public void BrickButton()
    {
        PlaceSpaceObj.SetActive(true);
        HighlightBoxer(1);
        ObjectPlacer.SelectedObject = ObjectList[0];
        ObjectRotator.RotateObject = false;
    }

    public void MetalButton()
    {
        PlaceSpaceObj.SetActive(true);
        HighlightBoxer(2);
        ObjectPlacer.SelectedObject = ObjectList[1];
        ObjectRotator.RotateObject = false;
    }

    public void PlayButton()
    {
        start = true;
        ObjectPlacer.GameStarted = true;
        LeanTween.move(Hotbar, HotBarOffScreen, 2f);
        ObjectRotator.RotateObject = false;
    }

    public void EraserButton()
    {
        PlaceSpaceObj.SetActive(true);
        HighlightBoxer(3);
        ObjectPlacer.SelectedObject = ObjectList[3];
        ObjectRotator.RotateObject = false;
    }

    public void PlaceSpace()
    {
        ObjectPlacer.PlaceObject();
        UpdateObjectNumbers();
    }

    public void RotationUser()
    {
        PlaceSpaceObj.SetActive(false);
        HighlightBoxer(4);
        ObjectRotator.RotateObject = true;
        ObjectPlacer.SelectedObject = ObjectList[4];
    }

    void HighlightBoxer(int Index)
    {
        if(Index == 0)
        {
            HighlightBoxes[0].SetActive(true);
            HighlightBoxes[1].SetActive(false);
            HighlightBoxes[2].SetActive(false);
            HighlightBoxes[3].SetActive(false);
            HighlightBoxes[4].SetActive(false);
        }

        if (Index == 1)
        {
            HighlightBoxes[1].SetActive(true);
            HighlightBoxes[0].SetActive(false);
            HighlightBoxes[2].SetActive(false);
            HighlightBoxes[3].SetActive(false);
        }

        if (Index == 2)
        {
            HighlightBoxes[2].SetActive(true);
            HighlightBoxes[0].SetActive(false);
            HighlightBoxes[1].SetActive(false);
            HighlightBoxes[3].SetActive(false);
            HighlightBoxes[4].SetActive(false);
        }

        if (Index == 3)
        {
            HighlightBoxes[3].SetActive(true);
            HighlightBoxes[0].SetActive(false);
            HighlightBoxes[1].SetActive(false);
            HighlightBoxes[2].SetActive(false);
            HighlightBoxes[4].SetActive(false);
        }

        if (Index == 4)
        {
            HighlightBoxes[4].SetActive(true);
            HighlightBoxes[0].SetActive(false);
            HighlightBoxes[1].SetActive(false);
            HighlightBoxes[2].SetActive(false);
            HighlightBoxes[3].SetActive(false);
        }
    }

    void UpdateObjectNumbers()
    {
        BrickNumText.GetComponent<TMPro.TextMeshProUGUI>().text = ObjectPlacer.BrickNum.ToString();
        SpringNumText.GetComponent<TMPro.TextMeshProUGUI>().text = ObjectPlacer.SpringNum.ToString();
        MetalNumText.GetComponent<TMPro.TextMeshProUGUI>().text = ObjectPlacer.MetalNum.ToString();
    }
}
