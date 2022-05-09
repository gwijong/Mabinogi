using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InvenOpen : MonoBehaviour
{
    /// <summary> �κ��丮 â</summary>
    public GameObject inven;
    /// <summary> �κ��丮 â�� �����ִ��� üũ</summary>
    bool isOpen = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) //IŰ�� ������ ����ǰâ ����
        {
            Open();
        }
    }

    public void Open()
    {
        if (isOpen) //����ǰâ�� ���������� �ݱ�
        {
            inven.transform.position += new Vector3(2000, 0, 0);
            isOpen = false;
        }
        else //����ǰâ�� ���������� ����
        {
            inven.transform.position += new Vector3(-2000, 0, 0);
            isOpen = true;
        }
    }
}
