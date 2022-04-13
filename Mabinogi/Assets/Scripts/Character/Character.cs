using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum SkillId
    {
        combat = 0,
        defense = 1,
        smash = 2,
        counter = 3
    }

    public CharacterData characterData;

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
    /// <summary> �ٿ� ������ </summary>
    public int downGauge = 0;
    /// <summary> �ٿ�� �ð� </summary>
    public float downTime = 2.0f;
    /// <summary> ���� üũ </summary>
    public int stiffnessCount = 0;
    /// <summary> ������ ��ų </summary>
    public int currentSkillId;
    /// <summary> ��� Ÿ�� ĳ���� </summary>
    public Character target;
    /// <summary> ��ų ���� �ڷ�ƾ </summary>
    protected IEnumerator cast;
    /// <summary> ���� �ð� �ڷ�ƾ </summary>
    protected IEnumerator stiffness;
    protected bool die = false;
    protected Animator ani;
    protected Rigidbody rigid;
    private void Awake()
    {
        ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        hitPoint = characterData.HitPoint;
        maxHitPoint = characterData.HitPoint;
        manaPoint = characterData.ManaPoint;
        maxManaPoint = characterData.ManaPoint;
        staminaPoint = characterData.StaminaPoint;
        maxStaminaPoint = characterData.StaminaPoint;
        strength = characterData.Strength;
        intelligence = characterData.Intelligence;
        dexterity = characterData.Dexterity;
        will = characterData.Will;
        luck = characterData.Luck;
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
        speed = characterData.Speed;
    }

    protected void SkillCast(Skill skill) //��ų �غ� �õ�
    {
        if(staminaPoint>= skill.skillData.CastCost && stiffnessCount == 0)  //���¹̳��� ��ų �غ� ���¹̳����� ���ų� ���� ���
        {
            StopCoroutine(cast);
            staminaPoint -= skill.skillData.CastCost; //���¹̳� ����
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
        currentSkillId = 0;
    }

    public void Down()//ĳ���� �ٿ� ó��
    {
        if (die) //����� ����̹Ƿ� ����
        {
            return;
        }
        else
        {
            AniOff();
            ani.SetBool("Down", true);
            Stiffness(downTime);  //downTime��ŭ ����
        }
    }
    public void DownCheck()
    {
        if (downGauge >= 100) //�ٿ�������� 100�� ������ �ٿ�
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
        AniOff();
        ani.SetBool("Hit", true);
        SkillCancel();
        float hitDamage = Random.Range(enemyMinDamage, enemyMaxDamage) * enemyBalance * enemyCoefficient;
        hitPoint = hitPoint - (int)hitDamage;
        if (hitPoint <= 0)
        {
            Die();
            return;
        }
        stiffness = Stiffness(enemyStiffnessTime);
        StartCoroutine(stiffness);
        downGauge = downGauge + enemyAttackDownGauge;
        DownCheck();
    }

    public void Hit(int enemyMaxDamage, int enemyMinDamage, float enemyCoefficient, float enemyBalance)//�ٿ�Ǵ� ��ų �ǰ� ó��
    {
        SkillCancel();
        float hitDamage = Random.Range(enemyMinDamage, enemyMaxDamage) * enemyBalance * enemyCoefficient;
        hitPoint = hitPoint - (int)hitDamage;
        if (hitPoint <= 0)
        {
            Die();
            return;
        }
    }

    public void Groggy(float time)//������ ���Žó� ī���� ���� ��� �׷α� ����
    {
        AniOff();
        ani.SetBool("Groggy", true);
        stiffness = Stiffness(time);
        StartCoroutine(stiffness);
        Invoke("Down", 0.2f);
    }
    protected void Die()//������� 0 ������ ��� ��� ó��
    {
        AniOff();
        ani.SetBool("Die", true);
        downGauge = 100;
        Down();
    }

    public IEnumerator Stiffness(float time)//���� �ð� �ڷ�ƾ
    {
        stiffnessCount++;
        yield return new WaitForSeconds(time);
        stiffnessCount--;
    }

    IEnumerator Casting(float time, int skillId)//��ų ���� �ð� �ڷ�ƾ
    {
        yield return new WaitForSeconds(time);
        currentSkillId = skillId;
    }

    public void AniOff()
    {
        foreach (AnimatorControllerParameter parameter in ani.parameters)
        {
            ani.SetBool(parameter.name, false);
        }
    }
    
}
