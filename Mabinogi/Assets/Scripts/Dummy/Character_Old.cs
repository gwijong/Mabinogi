using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGauge_Old
{
    /// <summary> �����. �ǰݽ� ���� </summary>
    public int hitPoint;
    /// <summary> �ִ� ����� </summary>
    public int maxHitPoint;
    /// <summary> ����. ���� ������ ���� </summary>
    public int manaPoint;
    /// <summary> �ִ� ���� </summary>
    public int maxManaPoint;
    /// <summary> ���¹̳�. ��ų ������ ���� </summary>
    public int staminaPoint;
    /// <summary> �ִ� ���¹̳� </summary>
    public int maxStaminaPoint;
}
public class CharacterStatus_Old
{
    /// <summary> ü��. ���� ���ݷ¿� ������ �� </summary>
    public int strength;
    /// <summary> ����. �������ݷ¿� ������ �� </summary>
    public int intelligence;
    /// <summary> �ؾ�. �뷱���� ������ �� </summary>
    public int dexterity;
    /// <summary> ����. ������ �̰ܳ��� ���鸮 ���°� �� Ȯ���� �����Ѵ� </summary>
    public int will;
    /// <summary> ���. ġ��Ÿ Ȯ���� ������ �� </summary>
    public int luck;
}

public class CharacterSkill_Old
{
    public Skill_Old type;
    public int rank;
}

public class Character_Old : CharacterState_Old
{
    public CharacterData characterData;



    public CharacterGauge_Old gauge = new CharacterGauge_Old();
    public CharacterStatus_Old stat = new CharacterStatus_Old();
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
    /// <summary> �ٿ� ������ </summary>
    public int downGauge = 0;
    /// <summary> �ٿ�� �ð� </summary>
    public float downTime = 4.0f;
    /// <summary> ���� üũ </summary>
    public int stiffnessCount = 0;
    /// <summary> ������ ��ų </summary>
    public Define.SkillState currentSkillId;
    /// <summary> ��� Ÿ�� ĳ���� </summary>
    public Character_Old target;
    /// <summary> ��ų ���� �ڷ�ƾ </summary>
    protected IEnumerator cast;
    /// <summary> ���� �ð� �ڷ�ƾ </summary>
    protected IEnumerator stiffness;
    /// <summary> �ǰ� �ִϸ��̼ǿ� ���� </summary>
    protected int hitCount = 0;

    protected Rigidbody rigid;

    protected override void Awake()
    {
        base.Awake();
        rigid = GetComponent<Rigidbody>();
        
    }
    void OnEnable()
    {
        gauge.hitPoint = characterData.HitPoint;
        gauge.maxHitPoint = characterData.HitPoint;
        gauge.manaPoint = characterData.ManaPoint;
        gauge.maxManaPoint = characterData.ManaPoint;
        gauge.staminaPoint = characterData.StaminaPoint;
        gauge.maxStaminaPoint = characterData.StaminaPoint;
        maxPhysicalStrikingPower = characterData.MaxPhysicalStrikingPower;
        maxMagicStrikingPower = characterData.MaxMagicStrikingPower;
        minPhysicalStrikingPower = characterData.MinPhysicalStrikingPower;
        minMagicStrikingPower = characterData.MinMagicStrikingPower;
        wound = characterData.Wound;
        woundAttack = characterData.WoundAttack;
        critical = characterData.Critical;
        balance = characterData.Balance;
        physicalDefensivePower = characterData.PhysicalDefensivePower;
        magicDefensivePower = characterData.MagicDefensivePower;
        physicalProtective = characterData.PhysicalProtective;
        magicProtective = characterData.MagicProtective;
        deadly = characterData.Deadly;
        
    }
    protected virtual void Update()
    {
        OffensiveCheck();
    }

    protected void SkillCast(Skill_Old skill) //��ų �غ� �õ�
    {
        if (State == Define.State.Die)
        {
            return;
        }

        if (gauge.staminaPoint>= skill.skillData.CastCost && stiffnessCount == 0)  //���¹̳��� ��ų �غ� ���¹̳����� ���ų� ���� ���
        {
            if (cast != null)
            {
                StopCoroutine(cast);
            }
            gauge.staminaPoint -= skill.skillData.CastCost; //���¹̳� ����
            cast = Casting(skill.skillData.CastTime, skill.skillData.SkillId);
            StartCoroutine(cast); // ��ų ���� �ڷ�ƾ ����
        }
    }

