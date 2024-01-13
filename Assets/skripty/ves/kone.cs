using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kone : MonoBehaviour
{
    private float plus = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.rotation.eulerAngles.x >= 315&&plus>0)
        {
            plus *= -1;
        }
        else if (gameObject.transform.rotation.eulerAngles.x <= 260&&plus < 0)
        {
            plus *= -1;
        }
        transform.eulerAngles = new Vector3(transform.eulerAngles.x+plus*Time.deltaTime, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
