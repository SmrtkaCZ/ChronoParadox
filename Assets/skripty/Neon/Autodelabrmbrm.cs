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
        

        if (rych >= 1)
        {
            rych = 1;
        }
        else
        {
            rych += brmbrm * Time.deltaTime;
        }

        if (transform.rotation.eulerAngles.y == 0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + rych);  
        }
        else if (transform.rotation.eulerAngles.y == 90F)
        {
            transform.position = new Vector3(transform.position.x + rych, transform.position.y, transform.position.z);
        }
        else if(transform.rotation.eulerAngles.y == 180F)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - rych);
        }
        else if(transform.rotation.eulerAngles.y == 270F)
        {
            transform.position = new Vector3(transform.position.x - rych, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CarDestroyer")
        {
            Destroy(gameObject);
        }
    }
}
