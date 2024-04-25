using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    void Update()
    {
        // Check if the 'q' key is pressed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q pressed");
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
