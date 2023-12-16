using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController),typeof(PlayerInput))]
public class PohybHlHrdiny : MonoBehaviour
{
    [Header("Values for change")]
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float RotationSpeed = 5f;

    [Header("Rules")]
    [SerializeField]
    private bool doublejumpallowed = true;
    [SerializeField]
    private bool sprintallowed = true;

    //values
    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameratrans;
    private float speed;
    private float jumpcount = 0;


    private InputAction moveaction;
    private InputAction jumpaction;
    private InputAction sprintaction;
    private InputAction intErekce;

    private Vector3 StartLocation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameratrans = Camera.main.transform;
        moveaction = playerInput.actions["WSAD"];
        jumpaction = playerInput.actions["Jump"];
        sprintaction = playerInput.actions["Sprint"];
        intErekce = playerInput.actions["Interaction"];
        StartLocation = transform.position;
        
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            jumpcount = 0;
            playerVelocity.y = 0f;
        }

        Vector2 input = moveaction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameratrans.right.normalized + move.z * cameratrans.forward.normalized;
        move.y = 0f;
        //sprint
        if(sprintaction.ReadValue<float>() > 0 && sprintallowed)
        {
            speed = playerSpeed * 2f;
        }
        else
        {
            speed = playerSpeed;
        }
        controller.Move(move * Time.deltaTime * speed);

        // Jump
        if (jumpaction.triggered && groundedPlayer || jumpaction.triggered && jumpcount == 1 && doublejumpallowed)
        {
            jumpcount++;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


        Quaternion rotation = Quaternion.Euler(0, cameratrans.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime);

        //Interekce
        if(intErekce.triggered && FFA.ucaninter)
        {
            FFA.pressedE = true;
            FFA.ucaninter = false;
        }


        //dropped
        if(transform.position.y <= -10F)
        {
            transform.position = StartLocation;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Point")
        {
            FFA.body++;
            other.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PortalTutor")
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
    }
}