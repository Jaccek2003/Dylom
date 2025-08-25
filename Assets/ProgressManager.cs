using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public bool wasBackpackTaken = false;

    public static ProgressManager Instance { get; private set; }

    void Awake()
    {
        // If an instance already exists and it's not this, destroy this one
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance
        Instance = this;

        // Optional: Keep this across scenes
        DontDestroyOnLoad(gameObject);
    }


    public void SetBackpack(bool value)
    {
        wasBackpackTaken = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
