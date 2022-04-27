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


    protected override void Update()
    {
        base.Update();
    }
}
