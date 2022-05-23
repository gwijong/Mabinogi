using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    GameObject Dialog;
    
    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.player;  //�÷��̾� ��ų ����Ʈ ���
        loadedSkill = skillList[Define.SkillState.Combat].skill; //��ų �⺻���� �ĺ����� �غ�� ��ų ����
        
    }

    private void Start()
    {
        Dialog = GameObject.FindGameObjectWithTag("Dialog");
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
        StartCoroutine(DropItem(Define.Item.Wool));
    }

    /// <summary> �ް� ä�� </summary>
    public void Egg()
    {
        PlayAnim("Egg");
        StartCoroutine(Wait(5f));
        StartCoroutine(DropItem(Define.Item.Egg));
    }

    /// <summary> 3�ʵ� ������ ���� </summary>
    IEnumerator DropItem(Define.Item item) 
    {
        yield return new WaitForSeconds(2.8f);
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.emotion_success);//���� ȿ����
        GameManager.itemManager.DropItem(item, 1);
    }

    /// <summary> NPC�� ��ȭ ���� </summary>
    public void Talk(NPC target)
    {
        if(target == this)
        {
            return;
        }

        DialogTalk dialog = FindObjectOfType<DialogTalk>();
        dialog.SetTarget(this, target);
    }
}
