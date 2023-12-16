using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnKlik : MonoBehaviour
{
    GameObject Panel;
    GameObject hrac;
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Panel = GameObject.FindGameObjectWithTag("Panel");
            Panel.gameObject.SetActive(false);
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            
        }
    }
    public void StartClick(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }
    public void OhreClick(bool hoj)
    {
        
        Panel.SetActive(hoj);
    }
    public void ExitClick()
    {
        Application.Quit();
    }
    public void ResetPozice()
    {
        Time.timeScale = 1;
        FFA.resetpozic = true;
        Time.timeScale = 0;
    }
}
