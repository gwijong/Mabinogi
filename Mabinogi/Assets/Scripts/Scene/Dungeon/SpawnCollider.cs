using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollider : MonoBehaviour
{
    //�浹�� ���� �ѹ� ������
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<Dungeon>().Spawn();
        gameObject.SetActive(false);
    }
}
