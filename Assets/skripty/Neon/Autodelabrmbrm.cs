using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodelabrmbrm : MonoBehaviour
{
    [SerializeField]
    private float brmbrm = 0.1F;
    private float rych;
    private float brrrm = 0;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        rych += brmbrm * Time.deltaTime;
        if(brrrm > 5) 
        {
            transform.Rotate(0, 90, 0);
            brrrm = 0;
            Debug.Log(transform.rotation.y);
        }
        if (transform.rotation.y == 0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + rych);  
        }
        if (transform.rotation.y == 0.7071068F)
        {
            transform.position = new Vector3(transform.position.x + rych, transform.position.y, transform.position.z);
        }
        if (transform.rotation.y == 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - rych);
        }
        if (transform.rotation.y == -0.7071068F)
        {
            transform.position = new Vector3(transform.position.x - rych, transform.position.y, transform.position.z);
        }
        brrrm+= Time.deltaTime; 
    }
}
