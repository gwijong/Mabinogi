using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �´°� ������ ������Ʈ��</summary>
public class Hitable : Interactable 
{

    
    public virtual bool TakeDamage(Character from)//�Ű������� �ʿ�� �߰��ϼ���
    {
        return true;
    }
}
