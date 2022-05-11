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
            case Define.Item.Fruit: return new Vector2Int(Resources.Load<ItemData>("Data/ItemData/Fruit").Width, Resources.Load<ItemData>("Data/ItemData/Fruit").Height); //�������Ŵ� 1*1�̴�
            case Define.Item.Wool: return new Vector2Int(Resources.Load<ItemData>("Data/ItemData/Wool").Width, Resources.Load<ItemData>("Data/ItemData/Wool").Height);  //������ 2*2�̴�
            case Define.Item.Egg: return new Vector2Int(Resources.Load<ItemData>("Data/ItemData/Egg").Width, Resources.Load<ItemData>("Data/ItemData/Egg").Height);  //�ް��� 1*1�̴�
            case Define.Item.Firewood: return new Vector2Int(Resources.Load<ItemData>("Data/ItemData/Firewood").Width, Resources.Load<ItemData>("Data/ItemData/Firewood").Height);  //�������� 1*3�̴�
            case Define.Item.LifePotion: return new Vector2Int(Resources.Load<ItemData>("Data/ItemData/LifePotion").Width, Resources.Load<ItemData>("Data/ItemData/LifePotion").Height);  //����¹����� 1*1�̴�
            case Define.Item.ManaPotion: return new Vector2Int(Resources.Load<ItemData>("Data/ItemData/ManaPotion").Width, Resources.Load<ItemData>("Data/ItemData/ManaPotion").Height);  //���������� 1*1�̴�
            case Define.Item.Bottle: return new Vector2Int(Resources.Load<ItemData>("Data/ItemData/Bottle").Width, Resources.Load<ItemData>("Data/ItemData/Bottle").Height);  //���� 1*2�̴�
            case Define.Item.BottleWater: return new Vector2Int(Resources.Load<ItemData>("Data/ItemData/BottleWater").Width, Resources.Load<ItemData>("Data/ItemData/BottleWater").Height);  //������ 1*2�̴�
            default : return Vector2Int.one;
        }
    }

    /// <summary>Define.Item.GetItemImage(); �ϸ� �ش� ������ ��������Ʈ ��ȯ </summary>
    public static Sprite GetItemImage(this Define.Item item)
    {
        switch (item)
        {
            case Define.Item.Fruit: return Resources.Load<ItemData>("Data/ItemData/Fruit").ItemSprite; //��������
            case Define.Item.Wool: return Resources.Load<ItemData>("Data/ItemData/Wool").ItemSprite;  //����
            case Define.Item.Egg: return Resources.Load<ItemData>("Data/ItemData/Egg").ItemSprite;  //�ް�
            case Define.Item.Firewood: return Resources.Load<ItemData>("Data/ItemData/FireWood").ItemSprite;  //��������
            case Define.Item.LifePotion: return Resources.Load<ItemData>("Data/ItemData/LifePotion").ItemSprite;  //����¹���
            case Define.Item.ManaPotion: return Resources.Load<ItemData>("Data/ItemData/ManaPotion").ItemSprite;  //��������
            case Define.Item.Bottle: return Resources.Load<ItemData>("Data/ItemData/Bottle").ItemSprite;  //��
            case Define.Item.BottleWater: return Resources.Load<ItemData>("Data/ItemData/BottleWater").ItemSprite;  //����
            default: return null;
        }
    }

    /// <summary>Define.Item.GetMaxStack(); �ϸ� �ش� ������ �ִ� ��ĥ �� �ִ� �� ��ȯ </summary>
    public static int GetMaxStack(this Define.Item item)
    {
        switch (item)
        {
            case Define.Item.Fruit: return Resources.Load<ItemData>("Data/ItemData/Fruit").Stack; //�������Ŵ� 5�� ��ĥ �� �ִ�
            case Define.Item.Wool: return Resources.Load<ItemData>("Data/ItemData/Wool").Stack;  //������ 5�� ��ĥ �� �ִ�
            case Define.Item.Egg: return Resources.Load<ItemData>("Data/ItemData/Egg").Stack;  //�ް��� 5�� ��ĥ �� �ִ�
            case Define.Item.Firewood: return Resources.Load<ItemData>("Data/ItemData/Firewood").Stack;  //���������� 5�� ��ĥ �� �ִ�
            case Define.Item.LifePotion: return Resources.Load<ItemData>("Data/ItemData/LifePotion").Stack;  //����� ������ 10�� ��ĥ �� �ִ�
            case Define.Item.ManaPotion: return Resources.Load<ItemData>("Data/ItemData/ManaPotion").Stack;  //���� ������ 10�� ��ĥ �� �ִ�
            case Define.Item.Bottle: return Resources.Load<ItemData>("Data/ItemData/Bottle").Stack;  //���� ��ĥ �� ����
            case Define.Item.BottleWater: return Resources.Load<ItemData>("Data/ItemData/BottleWater").Stack;  //������ ��ĥ �� ����
            default: return 1;
        }
    }
}