    protected void SkillCancel()//��ų ĵ��
    {
        if (cast != null)
        {
            StopCoroutine(cast);
        }
        currentSkillId = Define.SkillState.Combat;
    }

    public void Down()//ĳ���� �ٿ� ó��
    {
        rigid.AddForce(gameObject.transform.forward *-600);
        rigid.AddForce(gameObject.transform.up * 200);
        AniOff();
        offensive = true;
        ani.SetBool("BlowawayA", true);
        StartCoroutine("BlowawayTimer");
        if (gauge.hitPoint <= 0)
        {
            Die();
            return;
        }
        stiffness = Stiffness(downTime);
        StartCoroutine(stiffness); //downTime��ŭ ����
    }
    IEnumerator BlowawayTimer()
    {
        yield return new WaitForSeconds(0.05f);
        ani.SetBool("BlowawayA", false);
    }

    public void DownCheck()
    {
        if (downGauge >= 100 || gauge.hitPoint <= 0) //�ٿ�������� 100�� ������ �ٿ�
        {
            Down();
            downGauge = 0;
        }
    }

    public void Freeze(float time)//���潺�� ���� ��� ����
    {
        stiffness = Stiffness(time);
        StartCoroutine(stiffness);
    }

    public void Hit(int enemyMaxDamage, int enemyMinDamage, float enemyCoefficient, 
        float enemyBalance, float enemyStiffnessTime, int enemyAttackDownGauge)//��Ÿ �ǰ� ó��
    {
        if (State == Define.State.Die)
        {
            return;
        }
        AniOff();
        offensive = true;

        ani.SetBool("Hit" + ('A' + (hitCount++ % 2)), true);

        SkillCancel();
        float hitDamage = Random.Range(enemyMinDamage, enemyMaxDamage) * enemyBalance * enemyCoefficient;
        gauge.hitPoint -= (int)hitDamage;
        stiffness = Stiffness(enemyStiffnessTime);
        StartCoroutine(stiffness);
        downGauge += enemyAttackDownGauge;
        DownCheck();
    }

    public void Hit(int enemyMaxDamage, int enemyMinDamage, float enemyCoefficient, float enemyBalance)//�ٿ�Ǵ� ��ų �ǰ� ó��
    {
        if (State == Define.State.Die)
        {
            return;
        }
        SkillCancel();
        float hitDamage = Random.Range(enemyMinDamage, enemyMaxDamage) * enemyBalance * enemyCoefficient;
        gauge.hitPoint -= (int)hitDamage;
    }

    public void Groggy(float time)//������ ���Žó� ī���� ���� ��� �׷α� ����
    {
        AniOff();
        offensive = true;
        ani.SetBool("Groggy", true);
        stiffness = Stiffness(time);
        StartCoroutine(stiffness);
        Invoke("Down", 0.2f);
    }
    protected void Die()//������� 0 ������ ��� ��� ó��
    {
        State = Define.State.Die;       
    }

    public IEnumerator Stiffness(float time)//���� �ð� �ڷ�ƾ
    {
        offensive = true;
        stiffnessCount++;
        yield return new WaitForSeconds(time);
        stiffnessCount--;
        if (stiffnessCount == 0&& State != Define.State.Die)
        {
            AniOff();
            offensive = true;
        }
    }

    IEnumerator Casting(float time, Define.SkillState skillId)//��ų ���� �ð� �ڷ�ƾ
    {
        yield return new WaitForSeconds(time);
        currentSkillId = skillId;
    }

    public override void AniOff()
    {
        base.AniOff();
    }

    public void OffensiveCheck()
    {
        ani.SetBool("Offensive", offensive);

        if (target != null && target.State != Define.State.Die)
        {
            offensive = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && offensive == true)
        {
            offensive = false;
            target = null;

        }
        else if (Input.GetKeyDown(KeyCode.Space) && offensive == false)
        {
            offensive = true;
        }
    }

    public void Attack()
    {
        switch (currentSkillId)
        {
            case Define.SkillState.Combat:
                offensive = true;
                GetComponent<Combat_Old>().SkillUse(target);
                break;
            case Define.SkillState.Smash:
                offensive = true;
                GetComponent<Smash_Old>().SkillUse(target);
                break;
        }
        AttackWait(1f);
    }
    IEnumerator AttackWait(float time)
    {
        stiffnessCount++;
        yield return new WaitForSeconds(time);
        stiffnessCount--;
    }
}
