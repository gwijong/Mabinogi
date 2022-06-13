using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �� ���� ĳ���� </summary>
public class Hen : Character
{

    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.hen;  //��ż ��ų ����Ʈ ���
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
        return Define.InteractType.Egg; //���� �ƴϸ� �ް�ä�� ����
    }

    /// <summary> �� ���� ȿ���� </summary>
    public void Fly()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.chicken_fly, transform.position);
    }

    /// <summary> �ٿ� ȿ���� </summary>
    public void Blowaway()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.chicken_down, transform.position);// ȿ����
    }
    /// <summary> �±� ȿ���� </summary>
    public void Hit()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.chicken_hit, transform.position);// ȿ����
    }
}
