using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))] //������ٵ� �⺻ �����Ǿ� �־�� ��
[RequireComponent(typeof(BoxCollider))] //�ݶ��̴��� �⺻ �����Ǿ� �־�� ��
/// <summary> ��ȣ�ۿ��ϴ� ��� ĳ���Ϳ� �޸� ��ũ��Ʈ</summary>
public class Character : Movable
{

    #region �ɹ� ����
    [Tooltip("ĳ���� �̸�")]
    /// <summary> ĳ���� �̸� summary>
    public string characterName;
    [Tooltip("�̸� �ؽ�Ʈ UI�� Y����")]
    /// <summary> �̸� �ؽ�Ʈ UI�� Y���� summary>
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
    [Tooltip("ĳ���� ������(�÷��̾�, ��, ���� ��)")]
    public CharacterData data;
    [SerializeField]
    [Tooltip("������ Ÿ��")]
    /// <summary> ������ Ÿ��</summary>
    protected Interactable focusTarget;
    /// <summary> ������ Ÿ���� Ÿ�� enum</summary>
    protected Define.InteractType focusType;
    /// <summary> �̵� Ÿ�� enum</summary>
    protected Define.MoveType movetype;
    /// <summary> �غ� �Ϸ�� ���� ��ų</summary>
    [SerializeField]
    [Tooltip("�غ� �Ϸ�� ���� ��ų")]
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

    //�ɷ�ġ��
    /// <summary> �ִ빰�����ݷ� </summary>
    protected int maxPhysicalStrikingPower;
    /// <summary> �ִ븶�����ݷ� </summary>
    protected int maxMagicStrikingPower;
    /// <summary> �ּҹ������ݷ� </summary>
    protected int minPhysicalStrikingPower;
    /// <summary> �ּҸ������ݷ� </summary>
    protected int minMagicStrikingPower;
    /// <summary> ĳ���Ͱ� ���� �λ� </summary>
    protected int wound;
    /// <summary> ���ݽ� ���濡�� ������ �λ�� </summary>
    protected int woundAttack;
    /// <summary> ġ��Ÿ Ȯ�� </summary>
    protected float critical;
    /// <summary> �뷱��, �ּ�, �ִ� �������� �ߴ� ���� </summary>
    protected float balance;
    /// <summary> ���� ����. 1��1 ������ �������� ���ط� ���� </summary>
    protected int physicalDefensivePower;
    /// <summary> ���� ����. 1��1 ������ �������� ���ط� ���� </summary>
    protected int magicDefensivePower;
    /// <summary> ���� ��ȣ. 1�ۼ�Ʈ ������ ������ ���� </summary>
    protected int physicalProtective;
    /// <summary> ���� ��ȣ. 1�ۼ�Ʈ ������ ������ ���� </summary>
    protected int magicProtective;
    /// <summary> ����� ������ ������ �� �̰ܳ��� ���鸮 ���°� �� Ȯ�� </summary>
    protected int deadly;

    /// <summary> �ٿ� ���� �ð� </summary>
    protected float downTime = 4.0f;
    /// <summary> ���� ���� �ð� </summary>
    protected float attackTime = 1.0f;
    /// <summary> �ǰ� ���� �ð� </summary>
    protected float hitTime = 3.0f;
    /// <summary> �׷α� ���� �ð� </summary>
    protected float groggyTime = 6.0f;
    /// <summary> ���� ���� ���� �ð� </summary>
    protected float attackFailTime = 3.0f;
    /// <summary> �ǰݽ� ���� �Ұ� üũ </summary>
    protected int waitCount = 0;
    /// <summary> �ǰݽ� ���� �Ұ� �ڷ�ƾ </summary>
    protected IEnumerator wait;
    /// <summary> �ǰ� �ִϸ��̼� A, B�� ���� </summary>
    protected int hitCount = 0;
    /// <summary> �׷α� ���� üũ </summary>
    protected bool groggy = false;
    /// <summary> ��� üũ </summary>
    [Tooltip("��� üũ")]
    public bool die = false;
    /// <summary> ������ٵ� </summary>
    protected Rigidbody rigid;
    /// <summary> �ִϸ����� </summary>
    protected Animator anim;

    //��ų������ ��ũ���ͺ� ������Ʈ��
    /// <summary> �⺻���� �ĺ� ��ų ������ </summary>
    protected SkillData combatData;
    /// <summary> ���潺 ��ų ������ </summary>
    protected SkillData defenseData;
    /// <summary> ���Ž� ��ų ������ </summary>
    protected SkillData smashData;
    /// <summary> ī���� ���� ��ų ������ </summary>
    protected SkillData counterData;

