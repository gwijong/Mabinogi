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
        anim = GetComponentInChildren<Animator>();
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    /// <summary> ���� ä�� </summary>
    public void Sheeping()
    {
        PlayAnim("Sheeping");
        StartCoroutine(Wait(5f));
        //GameManager.itemManager.DropItem(Define.Item.Wool, 1);
        StartCoroutine(DropItem());
    }

    /// <summary> 2�ʵ� ���� ���� </summary>
    IEnumerator DropItem() 
    {
        yield return new WaitForSeconds(2.0f);
        GameManager.itemManager.DropItem(Define.Item.Wool, 1);
    }
}
