using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interakce : MonoBehaviour
{
    public TMP_Text text;
    
    private bool ididit = true;
    private bool tohokohopotrebujisity = false;
    private bool akce = false;
    private float rych = 0.01f;
    GameObject[] krab;
    // Start is called before the first frame update
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 5 && gameObject.name == "krabice venku")
        {
            krab = GameObject.FindGameObjectsWithTag("krabice");
            for (int i = 0; i < krab.Length; i++)
            {
                krab[i].gameObject.SetActive(false);
            }
        }
    }
    private void Update()
    {
        if(FFA.pressedE && tohokohopotrebujisity)
        {
            akce = true;
            ididit = false;
            FFA.pressedE = false;
        }

        //allActions
        if (akce)
        {
            text.gameObject.SetActive(false);
            //Tutor
            if (gameObject.name == "Buttontofirstlevel")
            {
                rych += rych * Time.deltaTime;
                if (transform.position.y < 4.419F)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + rych, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, 4.149F, transform.position.z);
                    akce = false;
                }
            }
            //neon(1)
            else if(gameObject.name == "Car")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
                akce = false;
            }
            //pirati (2)
            else if (gameObject.name == "krabice venku")
            {
                for(int i = 0; i < krab.Length; i++)
                {
                    krab[i].gameObject.SetActive(true);
                }
                gameObject.SetActive(false);
                
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(ididit)
            {
                text.gameObject.SetActive(true);
            }
            tohokohopotrebujisity = true;
            FFA.ucaninter = ididit;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tohokohopotrebujisity = false;
            FFA.ucaninter = false;
            text.gameObject.SetActive(false);
        }
    }
}
