using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �ش� ��ư�� ������ ��ư�� �÷��̾��� ��ȣ�ۿ�</summary>
public class NameButton : MonoBehaviour
{
    /// <summary> ��ȣ�ۿ� ��ư �޼���</summary>
    public void GetItem()
    {
        //��Ʈ Ű�� �̸� �����Ҷ� �̸� ��ư ������ ��ȣ�ۿ��ϴ� �޼���
        GameManager.manager.GetComponent<PlayerController>().SetTarget(GetComponentInParent<Interactable>());        
    }
}
