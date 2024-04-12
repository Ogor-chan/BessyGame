using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EntryIllustrationsController : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;
    private double animDuration;
    [SerializeField] UnityEngine.Object crystalLakeScene;

    private void Awake()
    {
        // Store the duration of the animation
        animDuration = playableDirector.duration;
    }

    private void Start()
    {
        // Option 1: Listen for the completion event
        playableDirector.stopped += OnAnimationStopped;
    }

    private void OnDestroy()
    {
        // Make sure to unsubscribe from the event to prevent memory leaks
        playableDirector.stopped -= OnAnimationStopped;
    }

    private void OnAnimationStopped(PlayableDirector director)
    {
        if (director == playableDirector)
        {
            Debug.Log("Animation has stopped.");
            // Animation has finished playing
        }
    }

    private void Update()
    {
        // Option 2: Check if the current time equals or exceeds the duration
        if (playableDirector.time >= animDuration)
        {
            Debug.Log("Animation has finished playing.");
            SceneManager.LoadScene(crystalLakeScene.name);
            // Animation has finished playing
        }
    }


}
