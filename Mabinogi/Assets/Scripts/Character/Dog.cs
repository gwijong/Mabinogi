using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ���� �� </summary>
public class Dog : Character
{

    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.dog;  //�� ��ų ����Ʈ ���
        loadedSkill = skillList[Define.SkillState.Combat].skill; //��ų �⺻���� �ĺ����� �غ�� ��ų ����
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    /// <summary> ������� ȿ���� </summary>
    public void Bark()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.dog01_natural_stand_offensive,transform.position);//�� ¢�� ȿ����
    }
    /// <summary> �ٿ� ȿ���� </summary>
    public void Blowaway()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.dog01_natural_blowaway, transform.position);// ȿ����
    }
    /// <summary> �±� ȿ���� </summary>
    public void Hit()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.dog01_natural_hit, transform.position);// ȿ����
    }
}
