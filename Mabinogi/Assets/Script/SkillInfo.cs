using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�� �� ���θ� �������� �޷��� ������ �����ض�!
//�ƴϸ� �׳� �ٷ� ���!
public class SkillType
{
    public float castingTime;
    //����� ������ ���°� �ƴ϶�� �ϴ��� ������ ���� Ȯ��
    public virtual bool IsPatrol() { return false; }
    public virtual bool ValidCheck(SkillType other) { return true; }//���տ��� �̱�� Ʈ�� ���� �޽�
}

public class CombatSkill : SkillType
{
    public override bool ValidCheck(SkillType other) 
    {
        System.Type otherType = other.GetType();

        if (otherType == typeof(Defensee))
        {
            return false;
        }
        else if(otherType == typeof(Counter))
        {
            return false;
        };

        return true;
    }
}

public class CombatMastery : CombatSkill { }
public class Smashh : CombatSkill 
{
    public override bool ValidCheck(SkillType other)
    {
        System.Type otherType = other.GetType();

        if (otherType == typeof(CombatMastery))
        {
            return false;
        }
        else if (otherType == typeof(Counter))
        {
            return false;
        };

        return true;
    }
}
public class Defensee : CombatSkill //�߰� �����ϼ���
{
    public override bool IsPatrol() { return true; }
}
public class Counter : CombatSkill
{
    public override bool IsPatrol() { return true; }
}

//�̰Ŵ� ��ų �ϳ���!
public class SkillInfo
{
    public SkillType type;
    public int rank;
}

//JSON���� ����
//ó���� ������ �� dogSkill�� �ʱ�ȭ���ְ�
//�� static �ȿ� �ִ� ������ �ʱ�ȭ����!
//�Ŵ������� �ſ��� �ʱ�ȭ���ָ� ��! �ʱ�ȭ���� �� new�� �ٿ��� �� ��!
public class SkillList
{
    SkillInfo[] skills;

    public static SkillList dog;
    public static SkillList chicken;
    public static SkillList wolf;
    //��ŸƮ���� ��ų����Ʈ�� new �ٿ��� �ν��Ͻ� ���� json ���� ���� ������ �����ϼ���
    //������ �����ϰ� �ҷ��ö��� ��ųID enum���� ������ enum�� ����ġ ������ SkillList Ŭ������ ����
}

