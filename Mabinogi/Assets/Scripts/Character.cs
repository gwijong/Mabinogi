using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(EnemyDummyAI))] //���� �ΰ������� �⺻ �����Ǿ� �־�� ��
[RequireComponent(typeof(Rigidbody))] //������ٵ� �⺻ �����Ǿ� �־�� ��
[RequireComponent(typeof(BoxCollider))] //�ݶ��̴��� �⺻ �����Ǿ� �־�� ��
public class Character : Movable
{

    #region �ɹ� ����
    public string characterName;
    public float nameYpos;
    /// <summary> ����� ������ summary>
    public Gauge hitPoint = new Gauge();
    /// <summary> ���� ������ summary>
    public Gauge manaPoint = new Gauge();
    /// <summary> ���¹̳� ������ summary>
    public Gauge staminaPoint = new Gauge();
    /// <summary> �ٿ� ������ summary>
    protected Gauge downGauge = new Gauge();
    /// <summary> ĳ���� ������(�÷��̾�, ��, ���� ��) summary>
    public CharacterData data;

    [SerializeField]
    /// <summary> ������ Ÿ��</summary>
    protected Interactable focusTarget;
    /// <summary> ������ Ÿ���� Ÿ�� enum</summary>
    protected Define.InteractType focusType;

    /// <summary> �غ� �Ϸ�� ���� ��ų</summary>
    [SerializeField]
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
    protected bool offensive = true;
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
    /// <summary> �ǰ� ���� �ð� </summary>
    public float hitTime = 3.0f;
    /// <summary> �׷α� ���� �ð� </summary>
    public float groggyTime = 6.0f;
    /// <summary> ���� ���� ���� �ð� </summary>
    public float attackFailTime = 3.0f;
    /// <summary> �ǰݽ� ���� �Ұ� üũ </summary>
    public int waitCount = 0;
    /// <summary> �ǰݽ� ���� �Ұ� �ڷ�ƾ </summary>
    protected IEnumerator wait;
    /// <summary> �ǰ� �ִϸ��̼� A, B�� ���� </summary>
    protected int hitCount = 0;
    /// <summary> �׷α� ���� üũ </summary>
    protected bool groggy = false;
    /// <summary> ��� üũ </summary>
    public bool die = false;
    protected Rigidbody rigid;
    protected Animator anim;

    //��ų������ ��ũ���ͺ� ������Ʈ��
    public SkillData combatData;  //�⺻���� �ĺ� ��ų ������
    public SkillData defenseData;  //���潺 ��ų ������
    public SkillData smashData;  //���Ž� ��ų ������
    public SkillData counterData; //ī���� ���� ��ų ������

    /// <summary> ������̼� ȸ���� </summary>
    public float angularSpeed = 1000f;
    /// <summary> ������̼� ���ӵ� </summary>
    public float acceleration = 100f;
    #endregion
    protected override void Awake()
    {
        #region ������ �⺻�� �Ҵ�
        base.Awake();
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


        //��ų ĳ���� �ð��� ��ũ���ͺ� �������� ĳ���� �ð����� ����
        Skill.combatMastery.castingTime = combatData.CastTime; 
        Skill.defense.castingTime = defenseData.CastTime;
        Skill.smash.castingTime = smashData.CastTime;
        Skill.counterAttack.castingTime = counterData.CastTime;

        SetOffensive();//�ϻ���� ��ȯ�ϰ� �̵��ӵ��� �ȱ�� ������

        GameObject namePrefab = Instantiate(Resources.Load<GameObject>("Prefabs/NameUICanvas"));
        namePrefab.transform.localScale = Vector3.one * 0.01f;
        namePrefab.transform.SetParent(transform);
        namePrefab.transform.localPosition = new Vector3(0, nameYpos, 0);
        namePrefab.GetComponentInChildren<UnityEngine.UI.Text>().text = characterName;


        #endregion
    }

