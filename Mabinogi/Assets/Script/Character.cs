using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Movable
{
    Gauge hitPoint;
    Gauge manaPoint;
    Gauge staminaPoint;
    Gauge downGauge;

    /// <summary> �غ� �Ϸ�� ���� ��ų</summary>
    Skill skill;
    /// <summary> ��ų �������� ���� �ð�</summary>
    float skillCastingTimeLeft = 0.0f;
    /// <summary> ������ ���� Ÿ��</summary>
    Hitable attackTarget;

    bool offensive = false;
    bool controllable = true;

    Character myAttack;
    private void Start()
    {
        myAttack = this;
    }

    public virtual void Attack(Hitable enemyTarget)
    {
        if (enemyTarget.TakeDamage(myAttack) == true)//���ݿ� ������ ���
        {
            Debug.Log("���� ����");
        }
        else//���ݿ� ������ ���
        {
            //���潺 ���� �����ڴ� ���� ����� ���������� ���ɸ�
            //ī���ʹ� �ݰ� ���ϰ� �ٿ��
            Debug.Log("���� ����");
        };
    }

    /// <summary> ������ �� ĳ���Ϳ� �������� �ַ��� ������ �θ��� �Լ�</summary>//
    public override bool TakeDamage(Character enemyAttack)
    {
        bool result = true;//�⺻������ ������ ���������� ������ ��� �Ʒ��ʿ��� ���� üũ

        //���� ���ֺ��� �ο�� ��� �Ǵ� ���潺.ī���� ����, ������ ������ ������ ��ų ��� �������� üũ�ؾ� �ϴ� ���
        if(enemyAttack.skill!=null && this.attackTarget == enemyAttack || this.skill.mustCheck()==true)
        {
            
            result = enemyAttack.skill.WinnerCheck(this.skill); //���� ��ų�� �� ��ų�� �켱���� ��
        };
        DownCheck();
        DieCheck();
        return result;
    }
    public void DownCheck()
    {

    }
    public void DieCheck()
    {

    }
}
