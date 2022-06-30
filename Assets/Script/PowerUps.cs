using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
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
        int rnd = Random.Range(0, 4);
        power = (Power)rnd;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (rnd == 0)
            renderer.color = Color.cyan;
        if (rnd == 1)
            renderer.color = Color.gray;
        if (rnd == 2)
            renderer.color = Color.red;
        if (rnd == 3)
            renderer.color = Color.green;
    }

    private void OnMouseDown()
    {
        Use();
    }
    void Use()
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
        Destroy(gameObject);
    }
}
