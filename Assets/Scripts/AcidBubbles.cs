using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBubbles : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Base Layer.AcidBubbles");
    }
}
