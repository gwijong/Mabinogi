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
    public bool isOpen = false;
    // Update is called once per frame
    RectTransform invenPos;
    private void Start()
    {
        invenPos = inven.GetComponent<RectTransform>();
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
        StartCoroutine(InvenClose());
    }
    void OnUpdate()
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
            Inventory.use.SetActive(false);//���â �ݱ�
        }
        else //����ǰâ�� ���������� ����
        {
            GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.inventory_open);//���� ȿ����
        }
        isOpen = !isOpen;
        inven.SetActive(isOpen);
    }

    public void Close()
    {

        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.inventory_close);//�ݱ� ȿ����
        Inventory.use.SetActive(false);//���â �ݱ�
        inven.SetActive(false);
        isOpen = false;

    }

    IEnumerator InvenClose()
    {
        yield return null;
        Close();
    }
}
