using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnClick : MonoBehaviour
{
    private Transform player;
    public float clickRange= 20.0f;

    public UnityEvent onClick;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= clickRange)
        {
            onClick.Invoke();
        }
        else
        {
            Debug.Log("Musisz byæ bli¿ej, aby podnieœæ ten przedmiot.");
        }
    }
}