    protected virtual void OnUpdate()
    {
        GaugeCheck(); // ���� ������ ����

        PlayAnim("Move", agent.velocity.magnitude);  //�̵��� ���񿡼� float ������ �޾ƿͼ� �ִϸ��̼� ���

        TargetLookAt(focusTarget); //Ÿ�� �ٶ󺸱�

        SkillReady(); //skillCastingTimeLeft �����ð� üũ

        MovableCheck(); //waitCount�� �̿��� �̵� ���� üũ

        TargetCheck(); //������ Ÿ�� üũ
        
    }

    /// <summary> �������� ������Ʈ </summary>
    void GaugeCheck()
    {
        downGauge.Current -= 5 * Time.deltaTime; //�ٿ�������� �ʴ� 5�� ����
        if (loadedSkill.type == Define.SkillState.Combat && reservedSkill == null) //��ų�� �ĺ��̰� �������� ��ų�� ���� ���
        {
            staminaPoint.Current += 0.4f * Time.deltaTime; //���¹̳� �ʴ� 0.4 ����
            manaPoint.Current += 0.1f * Time.deltaTime; // ���� �ʴ� 0.1 ����
        }
        if (loadedSkill.type == Define.SkillState.Counter) //������ ��ų�� ī�����̸�
        {
            staminaPoint.Current -= 1 * Time.deltaTime; //�ʴ� 1�� ���¹̳� ����
        }
    }

    /// <summary> ������ Ÿ���� �繰����, ĳ�������� üũ�ϴ� ���� </summary>
    void TargetCheck()
    {
        if (focusTarget != null) //���콺�� Ŭ���� Ÿ���� �ִ� ���
        {
            Vector3 positionDiff = (focusTarget.transform.position - transform.position);//��� ��ǥ���� �� ��ǥ ��
            positionDiff.y = 0;//���� y�� ������
            float distance = positionDiff.magnitude;  //Ÿ�ٰ� ���� �Ÿ�
            if (distance > InteractableDistance(focusTarget.Interact(this))) //�Ÿ��� ��ȣ�ۿ� ���� �Ÿ����� �� ���
            {
                MoveTo(focusTarget.transform.position);  //�ٰ������� �̵�
                if(focusTarget.Interact(this)== Define.InteractType.Attack)//����� ���̸� �������� �̵�
                {
                    SetOffensive(true);
                }
            }
            else //�Ÿ��� ��ȣ�ۿ� ���� �Ÿ����� ����� ���
            {
                MoveStop(true); //�ٰ����� �̵� ����


                Character focusAsCharacter = null; //���� ���� �繰�� �� ��, ���� , �� ���� ĳ���͸� ��

                //Type.IsSubclassOf : Type���� �Ļ��Ǵ��� ���θ� Ȯ��
                if (focusTarget.GetType().IsSubclassOf(typeof(Character))) //�Ļ� Ŭ���� ĳ���Ͱ� �ִ��� üũ, ������ true
                {
                    focusAsCharacter = (Character)focusTarget;//������ ���
                };
                switch (focusTarget.Interact(this))//���� �ڽ��� ��ȣ�ۿ� Ÿ��
                {
                           case Define.InteractType.Attack: //��ȣ�ۿ� Ÿ���� �����̸�
                        if (skillCastingTimeLeft > 0 //��ų ���� �ð��� ���Ұų�
                            || loadedSkill.cannotAttack //���� �Ұ� ��ų�� �����߰ų�
                            || (focusAsCharacter != null && focusAsCharacter.die == true)) //������ ����� �����鼭 ������ ����� ���������
                        {
                            break;//���̽��� Ż��
                        };
                        Attack((Hitable)focusTarget); //�����Ѵ�
                        break;
                    case Define.InteractType.Talk:  //��ȣ�ۿ� Ÿ���� ��ȣ���̸�
                        //���⼱ ��ȭ�� Ǯ�����
                        break;
                    case Define.InteractType.Sheeping:  //��ȣ�ۿ� Ÿ���� ���� ä���̸�
                        this.GetComponent<Player>().Sheeping();
                        break;
                    case Define.InteractType.Get:  //��ȣ�ۿ� Ÿ���� ������ �ݱ��̸�
                        ItemInpo itemInpo = focusTarget.GetComponent<ItemInpo>();
                        if (itemInpo != null)
                        {
                            itemInpo.GetItem();
                        }
                        break;
                    default: Debug.Log(focusTarget.Interact(this));//�̻��� �� ������ �����

                        break;
                };
                focusTarget = null; //��ȣ�ۿ��� �������Ƿ� Ÿ���� ���
            }
        };
    }
    /// <summary> ������ �� �ִ��� üũ </summary>
    bool MovableCheck()
    {
        bool CanMove = true; //������ �� �ִ°���?
        if (waitCount == 0) // waitCount ����ġ�� ��Ȯ�� 0�� ���
        {
            CanMove = true; //�̵� ����
        }
        else
        {
            CanMove = false; //waitCount�� ���Ƽ� ������ �� ����
        }

        if (CanMove == false)  //���� �Ұ��� ���
        {
            focusTarget = null;  //Ÿ���� null�� �ٲ�(�´� �߿� ���� �� �� ���� ����)
        };

        MoveStop(!CanMove);//������̼� �̵� �޼��忡 �̵��������� bool�� �־���
        return CanMove;
    }

