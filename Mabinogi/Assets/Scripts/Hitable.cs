using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �´°� ������ ������Ʈ��</summary>
public class Hitable : Interactable 
{

    public override Define.InteractType Interact(Interactable other)  //���� �� �����Ƿ� InteractType.Attack ��ȯ 
    {
        return Define.InteractType.Attack;
    }
    /// <summary> �����ϴ� ������ ȣ���ϴ� ��� �´� �޼���</summary>
    public virtual bool TakeDamage(Character from)//�Ű������� �ʿ�� �߰��ϼ���
    {
        return true;
    }
}
