using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    private Animator anim;
    public GameObject pauseButton;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Retry() {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// pause action
    /// </summary>
    public void Pause() {
        // play pause animation
        anim.SetBool("isPause", true);
        pauseButton.SetActive(false);
    }

    public void Home() {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Resume() {
        // play resume animation
        Time.timeScale = 1;
        anim.SetBool("isPause", false);
    }

    public void PauseAnimEnd() {
        Time.timeScale = 0;
    }

    public void ResumeAnimEnd() {
        pauseButton.SetActive(true);
    }
}
