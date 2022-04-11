using UnityEngine;

//[CreateAssetMenu(menuName = "�޴� ���",fileName = "�⺻ ���ϸ�"), order = �޴��󿡼� ����]
[CreateAssetMenu(menuName = "Scriptable/Character/Playerdata", fileName = "PlayerData")]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    private int hitPoint = 100;
    /// <summary> �����. �ǰݽ� ���� </summary>
    public int HitPoint { get { return hitPoint; } }

    [SerializeField]
    private int manaPoint = 100;
    /// <summary> ����. ���� ������ ���� </summary>
    public int ManaPoint { get { return manaPoint; } }

    [SerializeField]
    private int staminaPoint = 100;
    /// <summary> ���¹̳�. ��ų ������ ���� </summary>
    public int StaminaPoint { get { return staminaPoint; } }

    [SerializeField]
    private int strength = 10;
    /// <summary> ü�� </summary>
    public int Strength { get { return strength; } }

    [SerializeField]
    private int intelligence = 10;
    /// <summary> ���� </summary>
    public int Intelligence { get { return intelligence; } }

    [SerializeField]
    private int dexterity = 10;
    /// <summary> �ؾ� </summary>
    public int Dexterity { get { return dexterity; } }

    [SerializeField]
    private int will = 10;
    /// <summary> ���� </summary>
    public int Will { get { return will; } }

    [SerializeField]
    private int luck = 10;
    /// <summary> ��� </summary>
    public int Luck { get { return luck; } }

    [SerializeField]
    private int physicalStrikingPower = 10;
    /// <summary> �������ݷ� </summary>
    public int PhysicalStrikingPower { get { return physicalStrikingPower + Strength; } }

    [SerializeField]
    private int magicStrikingPower = 10;
    /// <summary> �������ݷ� </summary>
    public int MagicStrikingPower { get { return physicalStrikingPower + Intelligence; } }

    [SerializeField]
    private int wound = 0;
    /// <summary> �λ� </summary>
    public int Wound { get { return wound; } }

    [SerializeField]
    private float critical = 0.2f;
    /// <summary> ġ��Ÿ Ȯ�� </summary>
    public int Critical { get { return physicalStrikingPower + Luck; } }

    [SerializeField]
    private float balance = 0.2f;
    /// <summary> �뷱��, �ּ�, �ִ� �������� �ߴ� ���� </summary>
    public int Balance { get { return physicalStrikingPower + Dexterity; } }

    [SerializeField]
    private int physicalDefensivePower = 3;
    /// <summary> ���� ���� </summary>
    public int PhysicalDefensivePower { get { return physicalDefensivePower; } }

    [SerializeField]
    private int magicDefensivePower = 3;
    /// <summary> ���� ���� </summary>
    public int MagicDefensivePower { get { return magicDefensivePower; } }

    [SerializeField]
    private int physicalProtective = 1;
    /// <summary> ���� ��ȣ </summary>
    public int PhysicalProtective { get { return physicalProtective; } }

    [SerializeField]
    private int magicProtective = 1;
    /// <summary> ���� ��ȣ </summary>
    public int MagicProtective { get { return magicProtective; } }
}
