using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �ش� ��ư�� ������ ��ư�� �÷��̾��� ��ȣ�ۿ�</summary>
public class NameButton : MonoBehaviour
{
    /// <summary> ��ȣ�ۿ� ��ư �޼���</summary>
    public void GetItem()
    {
        GameManager.manager.GetComponent<PlayerController>().SetTarget(GetComponentInParent<Interactable>());        
    }
}
