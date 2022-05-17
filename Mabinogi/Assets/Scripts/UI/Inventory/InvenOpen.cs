using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary> �κ��丮 â ���� ����</summary>
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

    /// <summary> �κ��丮 ����</summary>
    public void Open()
    {
        if (isOpen) //����ǰâ�� ���������� �ݱ�
        {
            GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.inventory_close);//�ݱ� ȿ����
            inven.GetComponent<Inventory>().use.SetActive(false);
            inven.transform.position += new Vector3(2000, 0, 0);
            isOpen = false;
        }
        else //����ǰâ�� ���������� ����
        {
            GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.inventory_open);//���� ȿ����
            inven.transform.position += new Vector3(-2000, 0, 0);
            isOpen = true;
        }
    }
}
