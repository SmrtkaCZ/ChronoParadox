using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Nakary;
    private float timer = 0;
    private float spawnrate = 100000;
    // Start is called before the first frame update
    void Start()
    {
        timer = spawnrate;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnrate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            Kary();
            timer = 0;
        }
    }
    private void Kary()
    {
        Instantiate(Nakary, transform.position, transform.rotation);
    }
}
