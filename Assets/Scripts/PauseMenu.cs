using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject controlScreen;
    [SerializeField] GameObject restart;
    [SerializeField] GameObject resume;
    [SerializeField] GameObject controls;
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;

    void Start() {
        setMusicVolume();
    }
    void FixedUpdate () {
        setMusicVolume();
    }
    
    public void Pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Controls() {
        restart.SetActive(false);
        resume.SetActive(false);
        controls.SetActive(false);
        musicSlider.gameObject.SetActive(false);
        controlScreen.SetActive(true);
    }

    public void Restart() {
        string currentscene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentscene);
        Time.timeScale = 1;
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Back() {
        controls.SetActive(true);
        restart.SetActive(true);
        resume.SetActive(true);
        musicSlider.gameObject.SetActive(true);
        controlScreen.SetActive(false);
    }

    public void setMusicVolume() {
        float volume = musicSlider.value;
        myMixer.SetFloat("Master", Mathf.Log10(volume)*20);
    }
}
