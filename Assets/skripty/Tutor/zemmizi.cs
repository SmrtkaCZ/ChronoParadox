using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zemmizi : MonoBehaviour
{
    private float yourDadsspeedwhenhefindoutaboutu = 0.005F;
    private float rych = 0;
    private bool zmiz = false;
    private Vector3 stpoz;
    // Update is called once per frame
    private void Start()
    {
        stpoz = transform.position;
    }
    void Update()
    {
        if (zmiz)
        {
            if(transform.position.y <= -10F)
            {
                zmiz = false;
                transform.position = stpoz;
                rych = 0;
            }
            rych += yourDadsspeedwhenhefindoutaboutu * Time.deltaTime;
            transform.position = new Vector3(transform.position.x , transform.position.y - rych, transform.position.z);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            zmiz = true;
        }
    }
}
