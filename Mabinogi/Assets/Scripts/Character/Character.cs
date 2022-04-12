using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterData characterData;
    public SkillData combat;
    public SkillData defense;
    public SkillData smash;
    public SkillData counterAttack;

    /// <summary> �����. �ǰݽ� ���� </summary>
    protected int hitPoint;
    /// <summary> ����. ���� ������ ���� </summary>
    protected int manaPoint;
    /// <summary> ���¹̳�. ��ų ������ ���� </summary>
    protected int staminaPoint;
    /// <summary> ü��. ���� ���ݷ¿� ������ �� </summary>
    protected int strength;
    /// <summary> ����. �������ݷ¿� ������ �� </summary>
    protected int intelligence;
    /// <summary> �ؾ�. �뷱���� ������ �� </summary>
    protected int dexterity;
    /// <summary> ����. ������ �̰ܳ��� ���鸮 ���°� �� Ȯ���� �����Ѵ� </summary>
    protected int will;
    /// <summary> ���. ġ��Ÿ Ȯ���� ������ �� </summary>
    protected int luck;
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
    /// <summary> �̵� �ӵ� </summary>
    protected int speed;
    /// <summary> �ٿ� ������ </summary>
    protected int downGauge = 0;
    /// <summary> �ٿ�� �ð� </summary>
    protected float downTime = 2.0f;
    /// <summary> �׷α� �ð� </summary>
    protected float groggyTime = 0.5f;
    /// <summary> ���� üũ </summary>
    protected int stiffnessCount = 0;
    /// <summary> ������ ��ų </summary>
    public int currentSkillId;
    /// <summary> ��� Ÿ�� ĳ���� </summary>
    public Character target;
    /// <summary> ��ų ���� �ڷ�ƾ </summary>
    protected IEnumerator cast;
    /// <summary> ���� �ð� �ڷ�ƾ </summary>
    protected IEnumerator stiffness;

    private void Awake()
    {
        hitPoint = characterData.HitPoint;
        manaPoint = characterData.ManaPoint;
        staminaPoint = characterData.StaminaPoint;
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
    protected void Move(Vector3 destination)//ĳ���� �̵�
    {

    }

    protected void SkillCast(SkillData skilldata) //��ų �غ� �õ�
    {
        if(staminaPoint>= skilldata.CastCost && stiffnessCount == 0)  //���¹̳��� ��ų �غ� ���¹̳����� ���ų� ���� ���
        {
            StopCoroutine(cast);
            staminaPoint -= skilldata.CastCost; //���¹̳� ����
            cast = Casting(skilldata.CastTime, skilldata.SkillId);
            StartCoroutine(cast); // ��ų ���� �ڷ�ƾ ����
        }
    }

    protected void SkillCancel()//��ų ĵ��
    {
        StopCoroutine(cast);
        currentSkillId = 0;
    }

    protected void Combat(SkillData combat)//Ÿ�ٿ� ��Ÿ ���� ����
    {
        target.downGauge += combat.DownGauge;
    }

    protected void CounterAttack(SkillData counterAttack)//Ÿ�ٿ� ī���� ���� ����
    {
        target.downGauge += counterAttack.DownGauge;
    }
    protected void Defense(SkillData defense)//Ÿ�ٿ��� ���潺 ����
    {
        target.Freeze(defense.Coefficient);//���潺 ��ų �����ŭ ����
    }
    protected void Smash(SkillData smash)//Ÿ�ٿ� ���Ž� ����
    {
        Groggy(groggyTime);
        target.downGauge += smash.DownGauge;

    }
    public void Down()//ĳ���� �ٿ� ó��
    {
        if (downGauge >= 100) //�ٿ�������� 100�̻��� ���� ����� ����̹Ƿ� ����
        {
            return;
        }
        else
        {
            Stiffness(downTime);  //downTime��ŭ ����
        }
    }
    public void Freeze(float time)//���潺�� ���� ��쳪 ī���� ���ÿ� ���� ��� ���� ó��
    {
        Stiffness(time);
    }

    public void Hit(Character enemy, int damage)//�ǰ� ó��
    {
        target = enemy;
        if (target.currentSkillId == 0 && currentSkillId == 1)
        {
            Defense(defense);
        }
        else
        {
            if (hitPoint <= 0)
            {
                Die();
                return;
            }
            SkillCancel();
            float stiffnessTime = 0;
            switch (target.currentSkillId)
            {
                case 0:
                    stiffnessTime = target.combat.StiffnessTime;
                    break;
                case 1:
                    stiffnessTime = target.defense.StiffnessTime;
                    break;
                case 2:
                    stiffnessTime = target.smash.StiffnessTime;
                    Groggy(groggyTime);
                    break;
                case 3:
                    stiffnessTime = target.counterAttack.StiffnessTime;
                    break;
            }
            stiffness = Stiffness(stiffnessTime);
            StartCoroutine(stiffness);
        }
        if (downGauge >= 100)
        {
            Down();
            downGauge = 0;
        }
    }
    public void Groggy(float time)//������ ���Žø� ���� ��� �׷α� ����
    {
        stiffness = Stiffness(time);
        StartCoroutine(stiffness);
    }
    protected void Die()//������� 0 ������ ��� ��� ó��
    {
        downGauge = 100;
        Down();
    }

    IEnumerator Stiffness(float time)//���� �ð� �ڷ�ƾ
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
}
