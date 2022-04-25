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
    /// <summary> ĳ���� ������(�÷��̾�, ��, ���� ��) summary>
    public CharacterData data;

    [SerializeField]
    /// <summary> ������ Ÿ��</summary>
    protected Interactable focusTarget;
    /// <summary> ������ Ÿ���� Ÿ�� enum</summary>
    protected Define.InteractType focusType;

    [SerializeField]
    /// <summary> �غ� �Ϸ�� ���� ��ų</summary>
    protected Skill loadedSkill;
    /// <summary> �غ����� ���� ��ų</summary>
    protected Skill reservedSkill;
    /// <summary> ���� ��� ��ų ����Ʈ</summary>
    protected SkillList skillList;
    /// <summary> ��ų �������� ���� �ð�</summary>
    protected float skillCastingTimeLeft = 0.0f;
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

    /// <summary> �ٿ� ���� �ð� </summary>
    public float downTime = 4.0f;
    /// <summary> ���� ���� �ð� </summary>
    public float attackTime = 1.0f;
    /// <summary> �ǰݽ� ���� �Ұ� üũ </summary>
    public int waitCount = 0;
    /// <summary> �ǰݽ� ���� �Ұ� �ڷ�ƾ </summary>
    protected IEnumerator wait;
    /// <summary> �ǰ� �ִϸ��̼� A, B�� ���� </summary>
    protected int hitCount = 0;
    /// <summary> �׷α� ���� üũ </summary>
    protected bool groggy = false;

    Rigidbody rigid;
    Animator anim;

    //��ų������ ��ũ���ͺ� ������Ʈ��
    public SkillData combatData;  //�⺻���� �ĺ� ��ų ������
    public SkillData defenseData;  //���潺 ��ų ������
    public SkillData smashData;  //���Ž� ��ų ������
    public SkillData counterData; //ī���� ���� ��ų ������

    /// <summary> ������̼� ȸ���� </summary>
    public float angularSpeed = 1000f;
    /// <summary> ������̼� ���ӵ� </summary>
    public float acceleration = 100f;
    protected override void Start()
    {
        base.Start();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        agent.angularSpeed = angularSpeed;  //������̼� ȸ����
        agent.acceleration = acceleration; //������̼� ���ӵ�
        agent.speed = data.Speed; //������̼� �̵��ӵ�
        runSpeed = data.Speed; //�̵� �ӵ�
        walkSpeed = data.Speed / 2; //�ȴ� �ӵ�
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
        downGauge.FillableRate = 1.0f;//�ٿ������ �ִ� ����
        downGauge.Current = 0;  //���� ������ �ٿ������


        maxPhysicalStrikingPower = data.MaxPhysicalStrikingPower;  //�ִ� �������ݷ�
        maxMagicStrikingPower = data.MaxMagicStrikingPower;  //�ִ� �������ݷ�
        minPhysicalStrikingPower = data.MinPhysicalStrikingPower;  //�ּ� �������ݷ�
        minMagicStrikingPower = data.MinMagicStrikingPower;  //�ּ� �������ݷ�
        wound = data.Wound;  //�λ�
        woundAttack = data.WoundAttack;  //���� �� �λ��
        critical = data.Critical;  //ġ��Ÿ
        balance = data.Balance;  //�뷱��
        physicalDefensivePower = data.PhysicalDefensivePower;  //���� ����
        magicDefensivePower = data.MagicDefensivePower;  //���� ����
        physicalProtective = data.PhysicalProtective;  //���� ��ȣ
        magicProtective = data.MagicProtective;  //���� ��ȣ
        deadly = data.Deadly;  //���鸮 Ȯ��
    }
    
    protected virtual void Update()
    {
        if(skillCastingTimeLeft>=0 && reservedSkill != null) //��ų ���� �ð��� ���Ұ� �������� ��ų�� ���� ���
        {
            skillCastingTimeLeft -= Time.deltaTime;  //���� ��ų ���� �ð��� ���������� �ٿ���
            switch (reservedSkill.type)  //�غ����� ���� ��ų Ÿ��
            {
                case Define.SkillState.Combat:  
                    agent.speed = runSpeed;  //�ĺ��̸� �޸��� �ӵ�
                    MoveStop(false);
                    break;
                case Define.SkillState.Defense:
                    agent.speed = walkSpeed;  //���潺�� �ȴ� �ӵ�
                    MoveStop(false);
                    break;
                case Define.SkillState.Smash:
                    agent.speed = runSpeed;  //���Žø� �޸��� �ӵ�
                    MoveStop(false);
                    break;
                case Define.SkillState.Counter:
                    agent.speed = 0;  //ī���͸� �̵� ����
                    MoveStop(true);
                    break;

            }           
        }else if(skillCastingTimeLeft <= 0 && reservedSkill != null)  //��ų ���� �ð��� �� ������ ���
        {
            loadedSkill = reservedSkill;  //�غ�� ��ų ����
            reservedSkill = null;  // �غ����� ��ų null�� ��ȯ
        }
             
        PlayAnim("Move", agent.velocity.magnitude);  //�̵��� ���񿡼� float ������ �޾ƿͼ� �ִϸ��̼� ���

        if (waitCount == 0)  //���� �Ұ� �ڷ�ƾ ���� 0�� ���
        {
            MoveStop(false); //�̵� ����
        }
        else //���� �Ұ��� ���
        {
            focusTarget = null;  //Ÿ���� null�� �ٲ�(�´� �߿� ���� �� �� ���� ����)
            MoveStop(true); //�̵� �Ұ�
        }

        if (focusTarget != null) //���콺�� Ŭ���� Ÿ���� �ִ� ���
        {
            float distance = (focusTarget.transform.position - transform.position).magnitude;  //Ÿ�ٰ� ���� �Ÿ�

            if (distance > InteractableDistance(focusType)) //�Ÿ��� ��ȣ�ۿ� ���� �Ÿ����� �� ���
            {
                MoveTo(focusTarget.transform.position);  //�ٰ������� �̵�
            }
            else //�Ÿ��� ��ȣ�ۿ� ���� �Ÿ����� ����� ���
            {
                MoveStop(true); //�ٰ����� �̵� ����
/////////////////////////////////////////////////////////////////////////////////////////////////////������� �ּ� �۾��� ����
                switch (focusTarget.Interact(this))
                {
                    case Define.InteractType.Attack:
                        Attack((Hitable)focusTarget);
                        break;
                    case Define.InteractType.Talk:
                        //���⼱ ��ȭ�� Ǯ�����
                        break;
                };
                //�� �� �����ϱ� Ǯ���ֱ�!
                focusTarget = null;
            }        
        };
    }

    public virtual float InteractableDistance(Define.InteractType wantType)
    {
        switch(wantType)
        {
            case Define.InteractType.Attack:
                {
                    //���߿� ���� ��Ÿ��� ��ų ��Ÿ� ���� ���⿡�� ��������!
                    return 2;
                }
            case Define.InteractType.Get: return 1;
            case Define.InteractType.Talk: return 3;
            default: return 2;
        };
    }

    /// <summary> ���� �Լ�</summary>
    public virtual void Attack(Hitable enemyTarget)
    {
        SetOffensive(true);
        this.transform.LookAt(enemyTarget.transform);
        enemyTarget.transform.LookAt(this.transform);
        if (waitCount != 0)
        {
            return;
        }
        wait = Wait(attackTime);
        StartCoroutine(wait);

        if (enemyTarget.TakeDamage(this) == true)//���ݿ� ������ ���
        {        
            Debug.Log("���� ����");
            loadedSkill = skillList[Define.SkillState.Combat].skill;
        }
        else//���ݿ� ������ ���
        {
            //������ ������ ��쿡�� ���� ����ڰ� ���ϰ��� �޾Ƽ� ������ ������ �ɸ��� �ؾ���
            //���潺 ���� �����ڴ� ���� ����� ���������� ���ɸ�
            //ī���ʹ� �ݰ� ���ϰ� �ٿ��
            if(enemyTarget.GetComponent<Character>().loadedSkill == skillList[Define.SkillState.Defense].skill)
            {
                enemyTarget.GetComponent<Character>().PlayAnim("Defense");
            }
            else if (enemyTarget.GetComponent<Character>().loadedSkill == skillList[Define.SkillState.Counter].skill)
            {
                enemyTarget.GetComponent<Character>().PlayAnim("Counter");
            };
            Groggy();
            enemyTarget.GetComponent<Character>().loadedSkill = skillList[Define.SkillState.Combat].skill;
            Debug.Log("���� ����");
            loadedSkill = skillList[Define.SkillState.Combat].skill;
            wait = Wait(3);
            StartCoroutine(wait);
        };
    }

    public override Define.InteractType Interact(Interactable other)
    {       
        if(IsEnemy(this, other))
        {
            return Define.InteractType.Attack;
        }
        else
        {
            return Define.InteractType.Talk;
        };
    }

    public override void MoveTo(Vector3 goalPosition)
    {
        base.MoveTo(goalPosition);
    }

    /// <summary> ������ �� ĳ���Ϳ� �������� �ַ��� ������ �θ��� �Լ�</summary>
    public override bool TakeDamage(Character Attacker)
    {
        reservedSkill = null;
        skillCastingTimeLeft = 0;

        SetOffensive(true);
        bool result = true;//�⺻������ ������ ���������� ������ ��� �Ʒ��ʿ��� ���� üũ

        //���� ���ֺ��� �ο�� ��� �Ǵ� ���潺.ī���� ����, ������ ������ ������ ��ų ��� �������� üũ�ؾ� �ϴ� ���
        if(Attacker.loadedSkill != null && this.focusTarget == Attacker || (this.loadedSkill != null && this.loadedSkill.mustCheck) )
        {
            result = Attacker.loadedSkill.WinnerCheck(this.loadedSkill); //���� ��ų�� �� ��ų�� �켱���� ��
        };

        if(result == true)
        {      
            switch (Attacker.loadedSkill.type)
            {
                case Define.SkillState.Combat:
                    Attacker.PlayAnim("Combat");
                    this.downGauge.Current += combatData.DownGauge;
                    this.hitPoint.Current -= Attacker.maxPhysicalStrikingPower * combatData.Coefficient * skillList[Define.SkillState.Combat].rank;
                    Debug.Log("���� �Ϲ� ���ݷ�: " + Attacker.maxPhysicalStrikingPower * combatData.Coefficient * skillList[Define.SkillState.Combat].rank);
                    break;
                case Define.SkillState.Defense:
                    Attacker.PlayAnim("Defense");
                    this.downGauge.Current += defenseData.DownGauge;
                    this.hitPoint.Current -= Attacker.maxPhysicalStrikingPower * defenseData.Coefficient * skillList[Define.SkillState.Defense].rank;
                    Debug.Log("��� �Ϲ� ���ݷ�: " + Attacker.maxPhysicalStrikingPower * defenseData.Coefficient * skillList[Define.SkillState.Defense].rank);
                    break;
                case Define.SkillState.Smash:
                    Attacker.PlayAnim("Smash");
                    groggy = true;
                    this.downGauge.Current += smashData.DownGauge;
                    this.hitPoint.Current -= Attacker.maxPhysicalStrikingPower * smashData.Coefficient * skillList[Define.SkillState.Smash].rank;
                    Debug.Log("���Ž� ���ݷ�: " + Attacker.maxPhysicalStrikingPower * smashData.Coefficient * skillList[Define.SkillState.Smash].rank);
                    break;
                case Define.SkillState.Counter:
                    Attacker.PlayAnim("Counter");
                    this.downGauge.Current += counterData.DownGauge;
                    this.hitPoint.Current -= Attacker.maxPhysicalStrikingPower * counterData.Coefficient * skillList[Define.SkillState.Counter].rank;
                    Debug.Log("ī���� ���ݷ�: " + Attacker.maxPhysicalStrikingPower * counterData.Coefficient * skillList[Define.SkillState.Counter].rank);
                    break;
            }
            
            if (this.hitPoint.Current <= 0)
            {
                DieCheck();
                this.downGauge.Current = 100;
            }
            else if (this.downGauge.Current < 100)
            {

                PlayAnim("HitA");
               
            }
            else if (this.downGauge.Current >= 100)
            {
                if (groggy)
                {
                    Groggy();
                    groggy = false;
                }
                else
                {
                    DownCheck();
                }
            }
        }
        return result;
    }

    public void Groggy()
    {
        wait = Wait(downTime+2);
        StartCoroutine(wait);
        IEnumerator groggy = GroggyDown();
        StartCoroutine(groggy);
        PlayAnim("Groggy");
        downGauge.Current = 0;
    }
    IEnumerator GroggyDown()
    {
        Debug.Log("�׷α�2");
        yield return new WaitForSeconds(1.0f);
        rigid.AddForce(gameObject.transform.forward * -600);
        rigid.AddForce(gameObject.transform.up * 500);
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
        rigid.AddForce(gameObject.transform.forward * -600);
        rigid.AddForce(gameObject.transform.up * 200);
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

    public void Casting(Define.SkillState value)
    {
        SkillInfo currentSkill = skillList[value];
        if (currentSkill == null) return;
        reservedSkill = currentSkill.skill;
        skillCastingTimeLeft = currentSkill.skill.castingTime;//������Ʈ���� ��ŸŸ������ ����//ĵ�� �� skill�� null
    }
}
