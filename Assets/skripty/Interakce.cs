using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interakce : MonoBehaviour
{
    public TMP_Text text;
    
    private bool ididit = true;
    private bool tohokohopotrebujisity = false;
    private bool akce = false;
    private bool Adolftalk1 = false;
    private bool Adolftalk2 = false;
    private bool Edvardtalk3 = false;
    private float rych = 0.01f;
    GameObject[] krab;
    GameObject Dialog;
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
        else if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            Dialog = GameObject.Find("Texting");
            if(gameObject.name == "V3")
            {
                Dialog.SetActive(false);
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
            else if(gameObject.name == "Car"||gameObject.name == "mince")
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
                FFA.questy++;
                gameObject.SetActive(false);
                akce = false;

            }
            else if(gameObject.tag == "vesnican")
            {
                Dialog.gameObject.SetActive(true);
                TMP_Text Dilaogovytext = GameObject.Find("Text").GetComponent<TMP_Text>();
                if (SceneManager.GetActiveScene().buildIndex == 6)
                {
                    if (gameObject.name == "V0")
                    {
                        Dilaogovytext.text =
                            "Benito: \n" +
                            "V�tej v Mnichov�.\n " +
                            "Hitler je m�j velk� kamar�d.";
                    }
                    else if (gameObject.name == "V1")
                    {
                        if(Adolftalk1)
                        {
                            Dilaogovytext.text =
                                "Edvard: \n" +
                                "V�tej v Mnichov�.\n " +
                                "Kon� toho ti mus�m p�j�it, kdy� to ��ka Adolf.";
                            FFA.questy++;
                            Edvardtalk3 = true;
                        }
                        else
                        {
                            Dilaogovytext.text =
                                "Edvard: \n" +
                                "V�tej v Mnichov�.\n " +
                                "Videl jsem n�co zvl�tn�ho v hor�ch.";
                        }
                    }
                    else if (gameObject.name == "V2")
                    {
                        Dilaogovytext.text =
                            "Nevil: \n" +
                            "V�tej v Mnichov�.\n " +
                            "Jo m�t moc je super v�c.";
                    }
                    else if (gameObject.name == "V3")
                    {
                        Dilaogovytext.text =
                            "Adolf: \n" +
                            "V�tej v Mnichov�.\n " +
                            "Vid�m, �e hled� kon� zajdi az Evardem ten ti jist� nej�k�ho p�j��.";
                        FFA.questy++;
                        Adolftalk1 = true;
                    } 
                }
                else
                {
                    if (gameObject.name == "V0")
                    {
                        Dilaogovytext.text =
                            "Benito: \n" +
                            "V�tej v Mnichov�.\n " +
                            "Hitler je m�j velk� kamar�d.";
                    }
                    else if (gameObject.name == "V1")
                    {
                        Dilaogovytext.text =
                            "Edvard: \n" +
                            "V�tej v Mnichov�.\n " +
                            "Kon� m� ko�ov�.";
                    }
                    else if (gameObject.name == "V2")
                    {
                        Dilaogovytext.text =
                            "Nevil: \n" +
                            "V�tej v Mnichov�.\n " +
                            "Jo m�t moc je super v�c.";
                    }
                    else if (gameObject.name == "V3")
                    {
                        Dilaogovytext.text =
                            "Adolf: \n" +
                            "V�tej v Mnichov�.\n " +
                            "Vid�m, �e hled� kon� zajdi az Evardem ten ti jist� nej�k�ho p�j��.";
                    }
                }

                ididit = true;
                akce = false;
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
            Dialog.gameObject.SetActive(false);
        }
    }
}
