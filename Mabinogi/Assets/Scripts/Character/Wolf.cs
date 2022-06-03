using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Character
{
    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.wolf;  //���� ��ų ����Ʈ ���
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
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.wolf01_natural_stand_offensive, transform.position);//���� ¢�� ȿ����
    }
}
