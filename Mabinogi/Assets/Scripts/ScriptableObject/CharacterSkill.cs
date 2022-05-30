using UnityEngine;

//[CreateAssetMenu(menuName = "�޴� ���",fileName = "�⺻ ���ϸ�"), order = �޴��󿡼� ����]
[CreateAssetMenu(menuName = "Scriptable/Skill/CharacterSkill", fileName = "CharacterSkill")]
public class CharacterSkill : ScriptableObject
{
    [SerializeField, Tooltip("�ĺ� �����͸� ��ų ��ũ"), Range(0, 15)] 
    int combatRank = 1;
    /// <summary> �ĺ� �����͸� ��ų ��ũ </summary>
    public int CombatRank { get { return combatRank; } }

    [SerializeField, Tooltip("���潺 ��ų ��ũ"), Range(0, 15)] 
    int defenseRank = 1;
    /// <summary> ���潺 ��ų ��ũ </summary>
    public int DefenseRank { get { return defenseRank; } }

    [SerializeField, Tooltip("���Ž� ��ų ��ũ"), Range(0, 15)] 
    int smashRank = 1;
    /// <summary> ���Ž� ��ų ��ũ </summary>
    public int SmashRank { get { return smashRank; } }

    [SerializeField, Tooltip("ī���� ���� ��ų ��ũ"), Range(0, 15)] 
    int counterRank = 1;
    /// <summary> ī���� ���� ��ų ��ũ </summary>
    public int CounterRank { get { return counterRank; } }

    [SerializeField, Tooltip("����� ��ų ��ũ"), Range(0, 15)]
    int windmillRank = 1;
    /// <summary> ����� ��ų ��ũ </summary>
    public int WindmillRank { get { return windmillRank; } }

    [SerializeField, Tooltip("���̽���Ʈ ��ų ��ũ"), Range(0, 15)]
    int iceboltRank = 1;
    /// <summary> ���̽���Ʈ ��ų ��ũ </summary>
    public int IceboltRank { get { return iceboltRank; } }

}
