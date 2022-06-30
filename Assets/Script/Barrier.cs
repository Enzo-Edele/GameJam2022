using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    int life;

    void Start()
    {
        Vector2 pos = new Vector2(0, 0);
        pos.x = 0;
        pos.y = GameManager.Instance.brickHeight * (GameManager.Instance.tower.height / 2f) + GameManager.Instance.tower.gameObject.transform.position.y;
        transform.position = pos;

        Vector2 scale = transform.localScale;
        scale.x = GameManager.Instance.brickWidth * GameManager.Instance.tower.width + 0.3f;
        scale.y = GameManager.Instance.brickHeight * GameManager.Instance.tower.height + 0.3f;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeLife(-1);
    }

    public void ChangeLife(int change)
    {
        life += change;
        if (life <= 0)
            gameObject.SetActive(false);
        else if (life == 1)
        {
            gameObject.SetActive(true);
            GetComponent<SpriteRenderer>().color = Color.grey;
        }
        else if (life == 2)
            GetComponent<SpriteRenderer>().color = Color.cyan;
        else if (life == 3)
            GetComponent<SpriteRenderer>().color = Color.blue;
    }
}
