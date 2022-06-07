using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>ĳ���� �Ӹ� ���� �޸� ��ų ��ǳ��</summary>
public class SkillUI : MonoBehaviour
{
    Character character; //�÷��̾�ų� ���̰ų� ������� ��� ĳ����
    public Image[] skillImages;//�غ�� ��ų �̹�����
    public Canvas skillCanvas; //��ų �̹����� ������ ���׸��� ĵ����
    IEnumerator skillCastingCoroutine; //��ų ���� ��ǳ�� �������� �ڷ�ƾ
    bool coroutineFlag = false;//�ڷ�ƾ �ߺ����� ����
    void Start()
    {
        character = GetComponentInParent<Character>(); //�θ� ������Ʈ�� ĳ���� ã�ƺ�
        if (character==null) //ĳ���� �� ã����
        {
            character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>(); //�÷��̾� ĳ���� ����
        }
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
    }

    
    void OnUpdate()
    {
        if (character.GetreservedSkill() == null) //�غ����� ��ų�� ������
        {
            switch (character.GetloadedSkill().type)
            {
                case Define.SkillState.Combat:  
                    Reset();
                    //�ĺ��� ��ǳ���� Ȱ��ȭ���� �ʴ´�

                    break;
                case Define.SkillState.Defense:
                    Reset();
                    skillImages[(int)Define.SkillState.Defense].gameObject.SetActive(true); //���潺 �̹��� Ȱ��ȭ

                    break;
                case Define.SkillState.Smash:
                    Reset();
                    skillImages[(int)Define.SkillState.Smash].gameObject.SetActive(true); //���Ž� �̹��� Ȱ��ȭ

                    break;
                case Define.SkillState.Counter:
                    Reset();
                    skillImages[(int)Define.SkillState.Counter].gameObject.SetActive(true); //ī���� �̹��� Ȱ��ȭ

                    break;
                case Define.SkillState.Windmill:
                    Reset();
                    skillImages[(int)Define.SkillState.Windmill].gameObject.SetActive(true); //����� �̹��� Ȱ��ȭ

                    break;
                case Define.SkillState.Icebolt:
                    Reset();
                    skillImages[(int)Define.SkillState.Icebolt].gameObject.SetActive(true); //���̽���Ʈ �̹��� Ȱ��ȭ

                    break;
            }
        }
        else //�غ����� ��ų�� ������
        {
            switch (character.GetreservedSkill().type)
            {
                case Define.SkillState.Combat:
                    ImageOff(); //��ǳ�� �̹��� ����

                    break;
                case Define.SkillState.Defense:
                    Casting();
                    skillImages[(int)Define.SkillState.Defense].gameObject.SetActive(true); //���潺 �̹��� Ȱ��ȭ

                    break;
                case Define.SkillState.Smash:
                    Casting();
                    skillImages[(int)Define.SkillState.Smash].gameObject.SetActive(true); //���Ž� �̹��� Ȱ��ȭ

                    break;
                case Define.SkillState.Counter:
                    Casting();
                    skillImages[(int)Define.SkillState.Counter].gameObject.SetActive(true); //ī���� �̹��� Ȱ��ȭ

                    break;
                case Define.SkillState.Windmill:
                    Casting();
                    skillImages[(int)Define.SkillState.Windmill].gameObject.SetActive(true); //����� �̹��� Ȱ��ȭ

                    break;
                case Define.SkillState.Icebolt:
                    Casting();
                    skillImages[(int)Define.SkillState.Icebolt].gameObject.SetActive(true); //���̽���Ʈ �̹��� Ȱ��ȭ

                    break;
            }
        }
    }
    /// <summary> ��ų ���� �ִϸ��̼� �ڷ�ƾ ����� �޼��� </summary>
    void Casting()
    {
        ImageOff();
        if (coroutineFlag == false)
        {
            coroutineFlag = true;
            skillCastingCoroutine = SkillCasting();
            StartCoroutine(skillCastingCoroutine);
        }
    }

    /// <summary> ��ų ���� �ִϸ��̼� �ڷ�ƾ ���� </summary>
    private void Reset()
    {
        coroutineFlag = false;
        ImageOff(); 
        if (skillCastingCoroutine != null)
        {
            StopCoroutine(skillCastingCoroutine);
        }
        skillCanvas.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }

    /// <summary> ��ų ������ �� �� ���� </summary>
    void ImageOff()
    {
        for (int i = 0; i < skillImages.Length; i++)
        {
            skillImages[i].gameObject.SetActive(false);
        }
    }

    /// <summary> ��ų ��ǳ�� �ִϸ��̼� �ڷ�ƾ </summary>
    IEnumerator SkillCasting()
    {
        for (int i = 0; i < 10; i++)
        {  //��ǳ�� ���̴� ����
            skillCanvas.transform.localScale = new Vector3(0.4f - (float)i / 100, 0.4f - (float)i / 100, 0.4f - (float)i / 100);
            yield return new WaitForSeconds(0.04f);
        }
        for (int i = 0; i < 10; i++)
        {  //��ǳ�� Ű��� ����
            skillCanvas.transform.localScale = new Vector3(0.3f + (float)i / 100, 0.3f + (float)i / 100, 0.3f + (float)i / 100);
            yield return new WaitForSeconds(0.04f);
        }
        coroutineFlag = false;
    }

    /// <summary> �غ����̰ų� �غ�� ��ų ��ҵǸ� �ʱ�ȭ </summary>
    public void SkillCancel()
    {
        Reset();
        character.Casting(Define.SkillState.Combat);//�⺻ �������� ��ų �ʱ�ȭ
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.skill_cancel, transform.position);//��ų ��� ȿ����
    }
}
