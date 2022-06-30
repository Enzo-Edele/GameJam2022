using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Projectile : MonoBehaviour
{

    float randNum;
    private int reboundCount;
    public GameObject popUpScore;
    //[SerializeField] GameObject             projectile;
    Rigidbody2D rb;
    public int scoreIntTxt = 100;
    public float multiplyer;
    //Vector2 originalSpeed = new Vector2(,);
    Vector2 Direction;
    float speed;

    void Start()
    {
        randNum = Random.Range(-5f, 5f);
        rb = this.GetComponent<Rigidbody2D>();
        Direction = new Vector2((0) - rb.position.x , (0 + randNum) - rb.position.y);
        rb.AddForce(Direction.normalized * 8, ForceMode2D.Impulse);
        GetComponentInParent<ProjectileSpawner>().projectiles.Add(gameObject);
        speed = GameManager.Instance.spawner.projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (reboundCount >= 3)
        {
            Destroy(this.gameObject);
        }
        /*else if (rb.position.x > 0 || rb.position.x > 1 || rb.position.y > 1 || rb.position.y < 0)
        {
            Destroy(this.gameObject);
        }*/
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
        if (collision.gameObject.layer == 6)
        {
            reboundCount++;
        }
    }
}
