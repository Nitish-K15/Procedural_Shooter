using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject controlsPanel, storyPanel,titleScreen;
   public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenControls()
    {
        controlsPanel.SetActive(true);
        titleScreen.SetActive(false);
    }

    public void OpenStory()
    {
        storyPanel.SetActive(true);
        titleScreen.SetActive(false);
    }

    public void GoBack()
    {
        storyPanel.SetActive(false);
        controlsPanel.SetActive(false);
        titleScreen.SetActive(true);
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void End()
    {
        Application.Quit();
    }
}
