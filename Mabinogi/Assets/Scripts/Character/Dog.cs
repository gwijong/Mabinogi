using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Character
{

    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.dog;  //�� ��ų ����Ʈ ���
        loadedSkill = skillList[Define.SkillState.Combat].skill; //��ų �⺻���� �ĺ����� �غ�� ��ų ����
    }

    private void Start()
    {
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    /// <summary> ������� ȿ���� </summary>
    public void Bark()
    {
        GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.dog01_natural_stand_offensive);//�� ¢�� ȿ����
    }
    /// <summary> �ٿ� ȿ���� </summary>
    public void Blowaway()
    {
        GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.dog01_natural_blowaway);// ȿ����
    }
    /// <summary> �±� ȿ���� </summary>
    public void Hit()
    {
        GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.dog01_natural_hit);// ȿ����
    }
}
