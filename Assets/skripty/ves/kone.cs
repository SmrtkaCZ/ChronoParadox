using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kone : MonoBehaviour
{
    private float plus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.rotation.eulerAngles.x >= 315f)
        {
            plus = 260;
        }
        else if (gameObject.transform.rotation.eulerAngles.x <= 260f)
        {
            plus = 315;
        }
        transform.rotation = Quaternion.Euler(plus, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
