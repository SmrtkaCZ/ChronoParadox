using UnityEngine;
using UnityEngine.InputSystem;

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

    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameratrans;
    private float rychlost;
    

    private InputAction moveaction;
    private InputAction jumpaction;
    private InputAction sprintaction;

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
        
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveaction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameratrans.right.normalized + move.z * cameratrans.forward.normalized;
        move.y = 0f;
        if(sprintaction.ReadValue<float>() > 0)
        {
            rychlost = playerSpeed * 2f;
            Debug.Log("ok");
        }
        else
        {
            rychlost = playerSpeed;
        }
        controller.Move(move * Time.deltaTime * rychlost);

        // Changes the height position of the player..
        if (jumpaction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


        Quaternion rotation = Quaternion.Euler(0, cameratrans.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime);
    }
}
