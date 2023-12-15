using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnKlik : MonoBehaviour
{
    GameObject Panel;
    private void Start()
    {
        Panel = GameObject.FindGameObjectWithTag("Panel");
        Panel.SetActive(false);
    }
    public void StartClick()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void OhreClick(bool hoj)
    {
        Panel.SetActive(hoj);
    }
    public void ExitClick()
    {
        Application.Quit();
    }
}
