using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject brick;
    public int height = 3;
    public int width = 3;

    public List<GameObject> spine = new List<GameObject>();
    private void Start()
    {
        BuildTower();
    }
    private void Update()
    {
        for (int i = 0; i < spine.Count; i++)
            if (spine[i] == null)
                spine.RemoveAt(i);
    }

    void BuildTower()
    {
        Vector2 position = transform.position;

        List<GameObject> floor = new List<GameObject>();
        for(int i = 0; i < height; i++)
        {
            GameObject spineBrick = Instantiate(brick, new Vector2(position.x, position.y + i * GameManager.Instance.brickHeight), Quaternion.identity, transform);//mettre brick height
            floor.Add(spineBrick);
            spine.Add(spineBrick);
            spine[spine.Count - 1].GetComponent<Brick>().spineIndex = spine.Count;

            for (int j = 0; j < (int)width/2; j++)
            {
                floor.Add(Instantiate(brick, new Vector2(position.x + (j + 1) * GameManager.Instance.brickWidth, position.y + i * GameManager.Instance.brickHeight), Quaternion.identity, transform));//mettre brick width
                floor.Add(Instantiate(brick, new Vector2(position.x - (j + 1) * GameManager.Instance.brickWidth, position.y + i * GameManager.Instance.brickHeight), Quaternion.identity, transform));
            }
            SetFloor(floor);
            floor.Clear();
        }
    }
    void SetFloor(List<GameObject> floorBricks)
    {
        Brick brick;
        for(int i = 0; i < floorBricks.Count; i++)
        {
            brick = floorBricks[i].GetComponent<Brick>();
            brick.SetNeighbor(i, floorBricks);
            if (i == 0)
                brick.isSpine = true;
        }
    }
    public void TowerDown(int index)
    {
        for (int i = index; i < spine.Count; i++)
        {
            spine[i].GetComponent<Brick>().BringDown();
        }
        if (spine.Count <= 0)
            Debug.Log("Loose");
    }
}
