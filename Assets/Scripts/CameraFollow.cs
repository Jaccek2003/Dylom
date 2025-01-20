using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smothing = 6f;
    Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;

        }
    private void FixedUpdate()
    {
        Vector3 targetCamPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPosition, smothing * Time.deltaTime);
    }
}
