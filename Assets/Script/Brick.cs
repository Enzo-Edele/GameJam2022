using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int life;
    public List<Brick> neighbor;

    /*[HideInInspector]*/public bool isSpine;
    public int spineIndex;

    private void Update()
    {
        CleanNeighbor();
        /*if (isSpine)
            GetComponent<SpriteRenderer>().color = Color.red;*/
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        LifeChange(-1);
    }
    void LifeChange(int change)
    {
        life += change;
        if (life <= 0)
            Destruct();
        else if (life == 1)
            GetComponent<SpriteRenderer>().sprite = GameManager.Instance.brickSprite[0];
        else if (life == 2)
            GetComponent<SpriteRenderer>().sprite = GameManager.Instance.brickSprite[1];
    }

    public void SetNeighbor(int index, List<Brick> homies)
    {
        for(int i = 0; i < homies.Count; i++)
            if (i != index)
                neighbor.Add(homies[i]);
    }
    void Destruct()
    {
        if (isSpine && neighbor.Count > 0)
        {
            neighbor[0].GetComponent<Brick>().isSpine = true;
            neighbor[0].GetComponent<Brick>().spineIndex = spineIndex;
            GetComponentInParent<Tower>().spine[spineIndex - 1] = neighbor[0];
        }
        else if (isSpine && neighbor.Count <= 0)
            GetComponentInParent<Tower>().TowerDown(spineIndex);
        Destroy(gameObject);
    }
    public void CleanNeighbor()
    {
        for (int i = 0; i < neighbor.Count; i++)
            if (neighbor[i] == null)
                neighbor.RemoveAt(i);
    }
    public void BringDown()
    {
        Vector3 pos = transform.position;
        pos.y -= GameManager.Instance.brickHeight;
        transform.position = pos;
        for (int i = 0; i < neighbor.Count; i++)
        {
            pos.x = neighbor[i].transform.position.x;
            neighbor[i].transform.position = pos;
        }
        spineIndex--;
    }
    public bool checkFlooreLives(int check)
    {
        bool changed = false;
        if (life <= check)
        {
            check = life;
            changed = true;
        }
        for(int i = 0; i < neighbor.Count; i++)
        {
            if (neighbor[i].life <= check && !changed)
            {
                check = neighbor[i].life;
                changed = true;
            }
        }
        if (changed)
        {
            LifeChange(1);
            for (int i = 0; i < neighbor.Count; i++)
            {
                neighbor[i].LifeChange(1);
            }
        }

        return changed;
    }
}
