using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    private float delay = 1f;
    [SerializeField] GameObject container;

    private List<string> messages = new List<string>();

    bool started = false;

    void Start()
    {
        messages.Add("We need to find our ship.");
        messages.Add("Watch out for the locals!");
    }

    void Update()
    {
        delay -= Time.deltaTime;
        if(delay<=0 && !started){
            Time.timeScale = 0;
            container.SetActive(true);
            UpdateText("It seems we've crash landed on an alien planet!");
            started = true;
        }
    }

    public void next() {
        if (messages.Count == 0) {
            container.SetActive(false);
            Time.timeScale = 1;
        }

        UpdateText(messages[0]);
        messages.RemoveAt(0);
    }

    // Function to update the text content
    public void UpdateText(string newText)
    {
        // Assign the new text to the TextMeshPro component
        textMeshPro.text = newText;
    }
}
