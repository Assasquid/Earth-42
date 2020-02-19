using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [Tooltip("In m^-1")][SerializeField] float xSpeed = 30f;
    [Tooltip("In m")] [SerializeField] float xRange = 10f;
    
    [Tooltip("In m^-1")] [SerializeField] float ySpeed = 20f;
    [Tooltip("In m")] [SerializeField] float yTopRange = 4.5f;
    [Tooltip("In m")] [SerializeField] float yBottomRange = -7f;

    [SerializeField] float positionPitchFactor = -1.5f;
    [SerializeField] float controlPitchFactor = -20;

    [SerializeField] float positionYawFactor = 2f;

    [SerializeField] float controlRollFactor = -45f;

    float xThrow, yThrow;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void OnTriggerEnter(Collider collider)
    {
        print("Ouch !");
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

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, yBottomRange, yTopRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
