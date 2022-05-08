using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    /// <summary>wantType.GetSkill(); �ϸ� �ش��ϴ� ��ų�� ������</summary>
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

    /// <summary>Define.Item.GetSize(); �ϸ� �ش� ������ ������ ��ȯ </summary>
    public static Vector2Int GetSize(this Define.Item item)
    {
        switch (item)
        {          
            case Define.Item.Fruit: return new Vector2Int(1, 1); //�������Ŵ� 1*1�̴�
            case Define.Item.Wool: return new Vector2Int(2, 2);  //������ 2*2�̴�

            default : return Vector2Int.one;
        }
    }

    public static Sprite GetItemImage(this Define.Item item)
    {
        switch (item)
        {
            case Define.Item.Fruit: return Resources.Load<Sprite>("UI/Inventory/Fruit"); //�������Ŵ� 1*1�̴�
            case Define.Item.Wool: return Resources.Load<Sprite>("UI/Inventory/Wool");  //������ 2*2�̴�

            default: return null;
        }
    }

    public static int GetMaxStack(this Define.Item item)
    {
        switch (item)
        {
            case Define.Item.Fruit: return 1; //�������Ŵ� 1*1�̴�
            case Define.Item.Wool: return 5;  //������ 2*2�̴�

            default: return 1;
        }
    }
}
