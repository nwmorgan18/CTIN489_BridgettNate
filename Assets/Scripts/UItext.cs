using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UItext : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    public float displayTime = 30f;
    [SerializeField] private string message;

    // Start is called before the first frame update
    void Start()
    {
        // Get the TextMesh component attached to the same GameObject
        textMesh = GetComponent<TextMeshProUGUI>();

        // Start the coroutine to show the text mesh temporarily
        StartCoroutine(ShowTextMeshForDuration());
    }

    IEnumerator ShowTextMeshForDuration()
    {
        // Show the text mesh
        textMesh.text = message;

        // Wait for the specified duration
        yield return new WaitForSeconds(displayTime);

        // Hide the text mesh after the duration
        textMesh.text = "";
    }
}
