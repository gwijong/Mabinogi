using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ���� �� </summary>
public class Golem : Character
{
    bool bossDieCheck = false;
    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.golem;  //�� ��ų ����Ʈ ���
        loadedSkill = skillList[Define.SkillState.Combat].skill; //��ų �⺻���� �ĺ����� �غ�� ��ų ����
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (die == true && bossDieCheck ==false) //���� ����� ����Ʈ ȿ��
        {
            bossDieCheck = true;
            GameObject dieEffect = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/ChargingPop"));
            dieEffect.transform.position = gameObject.transform.position + Vector3.up * 3;
            GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.item_get, transform.position);// ȿ����
        }
    }

    public override void Respawn()
    {
        base.Respawn();
        bossDieCheck = false;
    }
    /// <summary> ������� ȿ���� </summary>
    public void StandOffensive()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.golem01_woo, transform.position);// ȿ����
    }
    /// <summary> �ȱ� ȿ���� </summary>
    public void Walk()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.golem01_walk, transform.position);// ȿ����
    }
    /// <summary> �Ͼ�� ȿ���� </summary>
    public void DownToStand()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.golem01_downb_to_stand, transform.position);// ȿ����
    }
    /// <summary> �±� ȿ���� </summary>
    public void Hit()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.golem01_hit, transform.position);// ȿ����
    }
    /// <summary> �ٿ� ȿ���� </summary>
    public void Down()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.golem01_blowaway_ground, transform.position);// ȿ����
    }

}
