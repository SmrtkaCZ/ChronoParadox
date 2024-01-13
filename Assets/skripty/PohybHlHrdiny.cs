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
    public TMP_Text questing;
    public LayerMask groundLayer;


    //values
    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameratrans;
    private float speed;
    private float jumpcount = 0;
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
            FFA.questy = 0;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Leveling.text = "Level 2";
            FFA.questy = 3;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            Leveling.text = "Level 2";
            FFA.questy = 5;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            Leveling.text = "Level 3";
            FFA.questy = 6;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            Leveling.text = "Level 4";
            FFA.questy = 9;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            Leveling.text = "Level 5";
            FFA.questy = 12;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            Leveling.text = "Level 6";
            FFA.questy = 15;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            Leveling.text = "Level 7";
            FFA.questy = 18;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Leveling.text = "Level 8";
            FFA.questy = 21;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Leveling.text = "Level 8";
            FFA.questy = 21;
        }
        questsChanger();
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
            FFA.questy++;
            other.gameObject.SetActive(false);
            questsChanger();
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
            if(FFA.questy == 6)
            {
                FFA.questy = 7;
                questsChanger();
            }
        }
        else if(collision.gameObject.tag =="sea")
        {
            resetpoz = true;
            GameObject[] Lode = GameObject.FindGameObjectsWithTag("Lodmala");
            for(int i = 0; i < Lode.Length; i++)
            {
                if(Lode[i].name == "Lod_player")
                {
                    Lode[i].transform.position = new Vector3(81.19f, 1.943f, 16.36f);
                    Lode[i].transform.rotation = Quaternion.LookRotation(Vector3.right);
                }
                else
                {
                    Lode[i].transform.position = new Vector3(76.03f, 1.943f, 111.25f);
                    Lode[i].transform.rotation = Quaternion.LookRotation(Vector3.forward);
                }
            }
        }
        questsChanger();
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
    private void questsChanger()
    {
        switch(FFA.questy)
        {
            //Tutor
            case 0:
                questing.text = "Use SpaceBar to jump across gap between platforms.";
                break;
            case 1:
                questing.text = "Use Shift to run to next platform.";
                break;
            case 2:
                questing.text = "Use E to push the button.";
                break;
            //Neon1
            case 3:
                questing.text = "Find a Key.";
                break;
            case 4:
                questing.text = "Get out of office.";
                break;
            case 5:
                questing.text = "Find your time transporter and press E.";
                break;
            //Pir1
            case 6:
                questing.text = "Get on second Island.";
                break;
            case 7:
                questing.text = "Get on Pirats boat.";
                break;
            case 8:
                questing.text = "Take the item you want.";
                break;
            //Pir2
            case 9:
                questing.text = "Find something you can use to get on second island";
                break;
            case 10:
                questing.text = "Jump across boxes.";
                break;
            case 11:
                questing.text = "Get the hell out of here with your car.";
                break;
            //Ves1
            case 12:
                questing.text = "Find Adolf and talk to him.";
                break;
            case 13:
                questing.text = "Find Edvard and talk to him.";
                break;
            case 14:
                questing.text = "Take the unique horse and go to the forest";
                break;
            //Les
            case 15:
                questing.text = "Sreach the Forest.";
                break;
            case 16:
                questing.text = "You see the item you need take it.";
                break;
            case 17:
                questing.text = "Now go back to your horse.";
                break;
            //Ves2
            case 18:
                questing.text = "Find out who stole your car.";
                break;
            case 19:
                questing.text = "Give him what he want.";
                break;
            case 20:
                questing.text = "Go to your car.";
                break;
            //Neon2
            case 21:
                questing.text = "Go to office and show to your boss the items you found in past.";
                break;
            case 22:
                questing.text = "You got the money go and buy Game you want.";
                break;
            case 23:
                questing.text = "Press E and start playing.";
                break;
        }
    }
    bool CheckGrounded()
    {
            // Use the actual scaled radius of the SphereCollider for the SphereCast
            float scaledRadius = GetComponent<SphereCollider>().radius * transform.lossyScale.x;

            // A simple spherecast to check if the sphere is grounded
            RaycastHit hit;
            Vector3 castOrigin = transform.position + Vector3.up * scaledRadius * 0.5f;

            // Use the LayerMask parameter to filter only the specified ground layers
            return Physics.SphereCast(castOrigin, scaledRadius, Vector3.down, out hit, scaledRadius * 0.5f + 0.1f, groundLayer);
    }
}