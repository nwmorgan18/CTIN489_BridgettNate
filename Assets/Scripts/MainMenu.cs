using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public int targetSceneBuildIndex; // Name of the scene to transition to

    // Update is called once per frame
    void Update()
    {
        // Check if the mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to world point
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse click hits the collider
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            // If the collider is not null and it's the collider attached to this GameObject
            if (hitCollider != null && hitCollider == GetComponent<Collider2D>())
            {
                // Do something when clicked
                SceneManager.LoadScene(targetSceneBuildIndex); // Load the specified scene

            }
        }
    }
}
