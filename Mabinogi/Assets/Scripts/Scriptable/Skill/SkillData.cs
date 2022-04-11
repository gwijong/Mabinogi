using UnityEngine;

//[CreateAssetMenu(menuName = "�޴� ���",fileName = "�⺻ ���ϸ�"), order = �޴��󿡼� ����]
[CreateAssetMenu(menuName = "Scriptable/Skill/SkillData", fileName = "SkillData")]
public class SkillData : ScriptableObject
{
    [SerializeField]
    private int rank = 1;
    /// <summary> ��ų ��ũ </summary>
    public int Rank { get { return rank; } }

    [SerializeField]
    private float coefficient = 1.0f;
    /// <summary> ��ų ��� </summary>
    public float Coefficient { get { return coefficient * Rank; } }

    [SerializeField]
    private float castTime = 1.0f;
    /// <summary> ��ų ���� �ð� </summary>
    public float CastTime { get { return castTime; } }

    [SerializeField]
    private int castCost = 3;
    /// <summary> ��ų ���� �� ���� ��� </summary>
    public float CastCost { get { return castCost; } }


}
