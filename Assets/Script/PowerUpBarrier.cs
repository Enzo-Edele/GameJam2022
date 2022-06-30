using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBarrier : MonoBehaviour
{
    GameObject barrier;

    private void Start()
    {
        barrier = GameManager.Instance.barrier;
    }

    private void OnMouseDown()
    {
        Use();
    }
    public void Use()
    {
        barrier.SetActive(true);
    }
}
