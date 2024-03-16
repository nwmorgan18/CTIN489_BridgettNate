using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandIgnore : MonoBehaviour
{
    public GameObject swamp;
    private Collider2D swampCollider;

    void Start() {
        swampCollider = swamp.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Disable the swamp trigger when the player enters the island
            swampCollider.isTrigger = false;
            Debug.Log("Island");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Re-enable the swamp trigger when the player leaves the island
            swampCollider.isTrigger = true;
        }
    }
}
