using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ���� ������ ������</summary>
public class FieldItem : Interactable
{
    public override Define.InteractType Interact(Interactable other)
    {
        Debug.Log("ItemGether");
        return Define.InteractType.Get; //�ֿ� �� �ִ� ������
    }
}
