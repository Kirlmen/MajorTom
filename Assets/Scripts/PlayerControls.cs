using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("ship speed up and down based upon player's input ")] [SerializeField] float movementSpeed = 50f;

    [Tooltip("How far player moves horizontally ")] [SerializeField] float xRange = 17.2f; // x axis clamp
    [Tooltip("How far player moves vertically")] [SerializeField] float upYRange = 16f;// y axis clamp
    [Tooltip("How far player moves vertically")] [SerializeField] float downYRange = 3.2f;// y axis clamp

    [Header("Screen Position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -15f;

    [Header("Player Input based tuning")]
    [SerializeField] float positionYawFactor = 1f;
    [SerializeField] float controlRollFactor = -15f;
    [SerializeField] GameObject[] lasers;


    float xMovement, yMovement;



    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();

    }
    // x pitch, y yaw, z roll
    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlMovement = yMovement * controlPitchFactor;
        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float rollDueToControlMovement = xMovement * controlRollFactor;

        float pitch = pitchDueToPosition + pitchDueToControlMovement;  //x ekseninde dönüş. uçağın kanatlarının uçağın burnunu indirip kaldırması.
        float yaw = yawDueToPosition; // uçağın tamamının eğimsiz sağa sola y ekseninde dönüşü.
        float roll = rollDueToControlMovement; // uçağın burnundan kuyruğuna kadar olan hat. sağa ve sola eğimli z ekseninde  dönüşü.
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        //movement keys
        xMovement = Input.GetAxis("Horizontal");
        yMovement = Input.GetAxis("Vertical");

        //independent movement formula and movement screen border
        float yOffset = yMovement * Time.deltaTime * movementSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -downYRange, upYRange);


        float xOffset = xMovement * Time.deltaTime * movementSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);


        // movement vectors
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }


    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            LaserBool(true);
        }
        else
        {
            LaserBool(false);
        }
    }

    void LaserBool(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }




}
