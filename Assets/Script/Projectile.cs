using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Projectile : MonoBehaviour
{
    public GameObject                       popUpScore;
    [SerializeField] GameObject             projectile;
    Rigidbody2D                             rb;
    public int                              scoreIntTxt = 100;
    public float                            multiplyer;
    Vector2 originalSpeed = new Vector2(2, -2);
    void Start()
    {

       rb = projectile.GetComponent<Rigidbody2D>();
       rb.AddForce(originalSpeed * 80);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        //Trigger PopUP
        if (popUpScore)
        {
            ShowScore();
        }
        //GameObject.Destroy(projectile);
    }
    private void ShowScore()
    {
        var go = Instantiate(popUpScore, rb.position, Quaternion.identity);
        go.GetComponent<TextMeshPro>().text = scoreIntTxt.ToString();
    }
}