    /// <summary> ��ų �غ�, �Ϸ� </summary>
    void SkillReady()
    {       
        if (skillCastingTimeLeft >= 0 && reservedSkill != null) //��ų ���� �ð��� ���Ұ� �������� ��ų�� ���� ���
        {
            skillCastingTimeLeft -= Time.deltaTime;  //���� ��ų ���� �ð��� ���������� �ٿ���
            setMoveSpeed(reservedSkill.type);

        }
        else if (skillCastingTimeLeft <= 0 && reservedSkill != null)  //��ų ���� �ð��� �� ������ ���
        {
            loadedSkill = reservedSkill;  //�غ�� ��ų ����
            reservedSkill = null;  // �غ����� ��ų null�� ��ȯ
        }
    }

    /// <summary> ������ ��ų�� ���� �̵��ӵ� ���� </summary>
    void setMoveSpeed(Define.SkillState type)
    {
        float result = runSpeed;
        switch (type)  //�غ����� ���� ��ų Ÿ��
        {
            case Define.SkillState.Defense:
                result = walkSpeed;//���潺�� �ȴ� �ӵ�
                break;
            case Define.SkillState.Counter:
                result = 0;  //ī���͸� �̵� ����
                break;
        };
        if (walk && result > walkSpeed) result = walkSpeed;
        agent.speed = result;
    }

    /// <summary> ������ Ÿ�� �ٶ� </summary>
    public void TargetLookAt(Interactable target)
    {
        if (target != null && agent.velocity.magnitude <= 1.0f)
        {
            Vector3 look = target.transform.position;
            look.y = transform.position.y;
            transform.LookAt(look);
        };
    }

    /// <summary> ������ ��ǥ �ٶ� </summary>
    void TargetLookAt(Vector3 target)
    {
        if (target != null && agent.velocity.magnitude <= 1.0f)
        {
            target.y = transform.position.y;
            transform.LookAt(target);
        };
    }

    /// <summary> ��ȣ�ۿ� ���� �Ÿ� </summary>
    public virtual float InteractableDistance(Define.InteractType wantType)
    {
        switch(wantType)
        {
            case Define.InteractType.Attack:
                {                   
                    return 4; //���� ��Ÿ��� �þ ��� ���⼭ �����Ѵ�.
                }
            case Define.InteractType.Get: return 1; //������ �ݱ� ���� �Ÿ�
            case Define.InteractType.Sheeping: return 2; //���� ä�� ���� �Ÿ�
            case Define.InteractType.Talk: return 6;  //��ȭ ���� �Ÿ�
            default: return 4;  //�⺻���� 2�̴�.
        };
    }

