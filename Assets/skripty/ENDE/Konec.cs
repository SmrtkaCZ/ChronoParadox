using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Konec : MonoBehaviour
{
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = FFA.body + " / 8";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
