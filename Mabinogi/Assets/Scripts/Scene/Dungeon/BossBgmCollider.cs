using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ������ Ʈ���� �浹�ϸ� ������ ������� ����/summary>
public class BossBgmCollider : MonoBehaviour
{
    /// <summary> ������ ����������� �������ִ� Ʈ���� </summary>
    private void OnTriggerEnter(Collider other)
    {
        GameManager.soundManager.PlayBgmPlayer(Define.Scene.Boss);
    }
}
