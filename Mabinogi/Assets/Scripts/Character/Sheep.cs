using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : Character
{
    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.sheep;  //�� ��ų ����Ʈ ���
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

    public override Define.InteractType Interact(Interactable other)
    {
        if (IsEnemy(this, other)) //����� ���� ������ üũ
        {
            return Define.InteractType.Attack; //��ȣ�ۿ� Ÿ���� �������� ����
        }
        return Define.InteractType.Sheeping; //���� �ƴϸ� ����ä�� ����
    }

    public void Bark()
    {
        GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.sheep);//�� ��� ȿ����
    }
}
