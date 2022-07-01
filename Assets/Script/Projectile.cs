using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Projectile : MonoBehaviour
{
    private float                           randNum;
    private int                             reboundCount;
    public GameObject                       popUpScore;
    public GameObject                       boomAnim;
    private Rigidbody2D                     rb;
    public int                              scoreIntTxt = 100;
    public float                            speed;
    public float                            multiplyer;
    Vector2                                 Direction;
    Vector2                                 offSet;
    Transform spriteRot;

    void Start()
    {
        offSet = new Vector2(0, 1);
        spriteRot = this.GetComponent<SpriteRenderer>().transform;

        //physique au start
        rb = this.GetComponent<Rigidbody2D>();
        randNum = Random.Range(-5f, 5f);
        Direction = new Vector2((0) - rb.position.x , (0 + randNum) - rb.position.y);
        rb.AddForce(Direction.normalized * 8, ForceMode2D.Impulse);

        GetComponentInParent<ProjectileSpawner>().projectiles.Add(gameObject);
        speed = GameManager.Instance.spawner.projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //spinning Sprite
        spriteRot.transform.Rotate(0, 0, -450 * Time.deltaTime);
        if (reboundCount >= 3)
        {
            Destroy(this.gameObject);
        }
        /*else if (rb.position.x > 0 || rb.position.x > 1 || rb.position.y > 1 || rb.position.y < 0)
        {
            Destroy(this.gameObject);
        }*/
        if (transform.position.magnitude > 20.0f)
        {
            Destroy(gameObject);
        }
    }
    private void OnMouseDown()
    {
        GameManager.Instance.UpdateScore(100);
        SoundManager.Instance.Play("Shoot");
        //Trigger Boom
        if (boomAnim)
        {
            Boom();
        }
        //Trigger PopUP
        if (popUpScore)
        {
            ShowScore();
        }

        GameObject.Destroy(this.gameObject);

        //PowerUp
        int rnd = Random.Range(0, GameManager.Instance.dropRate);
        if(rnd == 0)
            Instantiate(GameManager.Instance.powerUpPrefab, transform.position, Quaternion.identity);
    }
    private void ShowScore()
    {
        var go = Instantiate(popUpScore, rb.position + offSet, Quaternion.identity);
        go.GetComponent<TextMeshPro>().text = scoreIntTxt.ToString();
    }

    private void Boom()
    {
        var go = Instantiate(boomAnim, rb.position, Quaternion.identity);
        Destroy(go, 0.6f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            SoundManager.Instance.Play("Rebound");
            reboundCount++;
        }
    }
}
