using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Character
{
    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.golem;  //�� ��ų ����Ʈ ���
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
    public void StandOffensive()
    {
        GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.golem01_woo);// ȿ����
    }
    /// <summary> ���Ž� ȿ���� </summary>
    public void Samsh()
    {
        //GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.bear01_natural_attack_smash);// ȿ����
    }
    /// <summary> ī���� ȿ���� </summary>
    public void Counter()
    {
        //GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.bear01_natural_attack_counter);// ȿ����
    }
    /// <summary> �ٿ� ȿ���� </summary>
    public void Blowaway()
    {
        //GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.bear01_natural_blowaway);// ȿ����
    }
    /// <summary> �±� ȿ���� </summary>
    public void Hit()
    {
        //GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.bear01_natural_hit);// ȿ����
    }

}
