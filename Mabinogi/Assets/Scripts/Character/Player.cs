using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �÷��̾� ĳ���� </summary>
public class Player : Character
{
    public GameObject dieCanvas;
    public GameObject diePanel;
    protected override void Awake()
    {
        base.Awake();
        skillList = SkillList.player;  //�÷��̾� ��ų ����Ʈ ���
        loadedSkill = skillList[Define.SkillState.Combat].skill; //��ų �⺻���� �ĺ����� �غ�� ��ų ����
        anim = GetComponentInChildren<Animator>();
        GameObject Effect = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/SpawnSimpleBlue"));
        Effect.transform.position = gameObject.transform.position + Vector3.up;
        Destroy(Effect, 2f);
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
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.emotion_success, transform.position);//���� ȿ����
        GameManager.itemManager.DropItem(item, 1);
    }

    /// <summary> NPC�� ��ȭ ���� </summary>
    public void Talk(NPC target)
    {
        if(target == this)
        {
            return;
        }

        DialogTalk dialog = FindObjectOfType<DialogTalk>();//��ȭ ĵ���� ������
        dialog.SetTarget(this, target);//��ȭ ��� ����
    }

    /// <summary> �÷��̾� ��� �� ó�� </summary>
    public override void PlayerDie()
    {
        GameManager.soundManager.PlayBgmPlayer(Define.Scene.Die); //��� ������� ���
        dieCanvas.SetActive(true);
        StartCoroutine(Die());//��� ĵ���� Ȱ��ȭ
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1.5f);
        diePanel.SetActive(true);
    }
}
