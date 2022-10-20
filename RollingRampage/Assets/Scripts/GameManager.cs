using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    public GameObject TutorialBox;
    public Text TutorialBoxText;
    private bool[] ToolTipTracker = { true, true, true, true, true, true };
    public bool FirstLevel;

    public GameObject[] HighlightBoxes;
    public Transform HotBarOffScreen;
    public GameObject Hotbar;
    public GameObject HotBarGroup;

    public GameObject Timer;
    public Text TimerText;
    private bool StartTheTimer = false;
    private float UITimerValue;

    //Placing Object Variables
    public ObjectPlacer ObjectPlacer;
    private GameObject[] ObjectList;
    public ObjectRotator ObjectRotator;

    //Player
    public Man Man;
    private bool GameOver = false;
    public float GameWinTimer;

    // Start is called before the first frame update
    void Start()
    {
        UITimerValue = GameWinTimer; 
        
        CamPos.CamFinalSize = FinalCamSize;
        CamPos.EndPoint = FinalCamPos;

        HighlightBoxer(1);

        UpdateObjectNumbers();

        ObjectList = ObjectPlacer.ObjectsToPlace;

        if(!FirstLevel)
        {
            for(int i = 0; i < ToolTipTracker.Length; i++)
            {
                ToolTipTracker[i] = false;
            }
        }

        this.Wait(2f, () =>
        {
            if (ToolTipTracker[0])
            {
                ChangeTutorialBoxText("Protect The Guy from the boulder!\n" +
                    "Place walls and objects using the inventory and tools\n" +
                    "Press the PLAY button to start the boulder rolling", 8f);
                ToolTipTracker[0] = false;
            }
        });
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

            if (StartTheTimer && !GameOver)
            {
                
                if (UITimerValue > 0)
                {
                    UITimerValue -= Time.deltaTime;
                    updateTimer(UITimerValue);
                }
                else
                {
                    Debug.Log("Time is UP!");
                    UITimerValue = 0;
                    StartTheTimer = false;
                }
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

    void updateTimer(float currentTime)
    {
        //currentTime += 1;

        TimerText.text = currentTime.ToString();
    }

    public void StartGameWin()
    {
        this.Wait(GameWinTimer, () =>
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
        if(ToolTipTracker[1])
        {
            ChangeTutorialBoxText("The Spring is used to bounce any object except The Guy", 4f);
            ToolTipTracker[1] = false;
        }
        
        PlaceSpaceObj.SetActive(true);
        HighlightBoxer(0);
        ObjectPlacer.SelectedObject = ObjectList[2];
        ObjectRotator.RotateObject = false;
    }

    public void BrickButton()
    {
        if (ToolTipTracker[2])
        {
            ChangeTutorialBoxText("The Brick Wall is a weak wall that will somewhat slow down the boulder", 4f);
            ToolTipTracker[2] = false;
        }

        PlaceSpaceObj.SetActive(true);
        HighlightBoxer(1);
        ObjectPlacer.SelectedObject = ObjectList[0];
        ObjectRotator.RotateObject = false;
    }

    public void MetalButton()
    {
        if (ToolTipTracker[3])
        {
            ChangeTutorialBoxText("The Metal Wall is a stronger wall that will considerably slow down the boulder", 4f);
            ToolTipTracker[3] = false;
        }

        PlaceSpaceObj.SetActive(true);
        HighlightBoxer(2);
        ObjectPlacer.SelectedObject = ObjectList[1];
        ObjectRotator.RotateObject = false;
    }

    public void PlayButton()
    {
        start = true;
        ObjectPlacer.GameStarted = true;
        StartTheTimer = true;

        HotBarGroup.GetComponent<RectTransform>().LeanMove(new Vector3(400, -223, 0), 2f).setEaseInExpo();
        LeanTween.moveLocal(Timer, new Vector3(600f, 350f, 0), 3).setEaseInOutExpo();
        //LeanTween.move(Hotbar, HotBarOffScreen, 2f);
        ObjectRotator.RotateObject = false;
    }

    public void EraserButton()
    {
        if (ToolTipTracker[4])
        {
            ChangeTutorialBoxText("Click on objects to add them back into your inventory", 4f);
            ToolTipTracker[4] = false;
        }

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
        if (ToolTipTracker[5])
        {
            ChangeTutorialBoxText("Pick up an already placed object\nRotate with the A and D keys", 4f);
            ToolTipTracker[5] = false;
        }

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
            HighlightBoxes[4].SetActive(false);
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

    void ChangeTutorialBoxText(string NewText, float TimeOnScreen)
    {
        RectTransform RT = TutorialBox.GetComponent<RectTransform>();
        
        TutorialBoxText.text = NewText;

        LeanTween.moveLocal(TutorialBox, new Vector3(-370f, 250f, 0), 3).setEaseInOutExpo();

        this.Wait(TimeOnScreen, () =>
        {
            LeanTween.moveLocal(TutorialBox, new Vector3(-370f, 700f, 0), 3).setEaseInOutExpo();
        });
    }

    void UpdateObjectNumbers()
    {
        BrickNumText.GetComponent<TMPro.TextMeshProUGUI>().text = ObjectPlacer.BrickNum.ToString();
        SpringNumText.GetComponent<TMPro.TextMeshProUGUI>().text = ObjectPlacer.SpringNum.ToString();
        MetalNumText.GetComponent<TMPro.TextMeshProUGUI>().text = ObjectPlacer.MetalNum.ToString();
    }
}
