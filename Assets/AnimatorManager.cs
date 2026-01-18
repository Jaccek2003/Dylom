using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeProperty(ChangeProperty change)
    {
        change.changeProperty(animator); 
    }
}
