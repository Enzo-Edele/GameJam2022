using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Projectile : MonoBehaviour
{
    private int                             reboundCount;
    public GameObject                       popUpScore;
    //[SerializeField] GameObject             projectile;
    Rigidbody2D                             rb;
    public int                              scoreIntTxt = 100;
    public float                            multiplyer;
    Vector2 originalSpeed = new Vector2(2, -2);
    void Start()
    {

       rb = this.GetComponent<Rigidbody2D>();
       rb.AddForce(originalSpeed * 80);
    }

    // Update is called once per frame
    void Update()
    {
        if(reboundCount >= 3)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnMouseDown()
    {
        //Trigger PopUP
        if (popUpScore)
        {
            ShowScore();
        }
        GameObject.Destroy(this.gameObject);
    }
    private void ShowScore()
    {
        var go = Instantiate(popUpScore, rb.position, Quaternion.identity);
        go.GetComponent<TextMeshPro>().text = scoreIntTxt.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            reboundCount++;
        }
    }
}
