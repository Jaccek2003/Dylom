using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class OnVideoEnd : MonoBehaviour
{
    [Header("Video Player")]
    public VideoPlayer videoPlayer;

    [Header("Actions to run when video ends")]
    public UnityEvent onVideoEnd;

    void Awake()
    {
        if (!videoPlayer)
            videoPlayer = GetComponent<VideoPlayer>();
    }

    void OnEnable()
    {
        videoPlayer.loopPointReached += HandleVideoEnd;
    }

    void OnDisable()
    {
        videoPlayer.loopPointReached -= HandleVideoEnd;
    }

    void HandleVideoEnd(VideoPlayer vp)
    {
        onVideoEnd.Invoke();
    }


}
