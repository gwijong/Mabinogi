using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Movable
{
    /// <summary> ����� ������ summary>
    protected Gauge hitPoint = new Gauge();
    /// <summary> ���� ������ summary>
    protected Gauge manaPoint = new Gauge();
    /// <summary> ���¹̳� ������ summary>
    protected Gauge staminaPoint = new Gauge();
    /// <summary> �ٿ� ������ summary>
    protected Gauge downGauge = new Gauge();
    public CharacterData data;

    /// <summary> ������ Ÿ��</summary>
    protected Hitable focusTarget;

    /// <summary> �غ� �Ϸ�� ���� ��ų</summary>
    protected Skill skill;
    /// <summary> ��ų �������� ���� �ð�</summary>
    protected float skillCastingTimeLeft = 0.0f;

    [SerializeField] Animator anim;

    protected bool controllable = true;
    protected bool offensive = false;

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
    
    protected virtual void Update()
    {
        PlayAnim("Move", agent.velocity.magnitude);

        //�ϴ��� ������ ���µ� ��Ȳ�� ���� (��ų�� ���� ��ų ��Ÿ� ������)
        //                                (���� ��Ÿ��� ���� �ְ�)
        //                                (��ȭ �Ÿ��� ���� �ְ�)
        if(focusTarget != null)
        {
            float distance = (focusTarget.transform.position - transform.position).magnitude;

            MoveTo(focusTarget.transform.position);
        };
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
    public override bool TakeDamage(Character enemyAttacker)
    {
        bool result = true;//�⺻������ ������ ���������� ������ ��� �Ʒ��ʿ��� ���� üũ

        //���� ���ֺ��� �ο�� ��� �Ǵ� ���潺.ī���� ����, ������ ������ ������ ��ų ��� �������� üũ�ؾ� �ϴ� ���
        if(enemyAttacker.skill != null && this.focusTarget == enemyAttacker || (this.skill != null && this.skill.mustCheck()) )
        {
            result = enemyAttacker.skill.WinnerCheck(this.skill); //���� ��ų�� �� ��ų�� �켱���� ��
        };
        DownCheck();
        DieCheck();
        return result;
    }

    public bool SetTarget(Character target)
    {
        if (target == null)
        {
            focusTarget = null;
            return false;
        };

        if (target.gameObject.layer == (int)Define.Layer.Enemy)
        {
            focusTarget = target;
            return true;
        };

        return false;

    }

    public void SetOffensive()
    {
        offensive = !offensive;
        PlayAnim("Offensive", offensive);
    }
    public void SetOffensive(bool value)
    {
        offensive = value;
        PlayAnim("Offensive", offensive);
    }

    public void DownCheck()
    {
        //PlayAnim("BlowAwayA");
    }
    public void DieCheck()
    {
        //PlayAnim("Die");
    }

    protected void PlayAnim(string wantName)
    {
        if (anim != null) anim.SetTrigger(wantName);
    }
    protected void PlayAnim(string wantName, bool value)
    {
        if (anim != null) anim.SetBool(wantName, value);
    }
    protected void PlayAnim(string wantName, float value)
    {
        if (anim != null) anim.SetFloat(wantName, value);
    }
    protected void PlayAnim(string wantName, int value)
    {
        if (anim != null) anim.SetInteger(wantName, value);
    }
}
