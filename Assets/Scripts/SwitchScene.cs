using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchScene : MonoBehaviour
{
    public int targetSceneBuildIndex; // Name of the scene to transition to

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure it's the player character entering the trigger
        {
            SceneManager.LoadScene(targetSceneBuildIndex); // Load the specified scene
            Debug.Log("Switching scene");
        }
    }
}
