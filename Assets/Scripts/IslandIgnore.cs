using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IslandIgnore : MonoBehaviour
{
    public GameObject swamp;
    private Collider2D swampCollider;
    static int numBridges = 0;

    void Start() {
        swampCollider = swamp.GetComponent<Collider2D>();
        numBridges = 0;
        // Debug.Log(gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerFeet"))
        {
            // Disable the swamp trigger when the player enters the island
            //swampCollider.isTrigger = false;
            swampCollider.gameObject.SetActive(false);
            numBridges++;
            // Debug.Log("Island");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerFeet"))
        {
            // Re-enable the swamp trigger when the player leaves the island
            swampCollider.gameObject.SetActive(true);
            numBridges--;

            if (numBridges == 0) {
                string currentscene = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentscene);
            }
        }
    }
}