    /// <summary> ���� �Լ�</summary>
    public virtual void Attack(Hitable enemyTarget)//Ÿ���� �����Ѵ�
    {
        SetOffensive(true);//�������� ��ȯ
        this.transform.LookAt(enemyTarget.transform); //Ÿ���� �ٶ󺻴�.

        //���� �Ұ� �����̰ų� �ĺ��ε� ���¹̳��� 2 �̸��̸� ������ �� �ϹǷ�
        if (waitCount != 0 ||(loadedSkill.type==Define.SkillState.Combat && staminaPoint.Current<combatData.CastCost)) 
        {
            return;  //�ٷ� �� �޼��带 ������
        }
        wait = Wait(attackTime); //���� ������ �ڷ�ƾ
        StartCoroutine(wait);

        //TakeDamage�Լ��� ���� ��ų�� Ǯ���� ������ ��ų�� �ӽ� ����
        Character asCharacter = null;
        if(enemyTarget.GetType().IsSubclassOf(typeof(Character)))
        {
            asCharacter = (Character)enemyTarget;  //���� ĳ���͸� �����
        };
        
        Define.SkillState otherSkill = Define.SkillState.Combat;  //���� ��ų�� �⺻�� �⺻����
        if (asCharacter != null) otherSkill = asCharacter.GetSkillType();//���� ĳ���Ͱ� �ִ� ��� ���� ��ų ������
        

        if (enemyTarget.TakeDamage(this) == true)//���ݿ� ������ ���
        {        
            PlayAnim(loadedSkill.AnimName);
            float waitTime = 0.0f;
            switch(loadedSkill.type)
            {
                case Define.SkillState.Combat:
                    waitTime = 1.0f;
                    staminaPoint.Current -= combatData.CastCost; //���� ������ ���¹̳��� 2 ����
                    break;
                case Define.SkillState.Smash: waitTime = 4.0f;
                    break;
            };

            wait = Wait(waitTime); //���� ���з� 3�ʰ� ����
            StartCoroutine(wait);

            loadedSkill = skillList[Define.SkillState.Combat].skill;//���� �� �⺻ �������� �غ�� ��ų �ʱ�ȭ
        }
        else//���ݿ� ������ ���
        {
            if(asCharacter != null)
            {
                switch (otherSkill)
                {
                    //������ ������ ��쿡�� ���� ����ڰ� ���ϰ��� �޾Ƽ� ������ ������ �ɸ��� �ؾ���
                    //���潺 ���� �����ڴ� ���� ����� ���������� ���ɸ�
                    case Define.SkillState.Defense:
                        PlayAnim("Combat");
                        wait = Wait(attackFailTime); //���� ���з� 3�ʰ� ����
                        StartCoroutine(wait);
                        break;

                    //ī���ʹ� �ݰ� ���ϰ� �ٿ��
                    case Define.SkillState.Counter:
                        this.downGauge.Current += counterData.DownGauge;
                        float damage = asCharacter.CalculateDamage(Define.SkillState.Counter);
                        this.hitPoint.Current -= damage;
                        Groggy();//�ݰ� �������Ƿ� �׷α� ���¿� ����
                        break;
                }
            };
            loadedSkill = skillList[Define.SkillState.Combat].skill;// �� ��ų �ʱ�ȭ

        };
    }

    public override Define.InteractType Interact(Interactable other) //��ȣ�ۿ� ��� Ÿ��
    {
        if(IsEnemy(this, other)) //����� ���� ������ üũ
        {
            return Define.InteractType.Attack; //��ȣ�ۿ� Ÿ���� �������� ����
        }
        else if(IsSheep(other, this))//������ ���̰� ���� �÷��̾����� üũ
        {
            return Define.InteractType.Sheeping; //��ȣ�ۿ� Ÿ���� ����ä������ ����
        }
        else if (IsItem(other))
        {
            return Define.InteractType.Get; //��ȣ�ۿ� Ÿ���� ������ �ݱ�� ����
        }
        else
        {
            return Define.InteractType.Talk; //��ȣ�ۿ� Ÿ���� ��ȭ�� ����
        }
        

    }

    public override void MoveTo(Vector3 goalPosition, bool isWalk = false)  //������̼� �̵� �޼���
    {
        base.MoveTo(goalPosition, isWalk);

        
    }

