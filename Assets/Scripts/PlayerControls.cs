using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float movementSpeed = 50f;
    [SerializeField] float xRange = 17.2f;
    [SerializeField] float upYRange = 16f;
    [SerializeField] float downYRange = 3.2f;
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -15f;

    [SerializeField] float positionYawFactor = 1f;
    [SerializeField] float controlRollFactor = -15f;

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

        //independent movement formula
        float yOffset = yMovement * Time.deltaTime * movementSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -downYRange, upYRange);


        float xOffset = xMovement * Time.deltaTime * movementSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);


        // movement vectors
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
