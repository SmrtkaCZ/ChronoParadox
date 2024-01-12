using Cinemachine;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [Header("Objekty")]
    public GameObject Pauza;
    public CinemachineVirtualCamera VC1;
    public TMP_Text Leveling;
    public TMP_Text Questing;
    public LayerMask groundLayer;


    //values
    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameratrans;
    private float speed;
    private float jumpcount = 0;
    private int quest = 0;
    private int bodyvlevelu = 0;
    private bool resetpoz = false;

    private Vector3 Cam;

    private InputAction moveaction;
    private InputAction jumpaction;
    private InputAction sprintaction;
    private InputAction intErekce;
    private InputAction ESCAPE;

    private bool pauza = false;

    private void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameratrans = Camera.main.transform;
        moveaction = playerInput.actions["WSAD"];
        jumpaction = playerInput.actions["Jump"];
        sprintaction = playerInput.actions["Sprint"];
        intErekce = playerInput.actions["Interaction"];
        ESCAPE = playerInput.actions["ESC"];

        FFA.StartPozice = transform.position;
        Cam = VC1.gameObject.transform.position;


        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Leveling.text = "Level 1";
            quest = 0;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Leveling.text = "Level 2";
            quest = 3;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            Leveling.text = "Level 2";
            quest = 5;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            Leveling.text = "Level 3";
            quest = 3;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            Leveling.text = "Level 4";
            quest = 3;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            Leveling.text = "Level 5";
            quest = 3;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            Leveling.text = "Level 6";
            quest = 3;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            Leveling.text = "Level 7";
            quest = 3;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Leveling.text = "Level 8";
            quest = 3;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Leveling.text = "Level 8";
            quest = 3;
        }
        QuestsChanger();
    }
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            jumpcount = 0;
            playerVelocity.y = 0f;
        }
        if(FFA.move)
        {
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
            if (jumpaction.triggered && CheckGrounded() || jumpaction.triggered && jumpcount == 1 && doublejumpallowed)
            {
                jumpcount++;
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }


        Quaternion rotation = Quaternion.Euler(0, cameratrans.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime);

        //Interekce
        if(intErekce.triggered && FFA.ucaninter)
        {
            FFA.pressedE = true;
            FFA.ucaninter = false;
        }

        //ESC
        if(ESCAPE.triggered)
        {
            pauza = !pauza;
            PauzaMenu(pauza);
        }

        //dropped
        if(transform.position == FFA.StartPozice && VC1.Follow != null)
        {
            resetpoz = false;
        }
        if(transform.position.y <= -10F || resetpoz)
        {
            VC1.Follow = null;
            VC1.LookAt = null;
            transform.position = FFA.StartPozice;
            VC1.Follow = transform;
            VC1.LookAt = transform;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Point")
        {
            bodyvlevelu++;
            other.gameObject.transform.parent.gameObject.SetActive(false);
        }
        else if(other.gameObject.tag == "zmenaukolu")
        {
            quest++;
            other.gameObject.SetActive(false);
            QuestsChanger();
        }
        
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PortalTutor")
        {
            FFA.body += bodyvlevelu;
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
        else if (collision.gameObject.tag == "car")
        {
            resetpoz = true;
        }
        else if (collision.gameObject.tag == "Lodmala")
        {
            gameObject.transform.parent = collision.gameObject.transform;
        }
        
    }

    private void PauzaMenu(bool cs)
    {
        
        if(cs)
        {
            VC1.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Pauza.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            VC1.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Pauza.gameObject.SetActive(false);
            Time.timeScale = 1;
            /*if (FFA.reset)
            {
                transform.position = FFA.StartPozice;
                FFA.reset = false;
                Debug.Log(transform.position);
            }*/
        }
        
    }
    private void QuestsChanger()
    {
        switch(quest)
        {
            //Tutor
            case 0:
                Questing.text = "Use SpaceBar to jump across gap between platforms.";
                break;
            case 1:
                Questing.text = "Use Shift to run to next platform.";
                break;
            case 2:
                Questing.text = "Use E to push the button.";
                break;
            //Neon1
            case 3:
                Questing.text = "Find a Key.";
                break;
            case 4:
                Questing.text = "Get out of building.";
                break;
            case 5:
                Questing.text = "Find your time transporter and press E.";
                break;
            //Pir1
            case 6:
                Questing.text = "Use E to push the button.";
                break;
            case 7:
                Questing.text = "Use E to push the button.";
                break;
            case 8:
                Questing.text = "Use E to push the button.";
                break;
            //Pir2
            case 9:
                Questing.text = "Use E to push the button.";
                break;
            case 10:
                Questing.text = "Use E to push the button.";
                break;
            case 11:
                Questing.text = "Use E to push the button.";
                break;
            //Ves1
            case 12:
                Questing.text = "Use E to push the button.";
                break;
            case 13:
                Questing.text = "Use E to push the button.";
                break;
            case 14:
                Questing.text = "Use E to push the button.";
                break;
            //Les
            case 15:
                Questing.text = "Use E to push the button.";
                break;
            case 16:
                Questing.text = "Use E to push the button.";
                break;
            case 17:
                Questing.text = "Use E to push the button.";
                break;
            //Ves2
            case 18:
                Questing.text = "Use E to push the button.";
                break;
            case 19:
                Questing.text = "Use E to push the button.";
                break;
            case 20:
                Questing.text = "Use E to push the button.";
                break;
            //Neon2
            case 21:
                Questing.text = "Use E to push the button.";
                break;
            case 22:
                Questing.text = "Use E to push the button.";
                break;
            case 23:
                Questing.text = "Use E to push the button.";
                break;
        }
    }
    bool CheckGrounded()
    {
        return Physics.SphereCast(transform.position, GetComponent<SphereCollider>().radius, Vector3.down, out _, GetComponent<SphereCollider>().radius * 0.5f + 0.1f, groundLayer);
    }
}