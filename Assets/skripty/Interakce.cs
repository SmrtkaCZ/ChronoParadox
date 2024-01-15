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
    

    
    private float rych = 0.01f;
    GameObject[] krab;
    GameObject Dialog;
    public TMP_Text Dilaogovytext;
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
        else if (SceneManager.GetActiveScene().buildIndex == 6 || SceneManager.GetActiveScene().buildIndex == 8)
        {
            Dialog = GameObject.Find("Texting");
            if(gameObject.name == "V")
            {
                FFA.V1 = true;
            }
            else if (gameObject.name == "V1")
            {
                FFA.V2 = true;
            }
            else if(gameObject.name == "V2")
            {
                FFA.V3 = true;
            }
            else if(gameObject.name == "V3")
            {
                FFA.V4 = true;
            }

            if(FFA.V1&&FFA.V2&&FFA.V3&&FFA.V4)
            {
                /*GameObject textovyobjekt = GameObject.Find("Text");
                Dilaogovytext = textovyobjekt.GetComponent<TMP_Text>();*/
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
            else if(gameObject.name == "Car"||gameObject.name == "mince"|| gameObject.name == "ves" && FFA.Neviltalk1)
            {
                FFA.body += FFA.bodyvlevelu;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
                akce = false;
            }
            else if (gameObject.name == "Key")
            {
                gameObject.SetActive(false);
                FFA.key = true;
                FFA.questy++;
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
                
                if (SceneManager.GetActiveScene().buildIndex == 6)
                {
                    if (gameObject.name == "V")
                    {
                        Dilaogovytext.text =
                            "Benito: \n" +
                            "Vítej v Mnichovì.\n " +
                            "Hitler je mùj velký kamarád.";
                    }
                    else if (gameObject.name == "V1")
                    {
                        if(FFA.Adolftalk1)
                        {
                            Dilaogovytext.text =
                                "Edvard: \n" +
                                "Vítej v Mnichovì.\n " +
                                "Konì toho ti musím pùjèit, když to øíka Adolf. \n"+
                                "Vem si tohodle, který je vedle mne";
                            if(!FFA.Edvardtalk1)
                            {
                                FFA.questy++;
                                FFA.Edvardtalk1 = true;
                            }
                        }
                        else
                        {
                            Dilaogovytext.text =
                                "Edvard: \n" +
                                "Vítej v Mnichovì.\n " +
                                "Videl jsem nìco zvláštního v horách.";
                        }
                    }
                    else if (gameObject.name == "V2")
                    {
                        Dilaogovytext.text =
                            "Nevil: \n" +
                            "Vítej v Mnichovì.\n " +
                            "Jo mít moc je super vìc.";
                    }
                    else if (gameObject.name == "V3")
                    {
                        Dilaogovytext.text =
                            "Adolf: \n" +
                            "Vítej v Mnichovì.\n " +
                            "Vidím, že hledáš konì, zajdi za Evardem ten ti jistì nejákého pùjèí.";
                        if(!FFA.Adolftalk1)
                        {
                            FFA.Adolftalk1 = true;
                            FFA.questy++;
                        }
                    } 
                }
                else if (SceneManager.GetActiveScene().buildIndex == 8)
                {
                    if (gameObject.name == "V")
                    {
                        Dilaogovytext.text =
                            "Benito: \n" +
                            "Vítej v Mnichovì.\n " +
                            "Hitler je mùj velký kamarád.";
                    }
                    else if (gameObject.name == "V1")
                    {
                        Dilaogovytext.text =
                            "Edvard: \n" +
                            "Vítej v Mnichovì.\n " +
                            "Konì mí koòové.";
                    }
                    else if (gameObject.name == "V2")
                    {
                        if(FFA.Adolftalk2) 
                        {
                            Dilaogovytext.text =
                            "Nevil: \n" +
                            "Vítej v Mnichovì.\n " +
                            "Auto je za domem";
                            if(!FFA.Neviltalk1)
                            {
                                FFA.Neviltalk1 = true;
                                FFA.questy++;
                            }
                        }
                        else
                        {
                            Dilaogovytext.text =
                            "Nevil: \n" +
                            "Vítej v Mnichovì.\n " +
                            "Jo mít moc je super vìc.";
                        }
                        
                    }
                    else if (gameObject.name == "V3")
                    {
                        Dilaogovytext.text =
                            "Adolf: \n" +
                            "Vítej v Mnichovì.\n " +
                            "Vidím, že hledáš Auto zajdi za Nevilem.";
                        if(!FFA.Adolftalk2)
                        {
                            FFA.Adolftalk2 = true;
                            FFA.questy++;
                        }
                    }
                }

                ididit = true;
                akce = false;
            }
            else if(gameObject.name == "Vaza")
            {
                FFA.questy++;
                FFA.do7 = true;
                gameObject.SetActive(false);
                akce = false;
            }
            //neon (2)
            else if(gameObject.name == "Milan" )
            {
                gameObject.SetActive(false);
                FFA.questy++;
                akce = false;
            }
            else if (gameObject.name == "stanek")
            {
                FFA.questy++;
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
            if (SceneManager.GetActiveScene().buildIndex == 6 || SceneManager.GetActiveScene().buildIndex == 8)
            {
                Dialog.gameObject.SetActive(false);
            }
        }
    }
}
