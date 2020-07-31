using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class DeathScreenController : MonoBehaviour
{
    GameObject gameMusic;
    public Text loadingText;
    public Text scoreText;
    private int score;
    public AudioSource backgroundMusic;
    public float fadeTime;
    private string GAME_SCENE = "GameScene";

    public void Awake()
    {
        loadingText.enabled = false;
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        }
        else
        {
            Debug.Log("Non Existing Key: Score");
            score = 0;
        }

        scoreText.text = score.ToString();
        PlayerPrefs.SetInt("Score", 0);
        gameMusic = GameObject.FindGameObjectWithTag("GameMusic");
        Destroy(gameMusic);
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().Fade(SceneFader.FadeDirection.Out));
    }
    public void Restart()
    {
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().Fade(SceneFader.FadeDirection.In));
        loadingText.enabled = true;
        StartCoroutine(FadeAudioOut(backgroundMusic, fadeTime));
        StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, GAME_SCENE));
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

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
