using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;


public class PauseScreen : MonoBehaviour
{
    public GameObject pauseObject;
    public Text loadingText;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        HidePaused();
        loadingText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("joyStart"))
        {
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
                ShowPaused();
            } else if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
                HidePaused();
            }
        }
    }

    public void Restart()
    {
        loadingText.enabled = true;
        SceneManager.LoadScene("GameScene");
        
    }

    public void Resume()
    {
        Time.timeScale = 1;
        HidePaused();
    }

    public void PauseControl()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            ShowPaused();
        } else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            HidePaused();
        }
    }

    public void ShowPaused()
    {
        pauseObject.SetActive(true);
        EventSystem es = GameObject.FindWithTag("PauseES").GetComponent<EventSystem>();
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(es.firstSelectedGameObject);
    }

    public void HidePaused()
    {
        pauseObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
