using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    //wantType.GetSkill(); �ϸ� �ش��ϴ� ��ų�� ������
    public static Skill GetSkill(this Define.SkillState from)
    {
        switch(from)
        {
            case Define.SkillState.Counter: return Skill.counterAttack;
            case Define.SkillState.Defense: return Skill.defense;
            case Define.SkillState.Smash:   return Skill.smash;
            default:                        return Skill.combatMastery;
        }
    }
}
