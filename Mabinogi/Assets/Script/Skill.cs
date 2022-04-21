using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//            ��ȯ��      ��������Ʈ �̸�    �Ű�����
public delegate bool winnerCheckDelegate(Skill other);

//�� �� ���θ� �������� �޷��� ������ �����ض�!
//�ƴϸ� �׳� �ٷ� ���!
public class Skill
{
    public winnerCheckDelegate WinnerCheck;//���տ��� �̱�� Ʈ�� ���� �޽�

    public Define.SkillState type;

    public float castingTime;

    public bool mustCheck; //����� ������ ���°� �ƴ϶�� �ϴ��� ������ ���� Ȯ��S

    public Skill(Define.SkillState wantType, float wantCastingTime, winnerCheckDelegate wantWinnerCheck, bool wantMustCheck = false)
    {
        type = wantType;
        castingTime = wantCastingTime;
        WinnerCheck = wantWinnerCheck;
        mustCheck = wantMustCheck;
    }

    //                                              Ÿ��,       �����ð�,    �̱�� �� üũ,    ������üũ����
    public static Skill combatMastery   = new Skill(Define.SkillState.Combat, 0.0f, CombatNormal);
    public static Skill smash           = new Skill(Define.SkillState.Smash, 1.0f, CombatBreakDefense);
    public static Skill counterAttack   = new Skill(Define.SkillState.Counter, 1.5f, AllwaysWinner, true);
    public static Skill defense         = new Skill(Define.SkillState.Defense, 1.0f, LoseForSmash, true);

    static bool CombatNormal(Skill other)
    {
        switch(other.type)
        {
            case Define.SkillState.Defense:
            case Define.SkillState.Counter: return false;
            default: return true;
        };
    }

    static bool CombatBreakDefense(Skill other)
    {
        switch (other.type)
        {
            case Define.SkillState.Counter: return false;
            default: return true;
        };
    }

    static bool LoseForSmash(Skill other)
    {
        switch (other.type)
        {
            case Define.SkillState.Smash: return false;
            default: return true;
        };
    }

    static bool AllwaysWinner(Skill other)
    {
        return true;
    }
}

//�̰Ŵ� ��ų �ϳ���!
public class SkillInfo
{
    public Skill skill;
    public int rank;

    public SkillInfo(Define.SkillState wantType, int wantRank) 
    {
        skill = wantType.GetSkill();
        rank = wantRank;
    }
}


/// <summary> ���� ��� ��ų</summary>
public class SkillList
{

    SkillInfo[] skills;
    static CharacterSkill dogSkill = Resources.Load<CharacterSkill>("Data/CharacterSkill/DogSkill");
    public SkillInfo this[int index]
    {
        get 
        {
            if (index >= skills.Length || index < 0) return null;

            return skills[index]; 
        }
    }

    public SkillInfo this[Define.SkillState type]
    {
        get
        {
            foreach(SkillInfo current in skills)
            {
                if (current.skill.type == type) return current;
            };

            return null;
        }
    }

    public SkillList(SkillInfo[] value) { skills = value; }

    public static SkillList dog = new SkillList(new SkillInfo[]
    {       
        new SkillInfo(Define.SkillState.Combat, dogSkill.CombatRank),
        new SkillInfo(Define.SkillState.Smash, dogSkill.SmashRank),
        new SkillInfo(Define.SkillState.Defense, dogSkill.DefenseRank),
        new SkillInfo(Define.SkillState.Counter, dogSkill.CounterRank),
    });
    public static SkillList chicken;

    public static SkillList wolf = new SkillList(new SkillInfo[]
    {
        new SkillInfo(Define.SkillState.Combat, 4),
        new SkillInfo(Define.SkillState.Smash, 1),
        new SkillInfo(Define.SkillState.Defense, 1),
        new SkillInfo(Define.SkillState.Counter, 1),
    });
    //��ŸƮ���� ��ų����Ʈ�� new �ٿ��� �ν��Ͻ� ���� json ���� ���� ������ �����ϼ���
    //������ �����ϰ� �ҷ��ö��� ��ųID enum���� ������ enum�� ����ġ ������ SkillList Ŭ������ ����
}

