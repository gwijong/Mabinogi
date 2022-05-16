using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    /// <summary>target�� compare�� �Ȱ��ų� ����Ŭ�������� Ȯ��</summary>
    public static bool IsClassOf(this System.Type target, System.Type compare)
    {
        return target == compare || target.IsSubclassOf(compare);
    }

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

    /// <summary>Define.SkillData.GetSkillData(); �ϸ� �ش� ��ų ������ ��ȯ</summary>
    public static SkillData GetSkillData(this Define.SkillState skill)
    {
        switch (skill)
        {
            case Define.SkillState.Combat: return Resources.Load<SkillData>("Data/SkillData/Combat"); //�⺻ �������� ��ų ������ ��ȯ
            case Define.SkillState.Defense: return Resources.Load<SkillData>("Data/SkillData/Defense");  //���潺 ��ų ������ ��ȯ
            case Define.SkillState.Smash: return Resources.Load<SkillData>("Data/SkillData/Smash");  //���Ž� ��ų ������ ��ȯ
            case Define.SkillState.Counter: return Resources.Load<SkillData>("Data/SkillData/CounterAttack");  //ī���� ��ų ������ ��ȯ      
            default: return Resources.Load<SkillData>("Data/SkillData/Combat"); //�̻��� �� ������ �⺻ �������� ��ų ������ ��ȯ
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

    /// <summary>Define.Item.MakePrefab(); ������ ������ ������ ������ ���ӿ�����Ʈ ��ȯ </summary>
    public static GameObject MakePrefab(this Define.Item item)
    {
        switch (item)
        {
            case Define.Item.None: //���� ������ Ÿ���� none�̸� ���� �������� �����Ƿ� Ż��
                return null;
            case Define.Item.Fruit:
                return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Item/Fruit")); //���� ������ ����
            case Define.Item.Wool:
                return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Item/Wool")); //���� ������ ����
            case Define.Item.Egg:
                return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Item/Egg")); //�ް� ������ ����
            case Define.Item.Firewood:
                return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Item/Firewood")); //���� ������ ����
            case Define.Item.LifePotion:
                return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Item/LifePotion")); //����� ���� ������ ����
            case Define.Item.ManaPotion:
                return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Item/ManaPotion")); //���� ���� ������ ����
            case Define.Item.Bottle:
                return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Item/Bottle")); //�� ������ ����
            case Define.Item.BottleWater:
                return GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Item/BottleWater")); //���� ������ ����
            default: return null;
        };
    }

    /// <summary>Define.Item.GetItemName(); ������ �̸� ��Ʈ�� ��ȯ </summary>
    public static string GetItemName(this Define.Item item)
    {
        switch (item)
        {
            case Define.Item.Fruit: return Resources.Load<ItemData>("Data/ItemData/Fruit").ItemName; //�������� �̸�
            case Define.Item.Wool: return Resources.Load<ItemData>("Data/ItemData/Wool").ItemName;  //���� �̸�
            case Define.Item.Egg: return Resources.Load<ItemData>("Data/ItemData/Egg").ItemName;  //�ް� �̸�
            case Define.Item.Firewood: return Resources.Load<ItemData>("Data/ItemData/Firewood").ItemName;  //�������� �̸�
            case Define.Item.LifePotion: return Resources.Load<ItemData>("Data/ItemData/LifePotion").ItemName;  //����� ���� �̸�
            case Define.Item.ManaPotion: return Resources.Load<ItemData>("Data/ItemData/ManaPotion").ItemName;  //���� ���� �̸�
            case Define.Item.Bottle: return Resources.Load<ItemData>("Data/ItemData/Bottle").ItemName;  //�� �̸�
            case Define.Item.BottleWater: return Resources.Load<ItemData>("Data/ItemData/BottleWater").ItemName;  //���� �̸�
            default: return ""; //�� �� �������� "" ��Ʈ�� ��ȯ
        }
    }

    /// <summary>Define.Item.GetItemData(); ������ ��ũ���ͺ� ������Ʈ ������ ��ȯ </summary>
    public static ItemData GetItemData(this Define.Item item)
    {
        switch (item)
        {
            case Define.Item.Fruit: return Resources.Load<ItemData>("Data/ItemData/Fruit"); //�������� ��ũ���ͺ� ������Ʈ
            case Define.Item.Wool: return Resources.Load<ItemData>("Data/ItemData/Wool");  //���� ��ũ���ͺ� ������Ʈ
            case Define.Item.Egg: return Resources.Load<ItemData>("Data/ItemData/Egg");  //�ް� ��ũ���ͺ� ������Ʈ
            case Define.Item.Firewood: return Resources.Load<ItemData>("Data/ItemData/Firewood");  //�������� ��ũ���ͺ� ������Ʈ
            case Define.Item.LifePotion: return Resources.Load<ItemData>("Data/ItemData/LifePotion");  //����� ���� ��ũ���ͺ� ������Ʈ
            case Define.Item.ManaPotion: return Resources.Load<ItemData>("Data/ItemData/ManaPotion");  //���� ���� ��ũ���ͺ� ������Ʈ
            case Define.Item.Bottle: return Resources.Load<ItemData>("Data/ItemData/Bottle");  //�� ��ũ���ͺ� ������Ʈ
            case Define.Item.BottleWater: return Resources.Load<ItemData>("Data/ItemData/BottleWater");  //���� ��ũ���ͺ� ������Ʈ
            default: return null;
        }
    }
}
