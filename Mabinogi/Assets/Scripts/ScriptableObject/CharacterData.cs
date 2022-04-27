using UnityEngine;

//[CreateAssetMenu(menuName = "�޴� ���",fileName = "�⺻ ���ϸ�"), order = �޴��󿡼� ����]
[CreateAssetMenu(menuName = "Scriptable/Character/Playerdata", fileName = "PlayerData")]
public class CharacterData : ScriptableObject
{
    [Tooltip("�����. �ǰݽ� ����")]
    [SerializeField]
    [Range(10, 1000)]
    private int hitPoint = 100;
    /// <summary> �����. �ǰݽ� ���� </summary>
    public int HitPoint { get { return hitPoint; } }

    [Tooltip("����. ���� ������ ����")]
    [SerializeField]
    [Range(10, 1000)]
    private int manaPoint = 100;
    /// <summary> ����. ���� ������ ���� </summary>
    public int ManaPoint { get { return manaPoint; } }

    [Tooltip("���¹̳�. ��ų ������ ����")]
    [SerializeField]
    [Range(10, 1000)]
    private int staminaPoint = 100;
    /// <summary> ���¹̳�. ��ų ������ ���� </summary>
    public int StaminaPoint { get { return staminaPoint; } }

    [Tooltip("ü��. �������ݷ¿� ������ ��")]
    [SerializeField]
    [Range(10, 1000)]
    private int strength = 10;
    /// <summary> ü��. ���� ���ݷ¿� ������ �� </summary>
    public int Strength { get { return strength; } }

    [Tooltip("����. �������ݷ¿� ������ ��")]
    [SerializeField]
    [Range(10, 1000)]
    private int intelligence = 10;
    /// <summary> ����. �������ݷ¿� ������ �� </summary>
    public int Intelligence { get { return intelligence; } }

    [Tooltip("�ؾ�. �뷱���� ������ ��")]
    [SerializeField]
    [Range(10, 1000)]
    private int dexterity = 10;
    /// <summary> �ؾ�. �뷱���� ������ �� </summary>
    public int Dexterity { get { return dexterity; } }

    [Tooltip("����. ������ �̰ܳ��� ���鸮 ���°� �� Ȯ���� �����Ѵ�")]
    [SerializeField]
    [Range(10, 1000)]
    private int will = 10;
    /// <summary> ����. ������ �̰ܳ��� ���鸮 ���°� �� Ȯ���� �����Ѵ� </summary>
    public int Will { get { return will; } }

    [Tooltip("���. ġ��Ÿ Ȯ���� ������ ��")]
    [SerializeField]
    [Range(10, 1000)]
    private int luck = 10;
    /// <summary> ���. ġ��Ÿ Ȯ���� ������ �� </summary>
    public int Luck { get { return luck; } }

    [Tooltip("�ִ빰�����ݷ�")]
    [SerializeField]
    [Range(1, 1000)]
    private int maxPhysicalStrikingPower = 10;
    /// <summary> �ִ빰�����ݷ� </summary>
    public int MaxPhysicalStrikingPower { get { return maxPhysicalStrikingPower + Strength; } }

    [Tooltip("�ִ븶�����ݷ�")]
    [SerializeField]
    [Range(1, 1000)]
    private int maxMagicStrikingPower = 10;
    /// <summary> �ִ븶�����ݷ� </summary>
    public int MaxMagicStrikingPower { get { return maxMagicStrikingPower + Intelligence; } }

    [Tooltip("�ּҹ������ݷ�")]
    [SerializeField]
    [Range(1, 1000)]
    private int minPhysicalStrikingPower = 1;
    /// <summary> �ּҹ������ݷ� </summary>
    public int MinPhysicalStrikingPower { get { return minPhysicalStrikingPower + Strength / 2; } }

    [Tooltip("�ּҸ������ݷ�")]
    [SerializeField]
    [Range(1, 1000)]
    private int minMagicStrikingPower = 1;
    /// <summary> �ּҸ������ݷ� </summary>
    public int MinMagicStrikingPower { get { return minMagicStrikingPower + Intelligence / 2; } }

    [Tooltip("ĳ���Ͱ� ���� �λ�")]
    [SerializeField]
    [Range(0, 1000)]
    private int wound = 0;
    /// <summary> ĳ���Ͱ� ���� �λ� </summary>
    public int Wound { get { return wound; } }

    [Tooltip("���ݽ� ���濡�� ������ �λ��")]
    [SerializeField]
    [Range(0, 1000)]
    private int woundAttack = 1;
    /// <summary> ���ݽ� ���濡�� ������ �λ�� </summary>
    public int WoundAttack { get { return woundAttack; } }

    [Tooltip("ġ��Ÿ Ȯ��")]
    [SerializeField]
    [Range(0.0f, 0.8f)]
    private float critical = 0.2f;
    /// <summary> ġ��Ÿ Ȯ�� </summary>
    public float Critical { get { return critical + (float)Luck/100; } }

    [Tooltip("�뷱��, �ּ�, �ִ� �������� �ߴ� ����")]
    [SerializeField]
    [Range(0.2f, 0.8f)]
    private float balance = 0.2f;
    /// <summary> �뷱��, �ּ�, �ִ� �������� �ߴ� ���� </summary>
    public float Balance { get { return balance + (float)Dexterity/100; } }

    [Tooltip("���� ����. 1��1 ������ �������� ������ ����")]
    [SerializeField]
    [Range(0, 1000)]
    private int physicalDefensivePower = 3;
    /// <summary> ���� ����. 1��1 ������ �������� ���ط� ���� </summary>
    public int PhysicalDefensivePower { get { return physicalDefensivePower; } }

    [Tooltip("���� ����. 1��1 ������ �������� ���ط� ����")]
    [SerializeField]
    [Range(0, 1000)]
    private int magicDefensivePower = 3;
    /// <summary> ���� ����. 1��1 ������ �������� ���ط� ���� </summary>
    public int MagicDefensivePower { get { return magicDefensivePower; } }

    [Tooltip("���� ��ȣ. 1�ۼ�Ʈ ������ ������ ���� ")]
    [SerializeField]
    [Range(0, 100)]
    private int physicalProtective = 1;
    /// <summary> ���� ��ȣ. 1�ۼ�Ʈ ������ ������ ���� </summary>
    public int PhysicalProtective { get { return physicalProtective; } }

    [Tooltip("���� ��ȣ. 1�ۼ�Ʈ ������ ������ ����")]
    [SerializeField]
    [Range(0, 100)]
    private int magicProtective = 1;
    /// <summary> ���� ��ȣ. 1�ۼ�Ʈ ������ ������ ���� </summary>
    public int MagicProtective { get { return magicProtective; } }

    [Tooltip("����� ������ ������ �� �̰ܳ��� ���鸮 ���°� �� Ȯ��")]
    [SerializeField]
    [Range(0, 100)]
    private int deadly = 1;
    /// <summary> ����� ������ ������ �� �̰ܳ��� ���鸮 ���°� �� Ȯ�� </summary>
    public int Deadly { get { return deadly + will/5; } }

    [Tooltip("�̵� �ӵ�")]
    [SerializeField]
    [Range(0, 100)]
    private int speed = 1;
    /// <summary> �̵� �ӵ� </summary>
    public int Speed { get { return speed; } }
}
