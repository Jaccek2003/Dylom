using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour
{
    public Transform player;
   
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GameManager.instance.playerPosition);
        player.position = GameManager.instance.playerPosition;
        StartCoroutine(MovePlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MovePlayer()
    {
        yield return null;

        yield return null;


        player.position = GameManager.instance.playerPosition;
    }
}
