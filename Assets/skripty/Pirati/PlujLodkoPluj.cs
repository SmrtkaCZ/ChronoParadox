using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlujLodkoPluj : MonoBehaviour
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
        if(transform.childCount > 0)
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
                transform.position = new Vector3(transform.position.x - rych, transform.position.y, transform.position.z);
                transform.GetChild(0).position = new Vector3(transform.position.x - rych, transform.GetChild(0).position.y, transform.position.z);
            }
            else if (transform.rotation.eulerAngles.y == 90f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + rych);
                transform.GetChild(0).position = new Vector3(transform.position.x, transform.GetChild(0).position.y, transform.position.z + rych);
            }
            else if (transform.rotation.eulerAngles.y == 180F)
            {
                transform.position = new Vector3(transform.position.x + rych, transform.position.y, transform.position.z);
                transform.GetChild(0).position = new Vector3(transform.position.x + rych, transform.GetChild(0).position.y, transform.position.z);
            }
            else if (transform.rotation.eulerAngles.y == 270F)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - rych);
                transform.GetChild(0).position = new Vector3(transform.position.x, transform.GetChild(0).position.y, transform.position.z - rych);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "ChangeRotationOfBoat")
        {
            transform.eulerAngles = new Vector3(0f,90f,0f);
            rych = 0f;
            transform.GetChild(0).parent = null;
        }
        if (other.gameObject.name == "ChangeRotationOfBoat1")
        {
            transform.eulerAngles = new Vector3(0f,-90f, 0f);
            rych = 0f;
            transform.GetChild(0).parent = null;
        }
        if (other.gameObject.name == "ChangeRotationOfBoat2")
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            rych = 0f;
            transform.GetChild(0).parent = null;
        }
        if (other.gameObject.name == "ChangeRotationOfBoat3")
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            rych = 0f;
            transform.GetChild(0).parent = null;
        }

    }
}
