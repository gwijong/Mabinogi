using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Bark()
    {
        //GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.wolf01_natural_stand_offensive);// ȿ����
    }
}
