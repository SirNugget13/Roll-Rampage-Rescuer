using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] RectTransform fader;

    private void Start()
    {
        fader.gameObject.SetActive(true);
        this.Wait(0.3f, () =>
        {
            LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
            LeanTween.scale(fader, Vector3.zero, 1.2f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
            {
                fader.gameObject.SetActive(false);
            });
        });
    }

    public void LoadScenes(int index)
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, new Vector3(0, 0, 0), 0);

        this.Wait(1, () => 
        {
            LeanTween.scale(fader, Vector3.one, 0.5f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
            {
                SceneManager.LoadScene(index);
            });
        });
    }

    public void LoadScenes(string scene)
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
        {
            SceneManager.LoadScene(scene);
        });
    }

    public void LoadMainMenu()
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
        {
            SceneManager.LoadScene(0);
        });
    }
}