    [SerializeField]
    [Tooltip("������̼� ȸ����")]
    /// <summary> ������̼� ȸ���� </summary>
    protected float angularSpeed = 1000f;
    [SerializeField]
    [Tooltip("������̼� ���ӵ�")]
    /// <summary> ������̼� ���ӵ� </summary>
    protected float acceleration = 100f;
    /// <summary> ������ ��� </summary>
    public int gold;
    /// <summary> ��Ȱ�ߴ��� üũ </summary>
    public bool respawn = false;
    


    #endregion

    protected override void Awake()
    {
        base.Awake();
        #region ������ �⺻�� �Ҵ�
        //��ų������ ��ũ���ͺ� ������Ʈ ������ �Ҵ�
        combatData = Define.SkillState.Combat.GetSkillData();
        defenseData = Define.SkillState.Defense.GetSkillData();
        smashData = Define.SkillState.Smash.GetSkillData();
        counterData = Define.SkillState.Counter.GetSkillData();

        rigid = GetComponent<Rigidbody>();//������ٵ� �Ҵ�
        anim = GetComponent<Animator>();//�ִϸ����� �Ҵ�
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

        SetOffensive();//�ϻ���� ��ȯ�ϰ� �̵��ӵ��� �ȱ�� ������

        GameObject namePrefab = Instantiate(Resources.Load<GameObject>("Prefabs/UI/NameUICanvas")); //�̸� ǥ�� ĵ���� ����
        namePrefab.transform.localScale = Vector3.one * 0.01f; //�̸� ǥ�� ĵ���� ũ�� ����
        namePrefab.transform.SetParent(transform); //�̸� ǥ�� ĵ������ �� ĳ������ �ڽ� ������Ʈ�� ����
        namePrefab.transform.localPosition = new Vector3(0, nameYpos, 0); //�̸� ǥ�� ĵ������ ��ǥ�� �� ĳ���� ��ǥ���� nameYpos��ŭ ���� �÷���
        namePrefab.GetComponentInChildren<UnityEngine.UI.Text>().text = characterName; //�̸� ǥ�� ĵ������ �ؽ�Ʈ�� characterName����

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

        DestinationCheck();

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

    void DestinationCheck()
    {
        if(movetype == Define.MoveType.DropItem)
        {
            Vector3 positionDiff = (agent.destination - transform.position);//��� ��ǥ���� �� ��ǥ ��
            positionDiff.y = 0;//���� y�� ������
            float distance = positionDiff.magnitude;  //Ÿ�ٰ� ���� �Ÿ�
            if (distance < 1) //�Ÿ��� ��ȣ�ۿ� ���� �Ÿ����� �� ���
            {
                Inventory.DropItem();
                movetype = Define.MoveType.Move;
                MoveStop(true);
            }
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
                if (focusTarget.Interact(this) == Define.InteractType.Attack)//����� ���̸� �������� �̵�
                {
                    SetOffensive(true); //�������� ��ȯ
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
                    case Define.InteractType.Talk:  //��ȣ�ۿ� Ÿ���� ��ȭ�̸�
                        if (GetType().IsClassOf(typeof(Player)) && focusTarget.GetType().IsClassOf(typeof(NPC)))
                        {
                            ((Player)this).Talk((NPC)focusTarget);//�÷��̾��� ��ȭ �޼��� ����
                        }
                        break;
                    case Define.InteractType.Sheeping:  //��ȣ�ۿ� Ÿ���� ���� ä���̸�
                        if (GetComponent<Player>() != null)
                        {
                            GetComponent<Player>().Sheeping();//�÷��̾��� ����ä�� �޼��� ����
                        }
                        break;
                    case Define.InteractType.Egg:  //��ȣ�ۿ� Ÿ���� �ް� ä���̸�
                        if (GetComponent<Player>() != null)
                        {
                            GetComponent<Player>().Egg();//�÷��̾��� �ް�ä�� �޼��� ����
                        }
                        break;
                    case Define.InteractType.Get:  //��ȣ�ۿ� Ÿ���� ������ �ݱ��̸�
                        ItemInpo itemInpo = focusTarget.GetComponent<ItemInpo>(); //Ÿ���� ������ ���� ��ũ��Ʈ ������Ʈ �������� �õ�
                        if (itemInpo != null)//Ÿ���� ������ ���� ��ũ��Ʈ ������Ʈ�� ������ ������
                        {
                            itemInpo.GetItem(); //������ ���� ��ũ��Ʈ�� ������ �ݱ� �޼��� ����
                        }
                        break;
                    default:
                        Debug.Log("Ÿ�ٰ��� ��ȣ�ۿ뿡�� ������ �߻���");//�̻��� �� ������ �����

                        break;
                };
                focusTarget = null; //��ȣ�ۿ��� �������Ƿ� Ÿ���� ���
            }
        };
    }
    /// <summary> ������ �� �ִ��� üũ </summary>
    bool MovableCheck()
    {
        bool CanMove = true; //������ �� �ִ°�?
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
            setMoveSpeed(reservedSkill.type);//��ų Ÿ�Կ� ���� �̵��ӵ� ����
        }
        else if (skillCastingTimeLeft <= 0 && reservedSkill != null)  //��ų ���� �ð��� �� ������ ���
        {
            loadedSkill = reservedSkill;  //�غ�� ��ų ����
            reservedSkill = null;  // �غ����� ��ų null�� ��ȯ
            if (loadedSkill.type != Define.SkillState.Combat)
            {
                GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.skill_ready);//��ų �غ� �Ϸ� ȿ����
            }
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
        if (target != null && agent.velocity.magnitude <= 1.0f && waitCount==0) //Ÿ���� �����ϰ� �ӵ��� 1 ���Ϸ� ���� ���������鼭 ��������� ������
        {
            Vector3 look = target.transform.position; //look�� Ÿ���� ��ǥ ����
            look.y = transform.position.y; //y��ǥ�� ������
            transform.LookAt(look); //Ÿ���� �ٶ�
        };
    }

    /// <summary> ������ ��ǥ �ٶ� </summary>
    void TargetLookAt(Vector3 target)
    {
        if (target != null && agent.velocity.magnitude <= 1.0f)//Ÿ���� �����ϰ� �ӵ��� 1 ���Ϸ� ���� ����������
        {
            target.y = transform.position.y; //y��ǥ�� ������
            transform.LookAt(target);//Ÿ���� �ٶ�
        };
    }

    /// <summary> ��ȣ�ۿ� ���� �Ÿ� </summary>
    public virtual float InteractableDistance(Define.InteractType wantType)
    {
        switch (wantType)
        {
            case Define.InteractType.Attack://���� �Ÿ�
                {
                    return 4; //���� ��Ÿ��� �þ ��� ���⼭ �����Ѵ�.
                }
            case Define.InteractType.Get: return 1; //������ �ݱ� ���� �Ÿ�
            case Define.InteractType.Sheeping: return 2; //���� ä�� ���� �Ÿ�
            case Define.InteractType.Talk: return 6;  //��ȭ ���� �Ÿ�
            
            default: return 2;  //�⺻���� 2�̴�.
        };
    }

    /// <summary> ���� �Լ�</summary>
    public virtual void Attack(Hitable enemyTarget)//Ÿ���� �����Ѵ�
    {
        SetOffensive(true);//�������� ��ȯ
        this.transform.LookAt(enemyTarget.transform); //Ÿ���� �ٶ󺻴�.

        //���� �Ұ� �����̰ų� �ĺ��ε� ���¹̳��� 2 �̸��̸� ������ �� �ϹǷ�
        if (waitCount != 0 || (loadedSkill.type == Define.SkillState.Combat && staminaPoint.Current < combatData.CastCost))
        {
            return;  //�ٷ� �� �޼��带 ������
        }
        wait = Wait(attackTime); //���� ������ �ڷ�ƾ
        StartCoroutine(wait);

        //TakeDamage�Լ��� ���� ��ų�� Ǯ���� ������ ��ų�� �ӽ� ����
        Character asCharacter = null;
        if (enemyTarget.GetType().IsSubclassOf(typeof(Character))) //enemyTarget�� Ŭ������ ��ӹ��� Ŭ������ Character Ŭ������ �ڽ�Ŭ�����̰ų� Character Ŭ�����̸�
        {
            asCharacter = (Character)enemyTarget;  //���� ĳ���͸� �����
        };

        Define.SkillState otherSkill = Define.SkillState.Combat;  //���� ��ų�� �⺻�� �⺻����
        if (asCharacter != null) otherSkill = asCharacter.GetSkillType();//���� ĳ���Ͱ� �ִ� ��� ���� ��ų ������


        if (enemyTarget.TakeDamage(this) == true)//���ݿ� ������ ���
        {
            PlayAnim(loadedSkill.AnimName); //�ش� ��ų�� �´� �ִϸ����� Ʈ���� ����
            float waitTime = 0.0f;//���� ��� �ð�
            switch (loadedSkill.type)
            {
                case Define.SkillState.Combat: //���������̸� 1�� ���
                    waitTime = 1.0f; //���� ��� �ð�
                    staminaPoint.Current -= combatData.CastCost; //���� ���ݿ� ������ ��� ���¹̳��� 2 ����                   
                    break;
                case Define.SkillState.Smash:
                    waitTime = 4.0f; //���Žø� 4�� ���
                    break;
            };

            wait = Wait(waitTime); //���� ��ų ������ ��ŭ ���
            StartCoroutine(wait);

            loadedSkill = skillList[Define.SkillState.Combat].skill;//���� �� �⺻ �������� �غ�� ��ų �ʱ�ȭ
        }
        else//���ݿ� ������ ���
        {
            if (asCharacter != null) //Ÿ���� �����ϴ°��
            {
                switch (otherSkill)
                {
                    //������ ������ ��쿡�� ���� ����ڰ� ���ϰ��� �޾Ƽ� ������ ������ �ɸ��� �ؾ���
                    //���潺 ���� �����ڴ� ���� ����� ���������� ���ɸ�
                    case Define.SkillState.Defense:
                        PlayAnim("Combat");//���ݿ� ���������� ���� �ִϸ��̼��� ����Ѵ�
                        wait = Wait(attackFailTime); //���� ���з� 3�ʰ� ����
                        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.guard);//�ݱ� ȿ����
                        StartCoroutine(wait);
                        break;

                    //ī���ʹ� �ݰ� ���ϰ� �ٿ��
                    case Define.SkillState.Counter:
                        this.downGauge.Current += counterData.DownGauge;//�ٿ�������� 100 �߰�
                        float damage = asCharacter.CalculateDamage(Define.SkillState.Counter); //������ ���
                        this.hitPoint.Current -= damage; //���� ����¿��� ������ ��ŭ ����
                        Groggy();//�ݰ� �������Ƿ� �׷α� ���¿� ����
                        break;
                }
            };
            loadedSkill = skillList[Define.SkillState.Combat].skill;// �� ��ų �ʱ�ȭ

        };
    }
    //other�� ��ȣ�ۿ��� �Ϸ��� ���� ��, ��ȯ���� �������� �� ���
    //this�� �� �ڽ� ĳ���� ��ũ��Ʈ ������Ʈ
    //�� �ڽ� ĳ���� ��ũ��Ʈ ������Ʈ�� Interact�޼��带 �����ϸ� ���� Ÿ���� �����Ѵ�.
    //focusTarget.Interact(this)�� �Ű����� other�� �� �ڽ� this�� ���� focusTarget�� Ÿ���� �����Ѵ�.
    public override Define.InteractType Interact(Interactable other)
    {
        if (IsEnemy(this, other)) //����� ���� ������ üũ
        {
            return Define.InteractType.Attack; //��ȣ�ۿ� Ÿ���� �������� ����
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

    public void MoveTo(Vector3 goalPosition, Define.MoveType currentMoveType ,bool isWalk = false)  //������̼� �̵� �޼���
    {
        base.MoveTo(goalPosition, isWalk);
        movetype = currentMoveType;
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
        if (Attacker.loadedSkill != null && this.focusTarget == Attacker || (this.loadedSkill != null && this.loadedSkill.mustCheck))
        {
            result = Attacker.loadedSkill.WinnerCheck(this.loadedSkill); //���� ��ų�� �� ��ų�� �켱���� ��
        };


        if (result == true) //������ ������ ������ ���
        {
            float damage = 0.0f;
            switch (Attacker.loadedSkill.type)//���� ��ų�� ���� ���� ���ظ� ����
            {
                case Define.SkillState.Combat: //���������� ���
                    this.downGauge.Current += combatData.DownGauge; //�ٿ�������� 40 ä����
                    damage = Attacker.CalculateDamage(Define.SkillState.Combat); //������ ���
                    break;
                case Define.SkillState.Smash: //���Ž��� ���
                    damage = Attacker.CalculateDamage(Define.SkillState.Smash); //������ ���
                    groggy = true; //�׷α� ���·� ��ȯ
                    this.downGauge.Current += smashData.DownGauge; //�ٿ�������� 100 ä����
                    break;
            }
            this.hitPoint.Current -= damage;//���� ����¿��� ��������ŭ ����

            if (this.hitPoint.Current <= 0.2)//������� 0.2������ ��� ���
            {                
                Dead();
            }
            else if (this.downGauge.Current < 100) //�ٿ�������� 100 ������ ���
            {
                //�׷α� ���°ų� ���ư��� ���°� �ƴϸ�
                if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Groggy" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Blowaway_Ground")
                {
                    PlayAnim("HitA");  //�ǰ� �ִϸ��̼� ���
                    GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.punch_hit);//��ġ ȿ����
                }
                wait = Wait(hitTime); // �ǰ� ���ӽð���ŭ ����
                StartCoroutine(wait);

            }
            else if (this.downGauge.Current >= 100)//�ٿ�������� 100 �̻��� ���
            {
                if (groggy) //�׷α� ���¿� ���� �ϸ�
                {
                    Groggy();//�׷α� ���·� ��
                    groggy = false; //�׷α� ���� ����
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
        switch (type)
        {
            case Define.SkillState.Smash://���Ž� ������ ���
                return maxPhysicalStrikingPower * smashData.Coefficient * skillList[Define.SkillState.Smash].rank;
            case Define.SkillState.Combat://���� ���� ������ ���
                return maxPhysicalStrikingPower * combatData.Coefficient * skillList[Define.SkillState.Combat].rank;
            case Define.SkillState.Counter://ī���� ������ ���
                return maxPhysicalStrikingPower * counterData.Coefficient * skillList[Define.SkillState.Counter].rank;
        };
        return 0; //�̻��Ѱ� ������ 0 ����
    }

    /// <summary> �غ� �Ϸ�� ��ų ���� </summary>
    public Define.SkillState GetSkillType()
    {
        if (loadedSkill == null) return Define.SkillState.Combat; //�غ� �Ϸ�� ��ų�� null�̸� �⺻������ �־���

        return loadedSkill.type;//�غ� �Ϸ�� ��ų ��ȯ
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
        focusTarget = target;
        return true;

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
        Blowaway();//ĳ���Ͱ� �ڷ� ���ư�
        wait = Wait(downTime); //���� �Ұ� �ڷ�ƾ
        StartCoroutine(wait);  //���� �Ұ� ����
        PlayAnim("BlowawayA"); //���ư��� �ִϸ��̼� ����

        downGauge.Current = 0; //�ٿ������ �ʱ�ȭ
    }

    /// <summary> ��� �� ����Ǵ� �޼��� </summary>
    public void Dead()
    {
        waitCount = 1;
        Blowaway();
        die = true;  //��� ���·� ��ȯ
        PlayAnim("Die");  //��� Ʈ���� üũ
        StartCoroutine("Die");//��� �ڷ�ƾ ����
    }

    /// <summary> �ִϸ����� �Ķ����(trigger) ����</summary>
    protected void PlayAnim(string wantName)
    {
        if (wantName == "Combat") //�ִϸ��̼� ��ų�� �ĺ��� ��� 
        {
            anim.SetInteger("CombatNumber", Random.Range(0, 3));//3���� ������ �ִϸ��̼� ����
        }
        if (anim != null) anim.SetTrigger(wantName);//�ִϸ������� wantName Trigger üũ
    }
    /// <summary> �ִϸ����� �Ķ����(bool) ����</summary>
    protected void PlayAnim(string wantName, bool value)
    {
        if (anim != null) anim.SetBool(wantName, value); //�ִϸ������� wantName bool üũ
    }
    /// <summary> �ִϸ����� �Ķ����(float) ����</summary>
    protected void PlayAnim(string wantName, float value)
    {
        if (anim != null) anim.SetFloat(wantName, value); //�ִϸ������� wantName float üũ
    }
    /// <summary> �ִϸ����� �Ķ����(int) ����</summary>
    protected void PlayAnim(string wantName, int value)
    {
        if (anim != null) anim.SetInteger(wantName, value); //�ִϸ������� wantName ���� üũ
    }

    /// <summary> ��ų ������</summary>
    public void Casting(Define.SkillState value)
    {
        loadedSkill = skillList[Define.SkillState.Combat].skill; //�غ� �Ϸ�� ��ų�� ����ϰ� �⺻���� �⺻�������� ��ȯ
        SkillInfo currentSkill = skillList[value]; //���� ��ų�� skillList[value]�̴�.
        if (currentSkill == null) return; //�Էµ� ���� ��ų�� null�� ��� ����
        switch (currentSkill.skill.type)//���� ��ų�� ���� ���� ���ظ� ����
        {
            case Define.SkillState.Defense:
                if (staminaPoint.Current < defenseData.CastCost)  //���� ��ų ������ �ʿ��� ���¹̳����� ���� ���
                {
                    return; // �����ϰ� ��ų ���� ����
                }
                GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.skill_standby);//��ų �غ��� ȿ����
                staminaPoint.Current -= defenseData.CastCost; // ������븸ŭ ���¹̳� ����
                break;
            case Define.SkillState.Smash:
                if (staminaPoint.Current < smashData.CastCost) //���� ��ų ������ �ʿ��� ���¹̳����� ���� ���
                {
                    return; // �����ϰ� ��ų ���� ����
                }
                GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.skill_standby);//��ų �غ��� ȿ����
                staminaPoint.Current -= smashData.CastCost; // ������븸ŭ ���¹̳� ����
                break;
            case Define.SkillState.Counter:
                if (staminaPoint.Current < counterData.CastCost) //���� ��ų ������ �ʿ��� ���¹̳����� ���� ���
                {
                    return; // �����ϰ� ��ų ���� ����
                }
                GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.skill_standby);//��ų �غ��� ȿ����
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
        if (this.hitPoint.Current <= 0.2)//������� 0.2������ ��� ���
        {
            Dead();
        }
        else
        {
            Blowaway();
        }
    }

    ///<summary> ĳ���Ͱ� �ڷ� �з����� ���ư�</summary>
    void Blowaway()
    {
        rigid.velocity = new Vector3(0, 0, 0);  //������ٵ��� �ӵ��� 0���� �ʱ�ȭ 
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.punch_blow);//���ư��� ȿ����
        rigid.AddForce(gameObject.transform.forward * -600);//�ڷ� �и�
        agent.velocity = (Vector3.up * 500); //���� ����
    }

    /// <summary> ��� �ڷ�ƾ</summary>
    IEnumerator Die()
    {
        yield return new WaitForSeconds(1f); //1�� ���
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.down);//��� ȿ����
        yield return new WaitForSeconds(0.5f); //0.5�� ���
        gameObject.GetComponent<BoxCollider>().enabled = false; //�ݶ��̴� ��
        yield return new WaitForSeconds(5f);
        //Respawn();
    }

    /// <summary> ĳ���� ��Ȱ</summary>
    public void Respawn()
    {
        waitCount = 0;
        die = false;
        hitPoint.Current = hitPoint.Max;
        PlayAnim("Reset");//�ִϸ��̼��� idle�� ��ȯ
        Vector3 spawnPosition = GetRandomPointOnNavMesh(transform.position, 7); //��� ��ġ ��ó���� ����޽� ���� ���� ��ġ ��������
        spawnPosition += Vector3.up * 2;  //�ٴڿ��� 2��ŭ y��ǥ ���� �ø���
        transform.position = spawnPosition;
        gameObject.GetComponent<BoxCollider>().enabled = true; //�ݶ��̴� ��     
        MoveTo(spawnPosition);//����޽� �̵����� �ʱ�ȭ
        respawn = true; //�ΰ����� ����
    }

    //����޽� ���� ������ ��ġ�� ��ȯ�ϴ� �޼���
    /// <summary> center�� �߽����� distance �ݰ� �ȿ����� ������ ��ġ�� ã�� </summary>
    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        // center�� �߽����� �������� maxDistance�� �� �ȿ����� ������ ��ġ �ϳ��� ����
        // Random.insideUnitSphere�� �������� 1�� �� �ȿ����� ������ �� ���� ��ȯ�ϴ� ������Ƽ
        Vector3 randomPos = (Random.insideUnitSphere * distance) + center;

        //����޽� ���ø��� ��� ������ �����ϴ� ����
        NavMeshHit hit;
        //maxDistance �ݰ� �ȿ��� randomPos�� ���� ����� ����޽� ���� �� ���� ã��
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);//out = ������� �Ű�����
        //ã�� �� ��ȯ
        return hit.position;
    }
}
