using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickEvent : MonoBehaviour
{
    public UnityEvent onClick;

    private Collider col;

    void Awake()
    {
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        onClick.Invoke();
    }
}
