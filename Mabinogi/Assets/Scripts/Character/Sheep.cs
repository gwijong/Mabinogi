using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �� ���� ĳ���� </summary>
public class Sheep : Character
{
    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.sheep;  //�� ��ų ����Ʈ ���
        loadedSkill = skillList[Define.SkillState.Combat].skill; //��ų �⺻���� �ĺ����� �غ�� ��ų ����
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override Define.InteractType Interact(Interactable other)
    {
        if (IsEnemy(this, other)) //����� ���� ������ üũ
        {
            return Define.InteractType.Attack; //��ȣ�ۿ� Ÿ���� �������� ����
        }
        return Define.InteractType.Sheeping; //���� �ƴϸ� ����ä�� ����
    }

    /// <summary> �� ��� ȿ���� </summary>
    public void Bark()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.sheep, transform.position);
    }
}