    /// <summary> ������ �� ĳ���Ϳ� �������� �ַ��� ������ �θ��� �Լ�</summary>
    public override bool TakeDamage(Character Attacker)
    {
        transform.LookAt(Attacker.transform); //���� ��븦 �ٶ󺻴�
        reservedSkill = null; //�غ����� ��ų ���
        skillCastingTimeLeft = 0; //�غ����� ��ų�� ��ҵǾ����Ƿ� ��� �ð��� 0���� �ʱ�ȭ

        SetOffensive(true); //�������� ��ȯ
        bool result = true;//�⺻������ ������ ���������� ������ ��� �Ʒ��ʿ��� ���� üũ
        agent.speed = runSpeed; //�̵��ӵ� �ʱ�ȭ
        walk = false;
        //���� ���ֺ��� �ο�� ��� �Ǵ� ���潺.ī���� ����, ������ ������ ������ ��ų ��� �������� üũ�ؾ� �ϴ� ���
        if (Attacker.loadedSkill != null && this.focusTarget == Attacker || (this.loadedSkill != null && this.loadedSkill.mustCheck) )
        {
            result = Attacker.loadedSkill.WinnerCheck(this.loadedSkill); //���� ��ų�� �� ��ų�� �켱���� ��
        };

        
        if(result == true) //���ݿ� ������ ���
        {
            float damage = 0.0f;
            switch (Attacker.loadedSkill.type)//���� ��ų�� ���� ���� ���ظ� ����
            {
                case Define.SkillState.Combat: 
                    this.downGauge.Current += combatData.DownGauge;
                    damage = Attacker.CalculateDamage(Define.SkillState.Combat);
                    break;
                case Define.SkillState.Smash:
                    damage = Attacker.CalculateDamage(Define.SkillState.Smash);
                    groggy = true;
                    this.downGauge.Current += smashData.DownGauge;
                    break;
            }
            this.hitPoint.Current -= damage;
            
            if (this.hitPoint.Current <= 0.2)//������� 0.2������ ��� ���
            {
                Dead();
            }
            else if (this.downGauge.Current < 100) //�ٿ�������� 100 ������ ���
            {
                //�׷α� ���°ų� ���ư��� ���°� �ƴϸ�
                if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name!="Groggy"|| anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Blowaway_Ground")
                {
                    PlayAnim("HitA");  //�ǰ� �ִϸ��̼� ���
                }
                wait = Wait(hitTime);
                StartCoroutine(wait);

            }
            else if (this.downGauge.Current >= 100)//�ٿ�������� 100 �̻��� ���
            {
                if (groggy) //�׷α� ���¿� ���� �ϸ�
                {
                    Groggy();//�׷α� ���·� ��
                    groggy = false;
                }
                else
                {
                    DownCheck();//�׷αⰡ �ƴ� ��� �Ϲ� �ٿ� ���·� ��
                }
            }
            loadedSkill = skillList[Define.SkillState.Combat].skill; //�ǰ� �� �غ�� ��ų �ʱ�ȭ
        }
        else //���ݿ� ������ ���
        {
            switch (loadedSkill.type)
            {
                case Define.SkillState.Defense:
                    PlayAnim("Defense"); //�� ���潺 �ִϸ��̼� ���
                    StartCoroutine(Wait(1f));//�� ���潺 ���� �� �̵� ����
                    //������ ���Ҵµ� ���潺�� ����
                    break;
                case Define.SkillState.Counter:
                    PlayAnim("Counter");//�� ī���� �ִϸ��̼� ���
                    StartCoroutine(Wait(2f));//�� ī���� ���� �� �̵� ����
                    //������ ���Ҵµ� ī���ͷ� ����
                    break;
                default:
                    break;
            };
            loadedSkill = skillList[Define.SkillState.Combat].skill; //��ų �ʱ�ȭ
        }

        return result;
    }



    /// <summary> ����� ����ϴ� ���� </summary>
    public float CalculateDamage(Define.SkillState type)
    {
        switch(type)
        {
            case Define.SkillState.Smash:
                return maxPhysicalStrikingPower * smashData.Coefficient * skillList[Define.SkillState.Smash].rank;
            case Define.SkillState.Combat:
                return maxPhysicalStrikingPower* combatData.Coefficient* skillList[Define.SkillState.Combat].rank;
            case Define.SkillState.Counter:
                return maxPhysicalStrikingPower* counterData.Coefficient* skillList[Define.SkillState.Counter].rank;
        };
        return 0;
    }

