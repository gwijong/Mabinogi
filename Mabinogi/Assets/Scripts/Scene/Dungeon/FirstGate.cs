using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ���� ���� ���� �� �ѹ� ����Ǵ� �ڵ��� </summary>
public class FirstGate : MonoBehaviour
{
    /// <summary> �� �ݶ��̴� </summary>
    public BoxCollider gateCollider;
    /// <summary> ��¦�� </summary>
    public GameObject[] doors;
    /// <summary> ĳ���Ͱ� �� Ʈ���� �ݶ��̴��� �浹</summary>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == (int)Define.Layer.Player)
        {
            GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.dungeon_door,doors[0].transform.position); //�� ���� ȿ����
            doors[0].SetActive(false);//���� �� ��Ȱ��ȭ
            doors[1].SetActive(false);//������ �� ��Ȱ��ȭ
            gateCollider.enabled = false; //�� �浹 ��Ȱ��ȭ
        }
    }
}
