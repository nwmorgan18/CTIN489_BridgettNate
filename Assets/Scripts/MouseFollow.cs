using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    Vector3 mousePosition;
    [SerializeField] private float moveSpeed = 0.1f;
    Rigidbody2D rb;
    Vector2 position = new Vector2(0f, 0f);
    //private bool mousedown = false;
    //AudioSource attractsound;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //attractsound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        /*
        if (Input.GetMouseButtonDown(0) || mousedown)
        {
            mousedown = true;
            if (!attractsound.isPlaying)
            {
                attractsound.Play();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            mousedown = false;
            if (attractsound.isPlaying)
            {
                attractsound.Stop();
            }
        }
        */
    }

    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }
}
