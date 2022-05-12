using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �´°� ������ ������Ʈ��</summary>
public class Hitable : Interactable 
{
    /*�� ��ũ��Ʈ�� ��ӹ��� ��ũ��Ʈ�� ������Ʈ�� ������ �ְų�
      �� ��ũ��Ʈ�� ������Ʈ�� ������ ������
      ���� �� �����Ƿ� InteractType.Attack ��ȯ
    */
    public override Define.InteractType Interact(Interactable other)  
    {
        return Define.InteractType.Attack;
    }
    /// <summary> �����ϴ� ������ ȣ���ϴ� ��� �´� �޼���</summary>
    public virtual bool TakeDamage(Character from)//�Ű������� �ʿ��ϸ� �߰��ϼ���
    {
        return true;
    }
}
