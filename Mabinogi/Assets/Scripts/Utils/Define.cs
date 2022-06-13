/// <summary> ���� ������</summary>
public class Define
{
    /// <summary> ���콺 �Է°�</summary>
    public enum mouseKey
    {
        LeftClick,
        rightClick,
        middleClick
    }
    /// <summary> ��ȣ�ۿ� Ÿ��</summary>
    public enum InteractType
    {
        None,
        Talk,
        Attack,
        Get,
        Sheeping,
        Egg,
    }
    /// <summary> �̵� Ÿ��</summary>
    public enum MoveType
    {
        Move,
        DropItem,
    }
    /// <summary> ĳ������ ����</summary>
    public enum State
    {
        Die,
        Moving,
        Idle,
        Casting,
    }
    /// <summary> ĳ������ ��ų ����</summary>
    public enum SkillState
    {
        Combat = 0,
        Defense = 1,
        Smash = 2,
        Counter = 3,
        Windmill = 4,
        Icebolt = 5,
    }

    /// <summary> ���̾� ����</summary>
    public enum Layer
    {
        Enemy = 6,
        Livestock = 7,
        Ground = 8,
        Block = 9,
        Tree = 10,
        Item = 11,
        NPC = 12,
        Player = 13,
    }

    /// <summary> �� ����</summary>
    public enum Scene
    {
        None,
        Soulstream,
        Tutorial,
        Intro,
        World,
        Dungeon,
        Boss,
        Die,
    }

    /// <summary> �̵� ���� </summary>
    public enum MoveState
    {  //�ӵ��� �ٸ��� ����
       //�Ȱ� �ٴ°� Character ���� ����
        Rooted,
        Walkable,
        Runnable,
    }
    
    /// <summary> ������ ���� </summary>
    public enum Item
    {
        None,
        Fruit,
        Bottle,
        BottleWater,
        Egg,
        LifePotion,
        ManaPotion,
        Wool,
        Firewood,
    }
    /// <summary> NPC��</summary>
    public enum NPC
    {
        None,
        Nao,
        Goro,
        Tarlach,
        Tin,
    }
    /// <summary> ȿ���� </summary>
    public enum SoundEffect 
    {
        punch_hit,
        punch_blow,
        guard,
        emotion_success,
        emotion_fail,
        eatfood,
        down,
        drinkpotion,
        dungeon_monster_appear1,
        skill_cancel,
        skill_standby,
        skill_ready,
        inventory_open,
        inventory_close,
        gen_button_down,
        character_levelup,
        dungeon_door,
        magic_standby,
        magic_ready,
        magic_lightning,
        dog01_natural_stand_offensive,
        dog01_natural_blowaway,
        dog01_natural_hit,
        wolf01_natural_stand_offensive,
        wolf01_natural_attack_counter,
        wolf01_natural_attack_smash,
        wolf01_natural_down,
        wolf01_natural_hit,
        sheep,
        chicken_fly,
        chicken_hit,
        chicken_down,
        bear01_natural_attack_smash,
        bear01_natural_hit,
        bear01_natural_blowaway,
        bear01_natural_attack_counter,
        bear01_natural_stand_offensive,
        golem01_woo,
        golem01_walk,
        golem01_downb_to_stand,
        golem01_hit,
        golem01_blowaway_ground,
        item_get,
        none,
    }

    /// <summary> NPC ��ȭ ��ư ���� </summary>
    public enum TalkButtonType
    {
        ToMain,
        EndTalk,
        Next,
        Shop,
        Note,
        Farewell,
    }
}
