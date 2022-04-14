using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MovableObject
{
    Gauge hitPoint;
    Gauge manaPoint;
    Gauge staminaPoint;
    Gauge downGauge;

    SkillType currentSkill;  //���� �غ�� ��ų
    float SkillReadyLeft = 0.0f; //��ų �������� ���� �ð�

    Hitable attackTarget;

    bool offensive = false;
    bool controllable = true;

    public virtual void Attack(Hitable target)
    {
        if(target.TakeDamage(this))
        {
            Debug.Log("���� ����");
        }
        else
        {
            //���潺 ���� �����ڴ� ���� ����� ���������� ���ɸ�
            Debug.Log("���� ����");
        };
    }

    //������ �������� �ַ��� �θ��� �Լ�!
    public override bool TakeDamage(Pawn from)
    {
        bool result = true;//�⺻������ ������ ���������� ������ ��� �Ʒ��ʿ��� ���� üũ
        //���� ������ ���� �ִ� ��쿡 �� ������ ���� ���� ��� �Ǵ� �������� �ʾƵ� �����ϴ� ���
        if( from.currentSkill!=null && from == attackTarget || currentSkill.IsPatrol())
        {
            //����� �����̶� �� �����̶� üũ!
            result = from.currentSkill.ValidCheck(currentSkill);
        };

        //�갡 ������ ��� üũ �ϰ� �ϰ�
        //�ٿ �갡 �ϰ� ��

        return result;
    }
}
