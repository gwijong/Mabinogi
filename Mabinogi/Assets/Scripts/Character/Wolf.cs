using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ���� ���� ĳ���� </summary>
public class Wolf : Character
{
    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.wolf;  //���� ��ų ����Ʈ ���
        loadedSkill = skillList[Define.SkillState.Combat].skill; //��ų �⺻���� �ĺ����� �غ�� ��ų ����
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    public void Bark()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.wolf01_natural_stand_offensive, transform.position);//���� ¢�� ȿ����
    }
    public void Samsh()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.wolf01_natural_attack_smash, transform.position);// ȿ����
    }
    /// <summary> ī���� ȿ���� </summary>
    public void Counter()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.wolf01_natural_attack_counter, transform.position);// ȿ����
    }
    /// <summary> �ٿ� ȿ���� </summary>
    public void Blowaway()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.wolf01_natural_down, transform.position);// ȿ����
    }
    /// <summary> �±� ȿ���� </summary>
    public void Hit()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.wolf01_natural_hit, transform.position);// ȿ����
    }
}
