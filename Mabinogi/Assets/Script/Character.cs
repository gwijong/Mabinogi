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
    protected Skill skill = new Skill();
    /// <summary> ��ų �������� ���� �ð�</summary>
    protected float skillCastingTimeLeft = 0.0f;

    [SerializeField] Animator anim;

    /// <summary> ���� ���� ����</summary>
    protected bool controllable = true;
    /// <summary> �ϻ�, ������� ��</summary>
    protected bool offensive = false;


    /// <summary> �ִ빰�����ݷ� </summary>
    public int maxPhysicalStrikingPower;
    /// <summary> �ִ븶�����ݷ� </summary>
    public int maxMagicStrikingPower;
    /// <summary> �ּҹ������ݷ� </summary>
    public int minPhysicalStrikingPower;
    /// <summary> �ּҸ������ݷ� </summary>
    public int minMagicStrikingPower;
    /// <summary> ĳ���Ͱ� ���� �λ� </summary>
    public int wound;
    /// <summary> ���ݽ� ���濡�� ������ �λ�� </summary>
    public int woundAttack;
    /// <summary> ġ��Ÿ Ȯ�� </summary>
    public float critical;
    /// <summary> �뷱��, �ּ�, �ִ� �������� �ߴ� ���� </summary>
    public float balance;
    /// <summary> ���� ����. 1��1 ������ �������� ���ط� ���� </summary>
    public int physicalDefensivePower;
    /// <summary> ���� ����. 1��1 ������ �������� ���ط� ���� </summary>
    public int magicDefensivePower;
    /// <summary> ���� ��ȣ. 1�ۼ�Ʈ ������ ������ ���� </summary>
    public int physicalProtective;
    /// <summary> ���� ��ȣ. 1�ۼ�Ʈ ������ ������ ���� </summary>
    public int magicProtective;
    /// <summary> ����� ������ ������ �� �̰ܳ��� ���鸮 ���°� �� Ȯ�� </summary>
    public int deadly;
    /// <summary> �̵� �ӵ� </summary>
    public int speed;

    /// <summary> �ٿ� ���� �ð� </summary>
    public float downTime = 4.0f;
    /// <summary> ���� �Ұ� üũ </summary>
    public int waitCount = 0;
    /// <summary> ���� �Ұ� �ڷ�ƾ </summary>
    protected IEnumerator wait;
    /// <summary> �ǰ� �ִϸ��̼� A, B�� ���� </summary>
    protected int hitCount = 0;

    Rigidbody rigid;

    protected override void Start()
    {
        base.Start();
        rigid = GetComponent<Rigidbody>();
        agent.angularSpeed = 1000;  //������̼� ȸ����
        agent.acceleration = 100; //������̼� ���ӵ�
        agent.speed = data.Speed; //������̼� �̵��ӵ�

        hitPoint.Max = data.HitPoint; //�ִ� �����      
        hitPoint.FillableRate = 1.0f;  //�λ��
        hitPoint.Current = data.HitPoint;  //���� �����
        

        manaPoint.Max = data.ManaPoint; //�ִ� ����
        manaPoint.FillableRate = 1.0f;  //���� �ִ� ����
        manaPoint.Current = data.ManaPoint;  //���� ����

        staminaPoint.Max = data.StaminaPoint; //�ִ� ���¹̳�
        staminaPoint.FillableRate = 1.0f;  //���
        staminaPoint.Current = data.StaminaPoint;  //���� ���¹̳�
        

        downGauge.Max = 100; //�ٿ� ������
        downGauge.FillableRate = 1.0f;
        downGauge.Current = 0;  //���� ������ �ٿ������


        maxPhysicalStrikingPower = data.MaxPhysicalStrikingPower;
        maxMagicStrikingPower = data.MaxMagicStrikingPower;
        minPhysicalStrikingPower = data.MinPhysicalStrikingPower;
        minMagicStrikingPower = data.MinMagicStrikingPower;
        wound = data.Wound;
        woundAttack = data.WoundAttack;
        critical = data.Critical;
        balance = data.Balance;
        physicalDefensivePower = data.PhysicalDefensivePower;
        magicDefensivePower = data.MagicDefensivePower;
        physicalProtective = data.PhysicalProtective;
        magicProtective = data.MagicProtective;
        deadly = data.Deadly;
        speed = data.Speed;
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
            if (distance > 2)
            {
                MoveTo(focusTarget.transform.position);
            }
            else
            {
                if(focusTarget.gameObject.layer == (int)Define.Layer.Enemy)
                {
                    MoveStop();                                      
                }
            }        
        };
    }

    /// <summary> ���� �Լ�</summary>
    public virtual void Attack(Hitable enemyTarget)
    {
        wait = Wait(0.5f);
        StartCoroutine(wait);

        if (enemyTarget.TakeDamage(this) == true)//���ݿ� ������ ���
        {
            PlayAnim("Combat");
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

        if(result == true)
        {           
            hitPoint.Current = hitPoint.Current - enemyAttacker.maxPhysicalStrikingPower;
            downGauge.Current = downGauge.Current + 40;
            Debug.Log(downGauge.Current);
            if (hitPoint.Current <= 0)
            {
                DieCheck();
                DownCheck();
            }
            else if (downGauge.Current < 100)
            {
                PlayAnim("HitA");
            }
            else if (downGauge.Current >= 100)
            {
                DownCheck();
            }
        }
        return result;
    }

    /// <summary> ���콺 �Է����� Ÿ�� ���� �õ�, Ű���� �Է½� ���� </summary>
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

    /// <summary> �����̽��� �Է����� �ϻ�, ������� ��ȯ </summary>
    public void SetOffensive()
    {
        offensive = !offensive;
        PlayAnim("Offensive", offensive);
    }
    /// <summary> �Ű����� ���� �༭ �ϻ�, ������� ��ȯ </summary>
    public void SetOffensive(bool value)
    {
        offensive = value;
        PlayAnim("Offensive", offensive);
    }

    /// <summary> �ٿ� </summary>
    public void DownCheck()
    {
        rigid.AddForce(gameObject.transform.forward * -60000);
        rigid.AddForce(gameObject.transform.up * 20000);
        wait = Wait(downTime);
        StartCoroutine(wait);
        PlayAnim("BlowawayA");
        downGauge.Current = 0;
    }

    /// <summary> ��� üũ </summary>
    public void DieCheck()
    {

        PlayAnim("Die");
    }

    /// <summary> �ִϸ����� �Ķ����(trigger) ����</summary>
    protected void PlayAnim(string wantName)  
    {
        if (anim != null) anim.SetTrigger(wantName);
    }
    /// <summary> �ִϸ����� �Ķ����(bool) ����</summary>
    protected void PlayAnim(string wantName, bool value)
    {
        if (anim != null) anim.SetBool(wantName, value);
    }
    /// <summary> �ִϸ����� �Ķ����(float) ����</summary>
    protected void PlayAnim(string wantName, float value)
    {
        if (anim != null) anim.SetFloat(wantName, value);
    }
    /// <summary> �ִϸ����� �Ķ����(int) ����</summary>
    protected void PlayAnim(string wantName, int value)
    {
        if (anim != null) anim.SetInteger(wantName, value);
    }

    public IEnumerator Wait(float time)//���� �ð� �ڷ�ƾ
    {
        offensive = true;
        waitCount++;
        yield return new WaitForSeconds(time);
        waitCount--;
    }
}
