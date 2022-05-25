using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ���� ���� ���� �� �ѹ� ����Ǵ� �ڵ��� </summary>
public class FirstGate : MonoBehaviour
{
    public BoxCollider gateCollider;
    public GameObject[] doors;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == (int)Define.Layer.Player)
        {
            doors[0].SetActive(false);
            doors[1].SetActive(false);
            gateCollider.enabled = false;
        }
    }
}
