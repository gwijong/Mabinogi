using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ���� ������ ������</summary>
public class FieldItem : Interactable
{
    //other���� �� InteractType�� �����Ѵ�
    public override Define.InteractType Interact(Interactable other)
    {
        return Define.InteractType.Get; //�ֿ� �� �ִ� ������
    }
}
