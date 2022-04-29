using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary> �¸��� üũ ��������Ʈ</summary>
//            ��ȯ��      ��������Ʈ �̸�    �Ű�����
public delegate bool winnerCheckDelegate(Skill other);

/// <summary> �� �� ���θ� �������� �޷����� �����ϰ� �ƴϸ� �ٷ� ��ų ���</summary>
public class Skill
{
    /// <summary> ���տ��� �̱�� Ʈ��</summary>
    public winnerCheckDelegate WinnerCheck;//���տ��� �̱�� Ʈ�� ���� �޽�

    /// <summary> ��ų Ÿ��</summary>
    public Define.SkillState type;

    public string AnimName;

    /// <summary> ��ų ���� �ð�</summary>
    public float castingTime;

    /// <summary> ������ ���� üũ</summary>
    public bool mustCheck;
    /// <summary> ���� �Ұ� ��ų�� ���</summary>
    public bool cannotAttack;

    /// <summary> Skill Ŭ���� ������</summary>
    public Skill(Define.SkillState wantType, string wantAnimName, float wantCastingTime, winnerCheckDelegate wantWinnerCheck, bool wantMustCheck = false, bool wantCannotAttack = false)
    {
        type = wantType;
        castingTime = wantCastingTime;
        WinnerCheck = wantWinnerCheck;
        mustCheck = wantMustCheck;
        cannotAttack = wantCannotAttack;
        AnimName = wantAnimName;
    }

    //                                                               Ÿ��,�����ð�,�̱�� �� üũ,������üũ����
    public static Skill combatMastery   = new Skill(Define.SkillState.Combat, "Combat", 0.0f, CombatWinCheck);
    public static Skill smash           = new Skill(Define.SkillState.Smash, "Smash", 1.0f, SmashWinCheck);
    public static Skill counterAttack   = new Skill(Define.SkillState.Counter, "Counter", 1.5f, CounterWinCheck, true, true);
    public static Skill defense         = new Skill(Define.SkillState.Defense, "Defense", 1.0f, DefenseWinCheck, true, true);

    /// <summary> ���� ��Ÿ�� �̱�� ���� true ���� ��� false ��ȯ</summary>
    static bool CombatWinCheck(Skill other)
    {
        switch(other.type)
        {
            case Define.SkillState.Defense:
            case Define.SkillState.Counter: return false;
            default: return true;
        };
    }

    /// <summary> ���Žð� �̱�� ���� true ���� ��� false ��ȯ</summary>
    static bool SmashWinCheck(Skill other)
    {
        switch (other.type)
        {
            case Define.SkillState.Combat:
            case Define.SkillState.Counter: return false;
            default: return true;
        };
    }

    /// <summary> ���潺�� �̱�� ���� true ���� ��� false ��ȯ</summary>
    static bool DefenseWinCheck(Skill other)
    {
        switch (other.type)
        {
            case Define.SkillState.Smash: return false;
            default: return true;
        };
    }

    /// <summary> ī���Ͱ� �̱�� ���� true ���� ��� false ��ȯ</summary>
    static bool CounterWinCheck(Skill other)
    {
        return true;
    }
}

/// <summary> ��ų �ϳ�</summary>
public class SkillInfo
{
    /// <summary> ��ų</summary>
    public Skill skill;
    /// <summary> ��ų ��ũ</summary>
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
    /// <summary> ���� ��� ��ų��</summary>
    SkillInfo[] skills;
    /// <summary> ���� ��ų ����</summary>
    static CharacterSkill dogSkill = Resources.Load<CharacterSkill>("Data/CharacterSkill/DogSkill"); 
    static CharacterSkill playerSkill = Resources.Load<CharacterSkill>("Data/CharacterSkill/PlayerSkill");
    /// <summary> ��ų ���� �ϳ� �ε����� ���</summary>
    public SkillInfo this[int index]
    {
        get 
        {
            if (index >= skills.Length || index < 0) return null;  //��ų ������ �Ѿ�� ��� ����ó��

            return skills[index]; 
        }
    }
    /// <summary> ��ų ���� �ϳ� Define.SkillState �����ε�</summary>
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

    /// <summary> ���� ��� ��ų���� SkillInfo�迭���� ������ ����</summary>
    public SkillList(SkillInfo[] value) 
    { 
        skills = value;
    }

    /// <summary> ���� ���� ��ų���� ��ų ����Ʈ�� ����</summary>
    public static SkillList dog = new SkillList(new SkillInfo[]
    {       
        new SkillInfo(Define.SkillState.Combat, dogSkill.CombatRank),
        new SkillInfo(Define.SkillState.Smash, dogSkill.SmashRank),
        new SkillInfo(Define.SkillState.Defense, dogSkill.DefenseRank),
        new SkillInfo(Define.SkillState.Counter, dogSkill.CounterRank),
    });

    public static SkillList player = new SkillList(new SkillInfo[]
{
        new SkillInfo(Define.SkillState.Combat, playerSkill.CombatRank),
        new SkillInfo(Define.SkillState.Smash, playerSkill.SmashRank),
        new SkillInfo(Define.SkillState.Defense, playerSkill.DefenseRank),
        new SkillInfo(Define.SkillState.Counter, playerSkill.CounterRank),
});
    /// <summary> ��ż�� ���� ��ų���� ��ų ����Ʈ�� ����</summary>
    public static SkillList hen = new SkillList(new SkillInfo[]
{
        new SkillInfo(Define.SkillState.Combat, playerSkill.CombatRank),
        new SkillInfo(Define.SkillState.Defense, playerSkill.DefenseRank),
});

    /// <summary> ���밡 ���� ��ų���� ��ų ����Ʈ�� ����</summary>
    public static SkillList wolf = new SkillList(new SkillInfo[]
    {
        new SkillInfo(Define.SkillState.Combat, 4),
        new SkillInfo(Define.SkillState.Smash, 1),
        new SkillInfo(Define.SkillState.Defense, 1),
        new SkillInfo(Define.SkillState.Counter, 1),
    });

}

