using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitable : Interactable //�´°� ������ ������Ʈ��
{
    //������ ������ ��쿡�� ���� ����ڰ� ���ϰ��� �޾Ƽ� ������ ������ �ɸ��� �ؾ���
    
    public virtual bool TakeDamage(Character from)//�Ű������� �ʿ�� �߰��ϼ���
    {
        return true;
    }
}
