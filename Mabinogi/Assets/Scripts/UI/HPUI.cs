using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> ĳ���� ���� �޸� HP�� </summary>
public class HPUI : MonoBehaviour
{
    /// <summary> ĳ���� ���� �޸� HP�� �̹��� </summary>
    public Image hpGauge;
    /// <summary> �ִ� ����� </summary>
    float maxHP;
    /// <summary> ���� ����� </summary>
    float currentHP;
    /// <summary> �θ� ������Ʈ�� ĳ���� ��ũ��Ʈ ������Ʈ </summary>
    Character character;
    void Start()
    {
        maxHP = gameObject.GetComponentInParent<Character>().GetCurrentHP();//�ִ� ����� ������
        GameManager.update.UpdateMethod -= OnUpdate;
        GameManager.update.UpdateMethod += OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        character = gameObject.GetComponentInParent<Character>();//�θ� ������Ʈ�� ĳ���� ������Ʈ ��������
    }

    // ��� ����
    void OnUpdate()
    {
        currentHP = character.GetCurrentHP();//���� ������� ��� ������
        hpGauge.fillAmount = currentHP / maxHP; //����� �̹��� ä���� ���� ����
    }
}
