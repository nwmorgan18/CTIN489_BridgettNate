using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAnim : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Base Layer.Fire");
    }

}
