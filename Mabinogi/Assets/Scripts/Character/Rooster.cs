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
    public void Fly()
    {
        GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.chicken_fly);//�� ���� ȿ����
    }

    /// <summary> �ٿ� ȿ���� </summary>
    public void Blowaway()
    {
        GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.chicken_down);// ȿ����
    }
    /// <summary> �±� ȿ���� </summary>
    public void Hit()
    {
        GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.chicken_hit);// ȿ����
    }
}
