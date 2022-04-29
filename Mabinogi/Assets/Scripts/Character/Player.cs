using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.player;  //�÷��̾� ��ų ����Ʈ ���
        loadedSkill = skillList[Define.SkillState.Combat].skill; //��ų �⺻���� �ĺ����� �غ�� ��ų ����
        
    }

    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<Animator>();
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }
}
