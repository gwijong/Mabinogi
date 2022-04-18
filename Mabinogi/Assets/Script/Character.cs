using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Movable
{
    /// <summary> ����� ������ summary>
    Gauge hitPoint;
    /// <summary> ���� ������ summary>
    Gauge manaPoint;
    /// <summary> ���¹̳� ������ summary>
    Gauge staminaPoint;
    /// <summary> �ٿ� ������ summary>
    Gauge downGauge;
    public CharacterData data;

    /// <summary> �غ� �Ϸ�� ���� ��ų</summary>
    Skill skill;
    /// <summary> ��ų �������� ���� �ð�</summary>
    float skillCastingTimeLeft = 0.0f;
    /// <summary> ������ ���� Ÿ��</summary>
    public Hitable attackTarget;

    bool controllable = true;

    protected override void Start()
    {
        base.Start();       
        agent.angularSpeed = 1000;  //������̼� ȸ����
        agent.acceleration = 100; //������̼� ���ӵ�
        agent.speed = data.Speed; //������̼� �̵��ӵ�

        hitPoint.Max = data.HitPoint; //�ִ� �����
        hitPoint.Current = data.HitPoint;  //���� �����
        hitPoint.FillableRate = 1.0f;  //�λ��

        manaPoint.Max = data.ManaPoint; //�ִ� ����
        manaPoint.Current = data.ManaPoint;  //���� ����

        staminaPoint.Max = data.StaminaPoint; //�ִ� ���¹̳�
        staminaPoint.Current = data.StaminaPoint;  //���� ���¹̳�
        staminaPoint.FillableRate = 1.0f;  //���

        downGauge.Max = 100; //�ٿ� ������
        downGauge.Current = 0;  //���� ������ �ٿ������
    }

    /// <summary> ���� �Լ�</summary>
    public virtual void Attack(Hitable enemyTarget)
    {
        if (enemyTarget.TakeDamage(this) == true)//���ݿ� ������ ���
        {
            Debug.Log("���� ����");
        }
        else//���ݿ� ������ ���
        {
            //������ ������ ��쿡�� ���� ����ڰ� ���ϰ��� �޾Ƽ� ������ ������ �ɸ��� �ؾ���
            //���潺 ���� �����ڴ� ���� ����� ���������� ���ɸ�
            //ī���ʹ� �ݰ� ���ϰ� �ٿ��
            Debug.Log("���� ����");
        };
    }

    /// <summary> ������ �� ĳ���Ϳ� �������� �ַ��� ������ �θ��� �Լ�</summary>
    public override bool TakeDamage(Character enmeyAttacker)
    {
        bool result = true;//�⺻������ ������ ���������� ������ ��� �Ʒ��ʿ��� ���� üũ

        //���� ���ֺ��� �ο�� ��� �Ǵ� ���潺.ī���� ����, ������ ������ ������ ��ų ��� �������� üũ�ؾ� �ϴ� ���
        if(enmeyAttacker.skill!=null && this.attackTarget == enmeyAttacker || this.skill.mustCheck()==true)
        {
            
            result = enmeyAttacker.skill.WinnerCheck(this.skill); //���� ��ų�� �� ��ų�� �켱���� ��
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
