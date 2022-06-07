using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooster : Character
{
    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.rooster;  //��ż ��ų ����Ʈ ���
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
