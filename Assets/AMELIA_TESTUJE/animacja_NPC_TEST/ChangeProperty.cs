using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "RunAnimation",
    menuName = "Animations/Run Animation",
    order = 1
)]
public  class ChangeProperty : ScriptableObject
{
    public string propertyID;
    public bool state;
    public void changeProperty(Animator animator) { 
        animator.SetBool(propertyID, state);
    }
}
