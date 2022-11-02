using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Level5GameManager : MonoBehaviour
{
    public bool start = false;

    [SerializeField] private CanvasGroup WinScreenGroup;
    [SerializeField] private CanvasGroup LoseScreenGroup;

    //Boulder Variables
    public GameObject FifthLevelBoulder;
    //public Transform BoulderStartingPoint;
    private bool BoulderIsSpawned = false;
    public GameObject BoulderCast;

    //Camera Variables
    public float SetBoulderCamSize;
    public CameraPosition CamPos;
    public Transform FinalCamPos;
    public float FinalCamSize;

    //UI Element Variables
    public GameObject PauseUI;
    private bool IsPaused = false;

    public GameObject SpringNumText;
    public GameObject BrickNumText;
    public GameObject MetalNumText;
    private bool GameWinScreenUse = false;
    public GameObject MainMenuButton;
    public GameObject PlaceSpaceObj;

    public GameObject[] HighlightBoxes;
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

        if (FifthLevelBoulder != null)
        {
            FifthLevelBoulder.SetActive(false);
        }

        ObjectList = ObjectPlacer.ObjectsToPlace;

        UpdateObjectNumbers();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Time.timeScale = 1;
                PauseUI.SetActive(false);
                IsPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                PauseUI.SetActive(true);
                IsPaused = true;
            }
        }

        if (start)
        {
            if (!BoulderIsSpawned)
            {
                SpawnBoulder();
                BoulderIsSpawned = true;

                if (FifthLevelBoulder != null)
                {
                    FifthLevelBoulder.SetActive(true);
                }
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
                    UITimerValue = 0;
                    StartTheTimer = false;
                }
            }
        }

        if(GameOver)
        {
            LoseScreenGroup.gameObject.SetActive(true);
            LoseScreenGroup.alpha = Mathf.Lerp(LoseScreenGroup.alpha, 1, 1 * Time.deltaTime);
        }

        if (GameWinScreenUse)
        {
            WinScreenGroup.alpha = Mathf.Lerp(WinScreenGroup.alpha, 1, 1 * Time.deltaTime);
        }

        if (!GameOver)
        {
            if (Man.IsDead)
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
            if (!GameOver)
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
        StartTheTimer = true;

        HotBarGroup.GetComponent<RectTransform>().LeanMove(new Vector3(400, -223, 0), 2f).setEaseInOutExpo();
        LeanTween.moveLocal(Timer, new Vector3(600f, 275f, 0), 3).setEaseInOutExpo();
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
        if (Index == 0)
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

    public void ResumeButton()
    {
        Debug.Log("Resume");
        Time.timeScale = 1;
        PauseUI.SetActive(false);
        IsPaused = false;
    }

    void UpdateObjectNumbers()
    {
        BrickNumText.GetComponent<TMPro.TextMeshProUGUI>().text = ObjectPlacer.BrickNum.ToString();
        SpringNumText.GetComponent<TMPro.TextMeshProUGUI>().text = ObjectPlacer.SpringNum.ToString();
        MetalNumText.GetComponent<TMPro.TextMeshProUGUI>().text = ObjectPlacer.MetalNum.ToString();
    }
}
