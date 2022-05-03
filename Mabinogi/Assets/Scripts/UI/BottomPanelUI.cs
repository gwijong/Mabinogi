using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BottomPanelUI : MonoBehaviour
{
    public Image hpGauge; //����� ������
    public Image mpGauge; //���� ������
    public Image spGauge; //���¹̳� ������

    public Text hpText; //ȭ�� �ϴ� UI�� ��µǴ� ����� �ؽ�Ʈ
    public Text mpText; //ȭ�� �ϴ� UI�� ��µǴ� ���� �ؽ�Ʈ
    public Text spText; //ȭ�� �ϴ� UI�� ��µǴ� ���¹̳� �ؽ�Ʈ

    Character character; //�÷��̾� ĳ���� �� �ϳ�

    float currentHP; //���� �����
    float maxHP; //�ִ� �����
    float currentMP; //���� ����
    float maxMP; //�ִ� ����
    float currentSP; //���� ���¹̳�
    float maxSP; // �ִ� ���¹̳�

    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>(); //�÷��̾� ĳ���� ã�ƿ�
        setPoint();//�ƽ��� ����
        setCurrentPoint(); //���簪�ϰ� ������ ���� ����
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown((int)Define.mouseKey.LeftClick) && Input.GetKey(KeyCode.LeftControl))
        {
            StartCoroutine("OneTickWait");//�� ������ ���� ����
        }

        setCurrentPoint();
        hpText.text = (int)currentHP + "/" + maxHP; //����� �ؽ�Ʈ ����
        mpText.text = (int)currentMP + "/" + maxMP; //���� �ؽ�Ʈ ����
        spText.text = (int)currentSP + "/" + maxSP; //���¹̳� �ؽ�Ʈ ����
    }

    /// <summary> ó�� �����ϰų� ĳ���Ͱ� �ٲ��� �ƽ��� ���� ���� </summary>
    void setPoint()
    {
        maxHP = character.hitPoint.Max;
        maxMP = character.manaPoint.Max;
        maxSP = character.staminaPoint.Max;
    }

    /// <summary> ���� ������ ���� ������Ʈ </summary>
    void setCurrentPoint()
    {
        currentHP = character.hitPoint.Current;
        currentMP = character.manaPoint.Current;
        currentSP = character.staminaPoint.Current;
        hpGauge.fillAmount = currentHP / maxHP;
        mpGauge.fillAmount = currentMP / maxMP;
        spGauge.fillAmount = currentSP / maxSP;
    }

    /// <summary> �� ������ ���� ���� �÷��̾� ã�� ������ ���� ���� </summary>
    IEnumerator OneTickWait()
    {
        yield return null;
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        setPoint();
        setCurrentPoint();
    }
}
