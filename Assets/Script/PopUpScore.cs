using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpScore : MonoBehaviour
{
    private TextMeshPro text;
    private float timer;
    private float timerMax = 0.6f;
    private Vector2 moveDir;
    Vector2 offSet = new Vector2(0, 1);

    private Color textColor;

    private void Awake()
    {
        text = transform.GetComponent<TextMeshPro>();
    }
    void Start()
    {
        timer = timerMax;
        moveDir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * 10.0f;
    }
    void Update()
    {
        transform.position += (Vector3)moveDir * Time.deltaTime;
        moveDir -= moveDir * 5 * Time.deltaTime;

        if (timer > timerMax * .5f)
        {
            float increaseScaleAmount = 1.1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1.5f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            float fadeAmount = 3f;
            textColor.a -= fadeAmount * Time.deltaTime;
            text.color = textColor;
            if (text.color.a <= 0)
                Destroy(gameObject);
        }
    }
}
