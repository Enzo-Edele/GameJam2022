using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] int life;
    public List<GameObject> neighbor;

    /*[HideInInspector]*/public bool isSpine;
    public int spineIndex;

    private void Update()
    {
        CleanNeighbor();
        if (isSpine)
            GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void OnMouseDown()
    {
        Hit();
    }
    void Hit()
    {
        life--;
        if (life <= 0)
            Destruct();
    }

    public void SetNeighbor(int index, List<GameObject> homies)
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
}
