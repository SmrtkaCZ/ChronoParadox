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

        FFA.bodyvlevelu = 0;
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
            FFA.key = false;
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
            FFA.do7 = false;
            FFA.questy = 12;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            Leveling.text = "Level 6";
            FFA.questy = 15;
            FFA.V1 = false;
            FFA.V2 = false;
            FFA.V3 = false;
            FFA.V4 = false;
            FFA.Adolftalk1 = false;
            FFA.Edvardtalk1 = false;

        }
        else if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            Leveling.text = "Level 7";
            FFA.questy = 18;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 9)
        {
            Leveling.text = "Level 8";
            FFA.questy = 21;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 10)
        {
            Leveling.text = "Level 8";
            FFA.questy = 22;
        }
        questsChanger();
    }
    void Update()
    {
        if(FFA.questy == 23 && intErekce.triggered)
        {
            FFA.body += FFA.bodyvlevelu;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
        questsChanger();
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            jumpcount = 0;
            playerVelocity.y = 0f;
        }
        questsChanger();
        if (FFA.move)
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

        questsChanger();
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
        questsChanger();
        //dropped
        if (transform.position == FFA.StartPozice && VC1.Follow != null)
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
        questsChanger();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Point")
        {
            FFA.bodyvlevelu++;
            other.gameObject.transform.parent.gameObject.SetActive(false);
        }
        else if(other.gameObject.tag == "zmenaukolu")
        {
            FFA.questy++;
            other.gameObject.SetActive(false);
            questsChanger();
        }
        else if (other.gameObject.tag =="zakonem" && FFA.Edvardtalk1)
        {
            FFA.body += FFA.bodyvlevelu;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
        else if (other.gameObject.tag == "zakonem2" && FFA.do7)
        {
            FFA.body += FFA.bodyvlevelu;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PortalTutor"|| collision.gameObject.tag == "Dvere" && FFA.key || collision.gameObject.tag == "Dvere" && FFA.questy == 22)
        {
            FFA.body += FFA.bodyvlevelu;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
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
    }
    private void PauzaMenu(bool cs)
    {
        
        if(cs)
        {
            VC1.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
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
                questing.text = "Mezerník ti pomùže pøeskoèit díru mezi platfromama.";
                break;
            case 1:
                questing.text = "Shift ti pomùže pøebìhnout padající plošiny.";
                break;
            case 2:
                questing.text = "Zkus použít E u tlaèítka.";
                break;
            //Neon1
            case 3:
                questing.text = "Najdi klíè.";
                break;
            case 4:
                questing.text = "Jdi ven.";
                break;
            case 5:
                questing.text = "Najdi auto a odjeï pryè.";
                break;
            //Pir1
            case 6:
                questing.text = "Zkus se dostat na druhý ostrov.";
                break;
            case 7:
                questing.text = "Pøesuò se na pirátskou loï.";
                break;
            case 8:
                questing.text = "Vem si minci.";
                break;
            //Pir2
            case 9:
                questing.text = "Najdi cestu jak se dostat na druhý ostrov.";
                break;
            case 10:
                questing.text = "Skákej pøes krabice.";
                break;
            case 11:
                questing.text = "Nastartuj auto a odjeï.";
                break;
            //Ves1
            case 12:
                questing.text = "Najdi Adolfa a promluv si s ním.";
                break;
            case 13:
                questing.text = "Nadji Edvarda a promluv si s ním.";
                break;
            case 14:
                questing.text = "Naskoè na vùz za konìm a odjeï.";
                break;
            //Les
            case 15:
                questing.text = "Prohledej les.";
                break;
            case 16:
                questing.text = "Vem si vázu.";
                break;
            case 17:
                questing.text = "Vra se ke koni a odjeï do vesnice.";
                break;
            //Ves2
            case 18:
                questing.text = "Najdi Adolfa a promluv si s ním.";
                break;
            case 19:
                questing.text = "Najdi Nevila a promluv si s ním.";
                break;
            case 20:
                questing.text = "Nastartuj auto a odjeï.";
                break;
            //Neon2
            case 21:
                questing.text = "Dej šefovi vìci, které si našel v minulosti.";
                break;
            case 22:
                questing.text = "Vydìlal jsi si peníze. Jdi si koupit hru k obchodníkovi ven.";
                break;
            case 23:
                questing.text = "Zmáckni E a zaèni hrát.";
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