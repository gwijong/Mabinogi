using UnityEngine;

//[CreateAssetMenu(menuName = "�޴� ���",fileName = "�⺻ ���ϸ�"), order = �޴��󿡼� ����]
[CreateAssetMenu(menuName = "Scriptable/Skill/SkillData", fileName = "SkillData")]
public class SkillData : ScriptableObject
{
    [Tooltip("��ų ID")]
    [SerializeField]
    [Range(0, 100)]
    private Define.SkillState skillId = 0;
    /// <summary> ��ų ID </summary>
    public Define.SkillState SkillId { get { return skillId; } }

    [SerializeField, Tooltip("��ų ��ũ"), Range(1, 15)] int rank = 1;
    /// <summary> ��ų ��ũ </summary>
    public int Rank { get { return rank; } }

    [Tooltip("��ų ���")]
    [SerializeField]
    [Range(1.0f, 100.0f)]
    private float coefficient = 1.0f;
    /// <summary> ��ų ��� </summary>
    public float Coefficient { get { return coefficient * Rank; } }

    [Tooltip("��ų ���� �ð�")]
    [SerializeField]
    [Range(0.0f, 100.0f)]
    private float castTime = 1.0f;
    /// <summary> ��ų ���� �ð� </summary>
    public float CastTime { get { return castTime; } }

    [Tooltip("��ų ���� �� ���� ���")]
    [SerializeField]
    [Range(0, 100)]
    private int castCost = 3;
    /// <summary> ��ų ���� �� ���� ��� </summary>
    public int CastCost { get { return castCost; } }

    [Tooltip("���� �ð�")]
    [SerializeField]
    [Range(0.0f, 100.0f)]
    private float stiffnessTime = 1.0f;
    /// <summary> ���� �ð� </summary>
    public float StiffnessTime { get { return stiffnessTime; } }

    [Tooltip("�ٿ� ������")]
    [SerializeField]
    [Range(0, 100)]
    private int downGauge = 100;
    /// <summary> �ٿ� ������ </summary>
    public int DownGauge { get { return downGauge; } }

}
