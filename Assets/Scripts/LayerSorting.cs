using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorting : MonoBehaviour
{
    public SpriteRenderer characterSpriteRenderer;
    public SpriteRenderer treeSpriteRenderer;

    private void Update()
    {
        // Check if the character is below the tree
        if (transform.position.y < treeSpriteRenderer.transform.position.y)
        {
            // Set character sorting layer to be in front of the tree
            characterSpriteRenderer.sortingLayerName = "Player in front of Object";
        }
        else
        {
            // Set character sorting layer back to original
            characterSpriteRenderer.sortingLayerName = "Player";
        }
    }
}
