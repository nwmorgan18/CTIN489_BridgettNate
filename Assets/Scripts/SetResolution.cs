using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour
{
    void Start()
    {
        // Set the screen resolution
        Screen.SetResolution(320, 180, false); // Adjust the width and height as needed
    }
}
