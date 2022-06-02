using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// SkillUICanvas �� �޸�
/// <summary> ĳ���� �Ӹ� ���� �޸��� ��ų ��ǳ�� </summary>
public class SkillBubble : MonoBehaviour
{
    Character character; //�÷��̾�ų� ���̰ų� ������� ��� ĳ����
    public Sprite[] skillSprites;//�غ�� ��ų �̹��� ��������Ʈ��
    Image skillImage; //��ų ��������Ʈ�� ��� �̹��� ������Ʈ
    Image backGroundImage; //��ų ��������Ʈ ���� ��� ��ǳ��
    IEnumerator skillCastingCoroutine; //��ų ���� ��ǳ�� �������� �ڷ�ƾ
    bool coroutineFlag = false;//�ڷ�ƾ �ߺ����� ����
    void Start()
    {
        backGroundImage = GetComponentsInChildren<Image>()[0];
        skillImage = GetComponentsInChildren<Image>()[1]; //ĵ������ �ڽ� ������Ʈ�� �̹��� ������Ʈ ã�ƿ�
        character = GetComponentInParent<Character>(); //�θ� ������Ʈ�� ĳ���� ã�ƺ�
        if (character == null) //ĳ���� �� ã����
        {
            character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>(); //�÷��̾� ĳ���� ����
        }
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
    }


    void OnUpdate()
    {
        if (character.die == true) //ĳ���Ͱ� ������ ��ų ��ǳ�� ��Ȱ��ȭ
        {
            Reset();
            skillImage.sprite = skillSprites[(int)character.GetloadedSkill().type]; //��ų ��������Ʈ ����
            backGroundImage.enabled = false; //��ų ��ǳ�� �̹��� ��Ȱ��ȭ
            skillImage.enabled = false; //��ų ��ǳ�� �̹��� ��Ȱ��ȭ
        }

        if (character.GetreservedSkill() == null) //�غ����� ��ų�� ������
        {
            Reset();
            skillImage.sprite = skillSprites[(int)character.GetloadedSkill().type]; //��ų ��������Ʈ ����
            if(character.GetloadedSkill().type == Define.SkillState.Combat) //��ų�� �⺻�����̸�
            {
                backGroundImage.enabled = false; //��ų ��ǳ�� �̹��� ��Ȱ��ȭ
                skillImage.enabled = false; //��ų ��ǳ�� �̹��� ��Ȱ��ȭ
            }
            
        }
        else //�غ����� ��ų�� ������
        {
            Casting();
            skillImage.sprite = skillSprites[(int)character.GetreservedSkill().type]; //��ų ��������Ʈ ����
            if(character.GetreservedSkill().type != Define.SkillState.Combat)//��ų�� �⺻������ �ƴϸ�
            {
                backGroundImage.enabled = true; //��ų ��ǳ�� �̹��� Ȱ��ȭ
                skillImage.enabled = true; //��ų ��ǳ�� �̹��� Ȱ��ȭ
            }
        }
    }

    /// <summary> ��ų ���� �ִϸ��̼� �ڷ�ƾ ���� </summary>
    private void Reset()
    {
        coroutineFlag = false;
        if (skillCastingCoroutine != null)
        {
            StopCoroutine(skillCastingCoroutine);
        }
        transform.localScale = new Vector3(0.4f, 0.4f, 0.4f); //��ų ��ǳ�� ũ�⸦ ���ʰ����� ����
    }


    /// <summary> ��ų ���� �ִϸ��̼� �ڷ�ƾ ����� �޼��� </summary>
    void Casting()
    {
        if (coroutineFlag == false)
        {
            coroutineFlag = true;
            skillCastingCoroutine = SkillCasting();
            StartCoroutine(skillCastingCoroutine);  //��ų ��ǳ�� �ִϸ��̼� �ڷ�ƾ ����
        }
    }


    /// <summary> ��ų ��ǳ�� �ִϸ��̼� �ڷ�ƾ </summary>
    IEnumerator SkillCasting()
    {
        for (int i = 0; i < 10; i++)
        {  //��ǳ�� ���̴� ����
            transform.localScale = new Vector3(0.4f - (float)i / 100, 0.4f - (float)i / 100, 0.4f - (float)i / 100);
            yield return new WaitForSeconds(0.04f);
        }
        for (int i = 0; i < 10; i++)
        {  //��ǳ�� Ű��� ����
            transform.localScale = new Vector3(0.3f + (float)i / 100, 0.3f + (float)i / 100, 0.3f + (float)i / 100);
            yield return new WaitForSeconds(0.04f);
        }
        coroutineFlag = false;
    }

    /// <summary> �غ����̰ų� �غ�� ��ų ��ҵǸ� �ʱ�ȭ </summary>
    public void SkillCancel()
    {
        Reset();       
        backGroundImage.enabled = false; //��ų ��ǳ�� �̹��� ��Ȱ��ȭ
        skillImage.enabled = false; //��ų ��ǳ�� �̹��� ��Ȱ��ȭ
        character.Casting(Define.SkillState.Combat);
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.skill_cancel);//��ų ��� ȿ����
        character.PlayAnim("SkillCancel");
    }
}
