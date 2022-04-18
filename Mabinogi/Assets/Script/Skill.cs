using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�� �� ���θ� �������� �޷��� ������ �����ض�!
//�ƴϸ� �׳� �ٷ� ���!
public class Skill
{
    public float castingTime;
    
    public virtual bool mustCheck() { return false; } //����� ������ ���°� �ƴ϶�� �ϴ��� ������ ���� Ȯ��
    public virtual bool WinnerCheck(Skill other) { return true; }//���տ��� �̱�� Ʈ�� ���� �޽�
}

public class BattleSkill : Skill
{
    public override bool WinnerCheck(Skill other) 
    {
        System.Type otherType = other.GetType();

        if (otherType == typeof(Defense))
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

public class CombatMastery : BattleSkill 
{
    public override bool WinnerCheck(Skill other)
    {
        return true;
    }
}
public class Smash : BattleSkill 
{
    public override bool WinnerCheck(Skill other)
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
public class Defense : BattleSkill //�߰� �����ϼ���
{
    public override bool mustCheck() { return true; }
}
public class Counter : BattleSkill
{
    public override bool mustCheck() { return true; }
}

//�̰Ŵ� ��ų �ϳ���!
public class SkillInfo
{
    public Skill type;
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

