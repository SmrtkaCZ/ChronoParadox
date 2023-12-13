using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodelabrmbrm : MonoBehaviour
{
    [SerializeField]
    private float brmbrm = 0.1F;
    private float rych;

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        rych += brmbrm * Time.deltaTime;
        if (transform.rotation.y == 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + rych);  
        }
        else if (transform.rotation.y == 90)
        {
            transform.position = new Vector3(transform.position.x + rych, transform.position.y, transform.position.z);
        }
        else if (transform.rotation.y == 180)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - rych);
        }
        else if (transform.rotation.y == 270)
        {
            transform.position = new Vector3(transform.position.x - rych, transform.position.y, transform.position.z);
        }
    }
}
