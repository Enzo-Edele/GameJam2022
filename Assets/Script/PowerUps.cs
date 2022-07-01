using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public Sprite sprite;
    public bool isSelect = false;
    Rigidbody2D rb;
    Vector2 customUp;

    public enum Power
    {
        Barrier,
        SlowMotion,
        Destroy,
        Life
    }
    private static Power power;

    private void Start()
    {
        customUp = new Vector2(Vector2.up.x + Random.Range(-2f, 2f), Vector2.up.y);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(customUp.normalized * 4, ForceMode2D.Impulse);
        int rnd = Random.Range(0, 4);
        power = (Power)rnd;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (rnd == 0)
            sprite = renderer.sprite = GameManager.Instance.powerUpSprite[0];
        if (rnd == 1)
            sprite = renderer.sprite = GameManager.Instance.powerUpSprite[1];
        if (rnd == 2)
            sprite = renderer.sprite = GameManager.Instance.powerUpSprite[2];
        if (rnd == 3)
            sprite = renderer.sprite = GameManager.Instance.powerUpSprite[3];
    }
    private void Update()
    {
        if (transform.position.magnitude > 20.0f && !isSelect)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        //Use();
        SoundManager.Instance.Play("PowerUPClicked");
        //UIManager.Instance.GetPowerUp(this, sprite);
    }
    public void Use()
    {
        switch (power)
        {
            case Power.Barrier:
                GameManager.Instance.barrier.GetComponent<Barrier>().ChangeLife(1);
                break;
            case Power.SlowMotion:
                GameManager.Instance.spawner.SlowMotion(true);
                break;
            case Power.Destroy:
                GameManager.Instance.spawner.DestroyAll();
                break;
            case Power.Life:
                GameManager.Instance.tower.LifeUp();
                break;
        }
    }
}
