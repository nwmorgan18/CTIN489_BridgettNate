using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchScene : MonoBehaviour
{
    [SerializeField] public string sceneName; // Name of the scene to transition to

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure it's the player character entering the trigger
        {
            SceneManager.LoadScene(sceneName); // Load the specified scene
        }
    }
}
