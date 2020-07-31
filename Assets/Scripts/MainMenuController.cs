using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject howToPlayMenu;
    public GameObject controlsMenu;
    public GameObject creditsMenu;
    public AudioSource backgroundMusic;
    public Text loadingText;
    public float fadeTime;
    private string GAME_SCENE = "GameScene";

    public void Start()
    {
        loadingText.enabled = false;
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().Fade(SceneFader.FadeDirection.Out)); 
    }

    public static IEnumerator FadeAudioOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public void PlayGame()
    {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().Fade(SceneFader.FadeDirection.In));
        loadingText.enabled = true;
        StartCoroutine(FadeAudioOut(backgroundMusic, fadeTime));
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In,GAME_SCENE));
    }

  
    public void HowToPlay()
    {
        mainMenu.SetActive(false);
        howToPlayMenu.SetActive(true);
        howToPlayMenu.GetComponentInChildren<Button>().Select();
    }

    public void Controls()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
        controlsMenu.GetComponentInChildren<Button>().Select();
    }


    public void Credits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
        creditsMenu.GetComponentInChildren<Button>().Select();
    }

    public void BackToMenu()
    {
        mainMenu.SetActive(true);
        mainMenu.GetComponentInChildren<Button>().Select();
        howToPlayMenu.SetActive(false);
        creditsMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
