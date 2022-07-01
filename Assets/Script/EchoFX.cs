using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoFX : MonoBehaviour
{
    private float timeBtwEchos;
    public float startTimeBtwEchos;
    public GameObject echo;
    //Projectile projectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwEchos <= 0)
        {
            GameObject instance = Instantiate(echo, transform.position, Quaternion.identity);
            Destroy(instance, 3f);
            timeBtwEchos = startTimeBtwEchos;
        }
        else
        {
            timeBtwEchos -= Time.deltaTime;
        }
    }
}
