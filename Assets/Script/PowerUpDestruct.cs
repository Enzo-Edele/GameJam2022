using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDestruct : MonoBehaviour
{
    private void OnMouseDown()
    {
        Use();
    }
    public void Use()
    {
        GameManager.Instance.spawner.DestroyAll();
    }
}
