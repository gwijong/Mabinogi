using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary> ��ų ���ս� �¸��� üũ ��������Ʈ</summary>
//            ��ȯ��      ��������Ʈ �̸�    �Ű�����
public delegate bool winnerCheckDelegate(Skill other);

/// <summary> ��ų����,�����ð�,�̱�½�ų,����,��������,Ʈ����</summary>
public class Skill
{
    /// <summary> ���տ��� �̱�� Ʈ��</summary>
    public winnerCheckDelegate WinnerCheck;//���տ��� �̱�� Ʈ�� ���� �޽�
    /// <summary> ��ų Ÿ��</summary>
    public Define.SkillState type;
    /// <summary> �ִϸ����� �Ķ���� Trigger �̸�</summary>
    public string AnimName;
    /// <summary> ��ų ���� �ð�</summary>
    public float castingTime;
    /// <summary> �ݵ�� ��ų ���� üũ</summary>
    public bool mustCheck;
    /// <summary>���� ���� �Ұ� ��ų�� ���</summary>
    public bool cannotAttack;

    /// <summary> Skill Ŭ���� ������</summary>
    public Skill(Define.SkillState wantType, string wantAnimName, float wantCastingTime, winnerCheckDelegate wantWinnerCheck, bool wantMustCheck = false, bool wantCannotAttack = false)
    {
        type = wantType; //��ų Ÿ��
        castingTime = wantCastingTime; //��ų ���� �ð�
        WinnerCheck = wantWinnerCheck;  //��ų ���ս� �̱�� ��ų üũ
        mustCheck = wantMustCheck;  //�ݵ�� ��ų ���� üũ
        cannotAttack = wantCannotAttack; //�������ݺҰ� ��ų
        AnimName = wantAnimName; // Trigger �̸�
    }

    /// <summary> Skill Ŭ���� combatMastery ��ü ����</summary>
    public static Skill combatMastery   = new Skill(Define.SkillState.Combat, "Combat", Define.SkillState.Combat.GetSkillData().CastTime, CombatWinCheck);
    /// <summary> Skill Ŭ���� smash ��ü ����</summary>
    public static Skill smash           = new Skill(Define.SkillState.Smash, "Smash", Define.SkillState.Smash.GetSkillData().CastTime, SmashWinCheck);
    /// <summary> Skill Ŭ���� counterAttack ��ü ����</summary>
    public static Skill counterAttack   = new Skill(Define.SkillState.Counter, "Counter", Define.SkillState.Counter.GetSkillData().CastTime, CounterWinCheck, true, true);
    /// <summary> Skill Ŭ���� defense ��ü ����</summary>
    public static Skill defense         = new Skill(Define.SkillState.Defense, "Defense", Define.SkillState.Defense.GetSkillData().CastTime, DefenseWinCheck, true, true);

    /// <summary> ���� ��Ÿ�� �̱�� ���� true ���� ��� false ��ȯ</summary>
    static bool CombatWinCheck(Skill other)
    {
        switch(other.type)
        {
            case Define.SkillState.Defense:  //������ ��ų�� ���潺�� ī������ ��� ���Ƿ� false ��ȯ
            case Define.SkillState.Counter: return false;
            default: return true;
        };
    }

    /// <summary> ���Žð� �̱�� ���� true ���� ��� false ��ȯ</summary>
    static bool SmashWinCheck(Skill other)
    {
        switch (other.type)
        {
            case Define.SkillState.Combat:  //������ ��ų�� �ĺ��̳� ī������ ��� ���Ƿ� false ��ȯ
            case Define.SkillState.Counter: return false;
            default: return true;
        };
    }

    /// <summary> ���潺�� �̱�� ���� true ���� ��� false ��ȯ</summary>
    static bool DefenseWinCheck(Skill other)
    {
        switch (other.type)
        {
            case Define.SkillState.Smash: return false; //������ ��ų�� ���Ž��� ��� ���Ƿ� false ��ȯ
            default: return true;
        };
    }

    /// <summary> ī���Ͱ� �̱�� ���� true ���� ��� false ��ȯ</summary>
    static bool CounterWinCheck(Skill other) //ī���ʹ� ������ �̱��
    {
        return true;
    }
}

/// <summary> ��ų �ϳ� ����(��ų����, ��ų��ũ)</summary>
public class SkillInfo
{
    /// <summary> ��ų</summary>
    public Skill skill;
    /// <summary> ��ų ��ũ</summary>
    public int rank;

