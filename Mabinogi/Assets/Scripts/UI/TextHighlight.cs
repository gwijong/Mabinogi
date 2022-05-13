using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�� ��ũ��Ʈ�� �̸� ���� ��ư�� ĵ������ �޷��ִ�.  
/// <summary> ĳ���ͳ� ������ �̸� �ؽ�Ʈ�� �ؽ�Ʈ ���� ��ưUI ����</summary>
public class TextHighlight : MonoBehaviour
{
    /// <summary>�ؽ�Ʈ ���� ��ưUI</summary>
    GameObject ui;
    private void Start()
    {
        //������Ʈ �Ŵ����� Update�޼��忡 �����ֱ�
        GameManager.update.UpdateMethod -= OnUpdate;
        GameManager.update.UpdateMethod += OnUpdate;
        ui = GetComponentInChildren<Button>().gameObject; //��ư�� ������ �ִ� �ڽ� ���ӿ�����Ʈ
    }

    void OnUpdate()
    {
        if (ui != null)
        {
            if (Input.GetKey(KeyCode.LeftAlt)) //���� ��ƮŰ�� ������
            {
                ui.SetActive(true); //��ư ���ӿ�����Ʈ Ȱ��ȭ
            }
            else //��ҿ� ��ư ���ӿ�����Ʈ ��Ȱ��ȭ
            {
                ui.SetActive(false); //��ư ���ӿ�����Ʈ ��Ȱ��ȭ
            }
        }
    }
}