    /// <summary> �غ� �Ϸ�� ��ų ���� </summary>
    public Define.SkillState GetSkillType()
    {
        if (loadedSkill == null) return Define.SkillState.Combat;

        return loadedSkill.type;
    }

    /// <summary> �غ����� ��ų ���� ���� </summary>
    public Skill GetreservedSkill()
    {
        return reservedSkill;
    }

    /// <summary> �غ� �Ϸ�� ��ų ���� ���� </summary>
    public Skill GetloadedSkill()
    {
        return loadedSkill;
    }

    /// <summary> ���� ����� ���� </summary>
    public float GetCurrentHP()
    {
        return hitPoint.Current;
    }

    /// <summary> ���콺 �Է����� Ÿ�� ���� �õ�, Ű���� �Է½� Ÿ�� ���� </summary>
    public bool SetTarget(Interactable target)
    {
        if (target == null) //Ÿ���� ���̸�
        {
            focusTarget = null;  //�ٶ󺸴� Ÿ���� null ����
            return false;
        };

        if (IsEnemy(this, target))
        {
            focusTarget = target;
            return true;
        };

        if (IsSheep(this, target))
        {
            focusTarget = target;
            return true;
        }

        if (IsItem(target))
        {
            focusTarget = target;
            return true;
        }
        return false;
    
    }

    /// <summary> �����̽��� �Է����� �ϻ�, ������� ��ȯ </summary>
    public void SetOffensive()
    {
        offensive = !offensive; //���� ���¸� ������
        OffensiveSetting();
    }
    /// <summary> �Ű����� ���� �༭ �ϻ�, ������� ��ȯ </summary>
    public void SetOffensive(bool value)
    {
        offensive = value;
        OffensiveSetting();
    }

    /// <summary> �ϻ�, ������忡 ���� �ִϸ��̼ǰ� �̵��ӵ� ��ȯ </summary>
    void OffensiveSetting()
    {
        PlayAnim("Offensive", offensive); //offensive bool���� ���� �ִϸ������� Offensive bool�� ��ȯ

        if (!offensive && agent.speed >= runSpeed) //�ϻ����̰� ���� �̵��ӵ��� �޸��� �ӵ� �̻��̸�
        {
            walk = true; //�ȱ�� ��ȯ
            agent.speed = walkSpeed; //�ȱ� �ӵ��� ��ȯ
        }
        else if (offensive) //��������̸�
        {
            walk = false; //�ٱ�� ��ȯ
            agent.speed = runSpeed; //�޸��� �ӵ��� ��ȯ
            if (reservedSkill != null) //�غ����� ��ų�� ������
                setMoveSpeed(reservedSkill.type); //�غ����� ��ų �̵��ӵ��� ����
            if (loadedSkill != null) //�غ�Ϸ�� ��ų�� ������
                setMoveSpeed(loadedSkill.type); //�غ�Ϸ�� ��ų �̵��ӵ��� ����
        }
    }

    /// <summary> �ٿ� </summary>
    public void DownCheck()
    {
        rigid.velocity = new Vector3(0, 0, 0);  //������ٵ��� �ӵ��� 0���� �ʱ�ȭ 
        Blowaway();
        wait = Wait(downTime); //���� �Ұ� �ڷ�ƾ
        StartCoroutine(wait);  //���� �Ұ� ����
        PlayAnim("BlowawayA"); //���ư��� �ִϸ��̼� ����
        downGauge.Current = 0; //�ٿ������ �ʱ�ȭ
    }

    /// <summary> ��� üũ </summary>
    public void Dead()
    {
        die = true;  //��� ���·� ��ȯ
        PlayAnim("Die");  //��� Ʈ���� üũ
        rigid.velocity = new Vector3(0, 0, 0); //������ٵ��� �ӵ��� 0���� �ʱ�ȭ 
        StartCoroutine("Die");//��� �ڷ�ƾ ����
    }

