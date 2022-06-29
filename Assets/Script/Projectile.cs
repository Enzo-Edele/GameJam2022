using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Projectile : MonoBehaviour
{
   
    [SerializeField] GameObject             projectile;
    Rigidbody2D                             rb;
    public float                            speed;
    public float                            force;
    Vector2 originalSpeed = new Vector2(2, -2);
    void Start()
    {
       rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(originalSpeed * 200);
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    private void OnMouseDown()
    {
        GameObject.Destroy(projectile);
    }
}
