using System;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{

    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float multiplier;

    [Header("References")]
    public Animator weaponAnimator;

    [Header("Swayvariables")]
    public float swayAmountA = 1;
    public float swayAmountB = 2;
    public float swayScale = 600;
    public float swayLerpSpeed = 14;
    public float swayTime;
    public Vector3 swayPosition;

    private void FixedUpdate()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * multiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * multiplier;

        // calculate target rotation
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        // rotate 
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);

        //weaponAnimator.speed = FirstPersonController.weaponAnimationSpeed;

        if(FirstPersonController.weaponAnimationSpeed>0)
        {
            weaponAnimator.SetBool("isWalking", true);
        }
        else
        {
            weaponAnimator.SetBool("isWalking", false);
        }

        CalculateSway();
    }
    private void CalculateSway()
    {
        var targetPosition = LissajousCurve(swayTime, swayAmountA, swayAmountB)/swayScale;
        swayPosition = Vector3.Lerp(swayPosition, targetPosition, Time.smoothDeltaTime * swayLerpSpeed);
        swayTime += Time.deltaTime;

        if(swayTime>6.3f)
        {
            swayTime = 0;
        }
        transform.localPosition = swayPosition;
    }
    private Vector3 LissajousCurve(float Time, float A, float B)
    {
        return new Vector3(Mathf.Sin(Time), A * Mathf.Sin(B * Time + Mathf.PI));
    }
}