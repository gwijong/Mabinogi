using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary> �κ��丮 â ���� ����</summary>
public class InvenOpen : MonoBehaviour
{
    /// <summary> �κ��丮 â</summary>
    public GameObject inven;
    /// <summary> ���� â</summary>
    public GameObject store;
    /// <summary> �κ��丮 â�� �����ִ��� üũ</summary>
    public bool isOpen = false;
    /// <summary> ����â�� �����ִ��� üũ</summary>
    public bool isStoreOpen = false;

    private void Start()
    {
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
        StartCoroutine(SetInven());
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

    /// <summary> ���� ����</summary>
    public void StoreOpen()
    {
        isStoreOpen = !isStoreOpen;
        store.SetActive(isStoreOpen);
    }

    /// <summary> �κ��丮 �ݱ�</summary>
    public void Close()
    {

        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.inventory_close);//�ݱ� ȿ����
        Inventory.use.SetActive(false);//���â �ݱ�
        inven.SetActive(false);
        isOpen = false;
    }
    /// <summary> ���� �ݱ�</summary>
    public void StoreClose()
    {
        store.SetActive(false);
        isStoreOpen = false;
    }
    /// <summary> �����Ҷ� �κ��丮, ���� �κ��丮 ��ġ ����</summary>
    IEnumerator SetInven()
    {
        inven.transform.position = new Vector3(inven.transform.position.x-1000, inven.transform.position.y, inven.transform.position.z);
        store.transform.position = new Vector3(store.transform.position.x-1000, store.transform.position.y, store.transform.position.z);
        yield return null;
        Inventory.use.SetActive(false);//���â �ݱ�
        inven.SetActive(false);
        isOpen = false;
        StoreClose();
    }
}
