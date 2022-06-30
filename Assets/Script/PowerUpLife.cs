using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLife : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.Instance.tower.LifeUp();
    }
    public void Use()
    {

    }
}