    /// <summary> �ִϸ����� �Ķ����(trigger) ����</summary>
    protected void PlayAnim(string wantName)  
    {
        if(wantName == "Combat") //�ִϸ��̼� ��ų�� �ĺ��� ��� 3���� ������ �ִϸ��̼� ����
        {
            anim.SetInteger("CombatNumber", Random.Range(0, 3));
        }
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

    /// <summary> ��ų ������</summary>
    public void Casting(Define.SkillState value)
    {
        loadedSkill = skillList[Define.SkillState.Combat].skill;
        SkillInfo currentSkill = skillList[value];
        if (currentSkill == null) return; //�Էµ� ��ų�� null�� ��� ����

        switch (currentSkill.skill.type)//���� ��ų�� ���� ���� ���ظ� ����
        {

            
            case Define.SkillState.Defense:
                if (staminaPoint.Current < defenseData.CastCost)  //���� ��ų ������ �ʿ��� ���¹̳����� ���� ���
                {
                    return; // �����ϰ� ��ų ���� ����
                }
                staminaPoint.Current -= defenseData.CastCost; // ������븸ŭ ���¹̳� ����
                break;
            case Define.SkillState.Smash:
                if (staminaPoint.Current < smashData.CastCost) //���� ��ų ������ �ʿ��� ���¹̳����� ���� ���
                {
                    return; // �����ϰ� ��ų ���� ����
                }
                staminaPoint.Current -= smashData.CastCost; // ������븸ŭ ���¹̳� ����
                break;
            case Define.SkillState.Counter:
                if (staminaPoint.Current < counterData.CastCost) //���� ��ų ������ �ʿ��� ���¹̳����� ���� ���
                {
                    return; // �����ϰ� ��ų ���� ����
                }
                staminaPoint.Current -= counterData.CastCost; // ������븸ŭ ���¹̳� ����
                break;
        }

        reservedSkill = currentSkill.skill; //�������� ��ų�� ����
        skillCastingTimeLeft = currentSkill.skill.castingTime;//������Ʈ���� ��ŸŸ������ ����//ĵ�� �� skill�� null
    }
    /// <summary> ���� �ð� �ڷ�ƾ</summary>
    public IEnumerator Wait(float time)
    {
        offensive = true; //���� ���� ��ȯ
        waitCount++;//���� �Ұ� ī��Ʈ ����
        yield return new WaitForSeconds(time);
        waitCount--; //���� �Ұ� ī��Ʈ ����
    }
    /// <summary> ���� �׷α� ���¿� ��</summary>
    public void Groggy()
    {
        wait = Wait(groggyTime);
        StartCoroutine(wait);  //���ۺҰ� �ڷ�ƾ ����
        IEnumerator groggy = GroggyDown(); 
        StartCoroutine(groggy); //�׷α� �ٿ� �ڷ�ƾ ����
        PlayAnim("Groggy"); //�׷α� �ִϸ��̼� ����
        downGauge.Current = 0; //�ٿ������ �ʱ�ȭ
    }
    /// <summary> �׷α� ���Ŀ� �ٿ� ���·� ��</summary>
    IEnumerator GroggyDown()
    {
        yield return new WaitForSeconds(1.0f); //1�ʰ� �׷α� �ִϸ��̼��� ����ǵ��� ���
        Blowaway();
    }

    /// <summary> ĳ���Ͱ� �ڷ� �з����� ���ư�</summary>
    void Blowaway()
    {
        rigid.AddForce(gameObject.transform.forward * -600);
        agent.velocity = (Vector3.up * 500);
    }

    IEnumerator Die()
    {
        Blowaway();
        yield return new WaitForSeconds(1.5f); //1.5�� ���
        rigid.constraints = RigidbodyConstraints.FreezeAll; //������ٵ� ������Ŵ
        gameObject.GetComponent<BoxCollider>().enabled = false; //�ݶ��̴� ��
        NavMeshAgent nav = gameObject.GetComponent<NavMeshAgent>();
        nav.speed = 0; //���� �̵��ӵ� 0 ����
        nav.angularSpeed = 0; //���� ȸ���ӵ� 0 ����
        nav.radius = 0.01f; //���� ������ ���� 0 ����
    }

}
