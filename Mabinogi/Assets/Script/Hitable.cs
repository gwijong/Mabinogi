using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitable : Interactable
{
    //������ ������ ��쿡�� ���� ����ڰ� ���ϰ��� �޾Ƽ� ������ ������ �ɸ��� �ؾ���
    
    public virtual bool TakeDamage(Pawn from)//�Ű������� �ʿ�� �߰��ϼ���
    {
        return true;
    }
}
