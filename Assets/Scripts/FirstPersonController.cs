using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public static int orbsCollected;
    public static bool isDead;

    public bool CanMove { get; private set; } = true;
    private bool isSprinting => canSprint && Input.GetKey(sprintKey);
    private bool shouldJump => characterController.isGrounded && Input.GetKeyDown(jumpKey);

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
   

    [Header("Control")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [Header("Movement Parameters")]
    [SerializeField] public float walkspeed = 3.0f;
    [SerializeField] private float sprintspeed = 6.0f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float LookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float LookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float UpperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float LowerLookLimit = 80.0f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    private Camera playerCamera;
    private CharacterController characterController;
    private Animator anim;

    public float currentSpeed;

    private Vector3 MoveDirection;
    private Vector2 CurrentInput;

    private float rotationX;
    private float finalSpeed;
    public static float weaponAnimationSpeed;

    public int Health;
    public GameObject redScreen;
    public bool isHit;

    public Text healthtext, speedtext,upgradetext;

    public static bool stop;
    public bool isPaused;
    public GameObject pause;

    private void Awake()
    {
        orbsCollected = 0;    
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        currentSpeed = characterController.velocity.magnitude;
        if(CanMove)
        {
            HandleMovement();
            HandleMouseLook();
            if (canJump)
                HandleJump();
            ApplyFinalMovement();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !isDead) //Pause Menu
        {
            Pause();
        }
        finalSpeed = walkspeed + Modifiers.instance.Speed;
        speedtext.text = " " + finalSpeed;
        healthtext.text = "" + Health;
        weaponAnimationSpeed = characterController.velocity.magnitude / finalSpeed;
        upgradetext.text = ""+orbsCollected;
        if(weaponAnimationSpeed>1)
        {
            weaponAnimationSpeed = 1;
        }
    }

    private void HandleMovement() //Handle Player Movement
    {
        CurrentInput = new Vector2((isSprinting ? sprintspeed : finalSpeed) * Input.GetAxis("Vertical"), (isSprinting ? sprintspeed : finalSpeed) * Input.GetAxis("Horizontal"));
        float moveDirectionY = MoveDirection.y;
        MoveDirection = (transform.TransformDirection(Vector3.forward) * CurrentInput.x) + (transform.TransformDirection(Vector3.right) * CurrentInput.y);
        MoveDirection.y = moveDirectionY;
    
    }

    private void HandleMouseLook() // Handle Mouse movement
    {
        rotationX -= Input.GetAxis("Mouse Y") * LookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -UpperLookLimit, LowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * LookSpeedX, 0); 
    }

    private void HandleJump() // Handles Player jump
    {
        if (shouldJump)
            MoveDirection.y = jumpForce;
    }

    private void ApplyFinalMovement() // Applies all the movement  values calculated above
    {
        if(!characterController.isGrounded)
        {
            MoveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(MoveDirection * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        if (!isHit && !isDead)
        {
            StartCoroutine(ApplyDamage(damage));
        }
    }

    void Pause()
    {
        if (!isPaused)
        {
            pause.SetActive(true);
            Time.timeScale = 0;
            CanMove = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            stop = true;
            isPaused = true;
        }
        else
        {
            pause.SetActive(false);
            Time.timeScale = 1;
            stop = false;
            CanMove = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isPaused = false;
        }
    }
    public void ApplyImpact(float impactForce,int Damage)
    {
        StartCoroutine(Impact(impactForce));
        TakeDamage(Damage);
        Debug.Log("Yes");
    }

    IEnumerator Impact(float impactForce)
    {
        characterController.enabled = false;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(-transform.forward * impactForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        characterController.enabled = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }
    IEnumerator Dying()
    {
        isDead = true;
        anim.enabled = true;
        anim.SetBool("isDead", true);
        CanMove = false;
        yield return new WaitForSecondsRealtime(2f);
        Pause();
    }
    IEnumerator ApplyDamage(int damage)
    {
        isHit = true;
        redScreen.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        redScreen.SetActive(false);
        Health = Health - damage;
        if (Health <= 0)
        {
            Health = 0;
            StartCoroutine(Dying());
        }
        yield return new WaitForSeconds(0.5f);
        isHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Health"))
        {
            Health += 10;
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Upgrade"))
        {
            orbsCollected++;
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene("Credits");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
