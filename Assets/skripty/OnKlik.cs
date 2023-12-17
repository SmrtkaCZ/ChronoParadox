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
    public GameObject hrac;
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Panel = GameObject.FindGameObjectWithTag("Panel");
            Panel.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if(Time.timeScale == 0f&&hrac.transform.position == FFA.StartPozice && SceneManager.GetActiveScene().buildIndex >= 1)
        {
            Time.timeScale = 0f;
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
        FFA.reset = true;
    }
}
