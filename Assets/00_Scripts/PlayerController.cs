using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    [Header("General")]
    [Tooltip("In m^-1")][SerializeField] float controlXSpeed = 30f;
    [Tooltip("In m")] [SerializeField] float xRange = 10f;
    [SerializeField] GameObject[] guns;
    
    [Tooltip("In m^-1")] [SerializeField] float controlYSpeed = 20f;
    [Tooltip("In m")] [SerializeField] float yTopRange = 4.5f;
    [Tooltip("In m")] [SerializeField] float yBottomRange = -7f;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -1.5f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Control-throw Based")]
    [SerializeField] float controlRollFactor = -45f;
    [SerializeField] float controlPitchFactor = -20;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    void OnPlayerDeath() // called by string reference
    {
        isControlEnabled = false;
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * controlXSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * controlYSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, yBottomRange, yTopRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            ActivateGuns();
        }

        else
        {
            DeactivateGuns();
        }
    }

    private void ActivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(true);
        }
    }

    private void DeactivateGuns()
    {
        foreach (GameObject gun in guns)
        { 
            gun.SetActive(false);
        }
    }
}
