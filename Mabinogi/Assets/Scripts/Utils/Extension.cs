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
            case Define.SkillState.Counter: return Skill.counterAttack; //ī����
            case Define.SkillState.Defense: return Skill.defense; //���潺
            case Define.SkillState.Smash:   return Skill.smash;  //���Ž�
            default:                        return Skill.combatMastery; //���� ����
        }
    }

    /// <summary>Define.Item.GetSize(); �ϸ� �ش� ������ ������ ��ȯ </summary>
    public static Vector2Int GetSize(this Define.Item item)
    {
        switch (item)
        {          
            case Define.Item.Fruit: return new Vector2Int(1, 1); //�������Ŵ� 1*1�̴�
            case Define.Item.Wool: return new Vector2Int(2, 2);  //������ 2*2�̴�
            case Define.Item.Egg: return new Vector2Int(1, 1);  //�ް��� 1*1�̴�
            case Define.Item.Firewood: return new Vector2Int(1, 3);  //�������� 1*3�̴�
            case Define.Item.LifePotion: return new Vector2Int(1, 1);  //����¹����� 1*1�̴�
            case Define.Item.ManaPotion: return new Vector2Int(1, 1);  //���������� 1*1�̴�
            case Define.Item.Bottle: return new Vector2Int(1, 2);  //���� 1*2�̴�
            case Define.Item.BottleWater: return new Vector2Int(1, 2);  //������ 1*2�̴�
            default : return Vector2Int.one;
        }
    }

    /// <summary>Define.Item.GetItemImage(); �ϸ� �ش� ������ ��������Ʈ ��ȯ </summary>
    public static Sprite GetItemImage(this Define.Item item)
    {
        switch (item)
        {
            case Define.Item.Fruit: return Resources.Load<Sprite>("UI/Inventory/Fruit"); //��������
            case Define.Item.Wool: return Resources.Load<Sprite>("UI/Inventory/Wool");  //����
            case Define.Item.Egg: return Resources.Load<Sprite>("UI/Inventory/Egg");  //�ް�
            case Define.Item.Firewood: return Resources.Load<Sprite>("UI/Inventory/FireWood");  //��������
            case Define.Item.LifePotion: return Resources.Load<Sprite>("UI/Inventory/LifePotion");  //����¹���
            case Define.Item.ManaPotion: return Resources.Load<Sprite>("UI/Inventory/ManaPotion");  //��������
            case Define.Item.Bottle: return Resources.Load<Sprite>("UI/Inventory/Bottle");  //��
            case Define.Item.BottleWater: return Resources.Load<Sprite>("UI/Inventory/BottleWater");  //����
            default: return null;
        }
    }

    /// <summary>Define.Item.GetMaxStack(); �ϸ� �ش� ������ �ִ� ��ĥ �� �ִ� �� ��ȯ </summary>
    public static int GetMaxStack(this Define.Item item)
    {
        switch (item)
        {
            case Define.Item.Fruit: return 5; //�������Ŵ� 5�� ��ĥ �� �ִ�
            case Define.Item.Wool: return 5;  //������ 5�� ��ĥ �� �ִ�
            case Define.Item.Egg: return 5;  //�ް��� 5�� ��ĥ �� �ִ�
            case Define.Item.Firewood: return 5;  //���������� 5�� ��ĥ �� �ִ�
            case Define.Item.LifePotion: return 10;  //����� ������ 10�� ��ĥ �� �ִ�
            case Define.Item.ManaPotion: return 10;  //���� ������ 10�� ��ĥ �� �ִ�
            case Define.Item.Bottle: return 1;  //���� ��ĥ �� ����
            case Define.Item.BottleWater: return 1;  //������ ��ĥ �� ����
            default: return 1;
        }
    }
}
