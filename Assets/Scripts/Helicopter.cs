using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public float switchInterval = 1f; // Time interval between sprite switches

    private SpriteRenderer spriteRenderer;
    private bool isSprite1Active = true;
    private float timer = 0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on the GameObject.");
            enabled = false; // Disable the script if SpriteRenderer is not found
        }

        // Set the initial sprite
        if (isSprite1Active)
            spriteRenderer.sprite = sprite1;
        else
            spriteRenderer.sprite = sprite2;
    }

    private void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if it's time to switch sprites
        if (timer >= switchInterval)
        {
            // Switch sprites
            if (isSprite1Active)
                spriteRenderer.sprite = sprite2;
            else
                spriteRenderer.sprite = sprite1;

            // Toggle the active sprite
            isSprite1Active = !isSprite1Active;

            // Reset the timer
            timer = 0f;
        }
    }
}
