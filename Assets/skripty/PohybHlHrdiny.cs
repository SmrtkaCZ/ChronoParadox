using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PohybHlHrdiny : MonoBehaviour
{
    [Header("Možnosti pohybu")]
    [SerializeField]
    public int rychlost;
    public float nahoru;

    Rigidbody teleso;
    Vector3 pohybFinal;
    private int pocetbodu;
    private bool skociljsem = true;
    private bool con = false;


    void Start()
    {
        teleso = GetComponent<Rigidbody>();
        Time.timeScale = 1;
    }

    void FixedUpdate()
    {
        teleso.AddForce(pohybFinal * rychlost);
    }

    public void Movement(InputAction.CallbackContext kontext)
    {
        Vector2 pomocny = kontext.ReadValue<Vector2>();
        pohybFinal = new Vector3(pomocny.x, 0f, pomocny.y);
    }
    public void Jump(InputAction.CallbackContext kontext)
    {
        if (skociljsem && kontext.performed)
        {
            teleso.AddForce(Vector3.up * nahoru);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            skociljsem = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            skociljsem = false;
        }
    }
}
