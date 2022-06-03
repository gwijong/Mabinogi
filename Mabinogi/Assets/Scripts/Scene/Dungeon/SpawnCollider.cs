using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollider : MonoBehaviour
{
    //�浹�� ���� �ѹ� ������
    private void OnTriggerEnter(Collider other)
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.dungeon_monster_appear1, transform.position); //���� ���� ȿ����
        FindObjectOfType<Dungeon>().Spawn();//���� ����
        gameObject.SetActive(false); //���� �� ���� ���ӿ�����Ʈ ��Ȱ��ȭ
    }
}
