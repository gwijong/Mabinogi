using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBgmCollider : MonoBehaviour
{
    /// <summary> ������ ����������� �������ִ� Ʈ���� </summary>
    private void OnTriggerEnter(Collider other)
    {
        GameManager.soundManager.PlayBgmPlayer(Define.Scene.Boss);
    }
}
