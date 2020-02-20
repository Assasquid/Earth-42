using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ok as long as this it the only script loading scenes

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")][SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab on player")][SerializeField] GameObject deathFX;
    [Tooltip("FX prefab on player")] [SerializeField] GameObject deathFXCam;

    private void OnTriggerEnter(Collider collider)
    {
        StartDeathSequence();
        deathFX.SetActive(true);
        deathFXCam.SetActive(true);
    }

    private void StartDeathSequence()
    {
        print("Player is dying");
        SendMessage("OnPlayerDeath");

    }
}