    /// <summary> SkillInfo ������</summary>
    public SkillInfo(Define.SkillState wantType, int wantRank) 
    {
        skill = wantType.GetSkill(); //���ϴ� ��ų�� Skill Ŭ���� ��ü ������
        rank = wantRank; //��ų ��ũ�� ������ ����̳� ��ų �����ð� ���� ��꿡 ����
    }
}


/// <summary> ���� ��� ��ų</summary>
public class SkillList
{
    /// <summary> ���� ��� ��ų��</summary>
    SkillInfo[] skills;
    /// <summary> ���� ��ų ����</summary>
    static CharacterSkill dogSkill = Resources.Load<CharacterSkill>("Data/CharacterSkill/DogSkill");
    /// <summary> �÷��̾��� ��ų ����</summary>
    static CharacterSkill playerSkill = Resources.Load<CharacterSkill>("Data/CharacterSkill/PlayerSkill");
    /// <summary> ��ż�� ��ų ����</summary>
    static CharacterSkill henSkill = Resources.Load<CharacterSkill>("Data/CharacterSkill/HenSkill");
    /// <summary> ��ż�� ��ų ����</summary>
    static CharacterSkill roosterSkill = Resources.Load<CharacterSkill>("Data/CharacterSkill/RoosterSkill");
    /// <summary> ���� ��ų ����</summary>
    static CharacterSkill foxrSkill = Resources.Load<CharacterSkill>("Data/CharacterSkill/FoxSkill");
    /// <summary> ���� ��ų ����</summary>
    static CharacterSkill sheepSkill = Resources.Load<CharacterSkill>("Data/CharacterSkill/SheepSkill");
    /// <summary> ������ ��ų ����</summary>
    static CharacterSkill wolfSkill = Resources.Load<CharacterSkill>("Data/CharacterSkill/WolfSkill");

    //    ������Ƽ �̸��� ���� �̸��̴� �׷��� �׳� �迭ó�� �ش� SkillInfo�� ���ȣ[] �ٿ��� ����Ѵ�
    /// <summary> ��ų ���� �ϳ� �ε����� ���</summary>
    public SkillInfo this[int index]
    {
        get 
        {
            if (index >= skills.Length || index < 0) return null;  //��ų ������ �Ѿ�� ��� ����ó��

            return skills[index]; 
        }
    }
    //    ������Ƽ �̸��� ���� �̸��̴� �׷��� �׳� �迭ó�� �ش� SkillInfo�� ���ȣ[] �ٿ��� ����Ѵ�
    /// <summary> ��ų ���� �ϳ� Define.SkillState �����ε�</summary>
    public SkillInfo this[Define.SkillState type]
    {
        get
        {
            foreach(SkillInfo current in skills)
            {
                if (current.skill.type == type) return current;
            };

            return null; //���� ��ųŸ�� �� ã���� null ��ȯ
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

    /// <summary> �÷��̾ ���� ��ų���� ��ų ����Ʈ�� ����</summary>
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
        new SkillInfo(Define.SkillState.Combat, henSkill.CombatRank),
        new SkillInfo(Define.SkillState.Defense, henSkill.DefenseRank),
    });
    /// <summary> ��ż�� ���� ��ų���� ��ų ����Ʈ�� ����</summary>
    public static SkillList rooster = new SkillList(new SkillInfo[]
    {
        new SkillInfo(Define.SkillState.Combat, roosterSkill.CombatRank),
        new SkillInfo(Define.SkillState.Defense, roosterSkill.DefenseRank),
    });

    /// <summary> ���찡 ���� ��ų���� ��ų ����Ʈ�� ����</summary>
    public static SkillList fox = new SkillList(new SkillInfo[]
    {
        new SkillInfo(Define.SkillState.Combat, foxrSkill.CombatRank),
        new SkillInfo(Define.SkillState.Defense, foxrSkill.DefenseRank),
    });
    /// <summary> ���� ���� ��ų���� ��ų ����Ʈ�� ����</summary>
    /// 
    public static SkillList sheep = new SkillList(new SkillInfo[]
    {
        new SkillInfo(Define.SkillState.Combat, sheepSkill.CombatRank),
        new SkillInfo(Define.SkillState.Defense, sheepSkill.DefenseRank),
    });
    /// <summary> ���밡 ���� ��ų���� ��ų ����Ʈ�� ����</summary>
    public static SkillList wolf = new SkillList(new SkillInfo[]
    {
        new SkillInfo(Define.SkillState.Combat, wolfSkill.CombatRank),
        new SkillInfo(Define.SkillState.Smash, wolfSkill.SmashRank),
        new SkillInfo(Define.SkillState.Defense, wolfSkill.DefenseRank),
        new SkillInfo(Define.SkillState.Counter, wolfSkill.CounterRank),
    });

}

