using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtOnCollision : MonoBehaviour
{
    private ThoughtManager thoughtManager;
    private bool thoughtOnCooldown;
    public Thought thought;
    public float thoughtCooldown = 2F;

    void Start()
    {
        thoughtManager = GameObject.FindGameObjectWithTag("ThoughtManager").GetComponent<ThoughtManager>();

        thought.onThoughtEnded.AddListener(OnThoughtEnded);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && !thoughtOnCooldown && thoughtManager.CurrentThought == null)
        {
            //float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            //if (distanceToPlayer <= talkRange)
            //{
            thoughtOnCooldown = true;
            thoughtManager.StartThought(thought);
            //}
        }
    }

    IEnumerator EnableThought()
    {
        yield return new WaitForSeconds(thoughtCooldown);

        thoughtOnCooldown = false;
    }

    private void OnThoughtEnded()
    {
        StartCoroutine(EnableThought());
    }

    private void OnDestroy()
    {
        if (thought != null)
            thought.onThoughtEnded.RemoveListener(OnThoughtEnded);
    }

    private void OnDisable()
    {
        if (thought != null)
            thought.onThoughtEnded.RemoveListener(OnThoughtEnded);
    }
}
