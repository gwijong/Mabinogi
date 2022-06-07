using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �� ���� </summary>
public class Bear : Character
{
    protected override void Awake()
    {
        base.Awake();
        walkSpeed = data.Speed;//���� ������ �޸�
        skillList = SkillList.bear;  //�� ��ų ����Ʈ ���
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
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.bear01_natural_stand_offensive, transform.position);// ȿ����
    }
    /// <summary> ���Ž� ȿ���� </summary>
    public void Samsh()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.bear01_natural_attack_smash, transform.position);// ȿ����
    }
    /// <summary> ī���� ȿ���� </summary>
    public void Counter()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.bear01_natural_attack_counter, transform.position);// ȿ����
    }
    /// <summary> �ٿ� ȿ���� </summary>
    public void Blowaway()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.bear01_natural_blowaway, transform.position);// ȿ����
    }
    /// <summary> �±� ȿ���� </summary>
    public void Hit()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.bear01_natural_hit, transform.position);// ȿ����
    }

}
