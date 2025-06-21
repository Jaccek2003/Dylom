using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickEvent2d : MonoBehaviour
{
    public UnityEvent onClick;

    private Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        onClick.Invoke();
    }
}
