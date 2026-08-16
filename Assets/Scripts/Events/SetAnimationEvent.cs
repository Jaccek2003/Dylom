using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetAnimationEvent : BaseEvent
{
    public Animator animator;

    public string animation;

    public bool state;

    public override void Exectute()
    {
        animator.SetBool(animation, state);
    }
}
