using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    public Image hpGauge;
    float maxHP;
    float currentHP;
    Character character;
    void Start()
    {
        maxHP = gameObject.GetComponentInParent<Character>().GetCurrentHP();
        GameManager.update.UpdateMethod -= OnUpdate;
        GameManager.update.UpdateMethod += OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        character = gameObject.GetComponentInParent<Character>();//�θ� ������Ʈ�� ĳ���� ������Ʈ ��������
    }

    // Update is called once per frame
    void OnUpdate()
    {
        currentHP = character.GetCurrentHP();//����� ������
        hpGauge.fillAmount = currentHP / maxHP; //����� �̹��� ä���� ���� ����
    }
}
