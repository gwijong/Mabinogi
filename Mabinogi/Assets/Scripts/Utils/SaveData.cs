using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;
/// <summary> �κ��丮 ���� ���� ������ ������</summary>
[Serializable]
public class InvenData
{
    /// <summary> �÷��̾��� ���</summary>
    public int gold;
    /// <summary> �ش� [�����۾��̵�] �������� ����</summary>
    public int[] itemAmount = new int[System.Enum.GetValues(typeof(Define.Item)).Length];
}

/// <summary> ����ǰ ������ ����</summary>
public class SaveData : MonoBehaviour
{
    /// <summary> ����� �����������̳�</summary>
    InvenData invendata;
    /// <summary> �÷��̾� �κ��丮</summary>
    PlayerInventory playerInventory;
    /// <summary> Json ���ڿ�</summary>
    string readJson;
    /// <summary> ���̺� ���� ������</summary>
    InvenData saveFile;
    void Start()
    {
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
        playerInventory = FindObjectOfType<PlayerInventory>();//�÷��̾� �κ��丮
        readJson = File.ReadAllText(Application.streamingAssetsPath + "/savefile.txt"); //���̺� ���� �������� ���ڿ�
        saveFile = JsonUtility.FromJson<InvenData>(readJson); //���̺� ���� ������
    }
    /// <summary> �� ���� �� ����</summary>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Invoke("LoadData", 0.1f);//�� �ε庸�� ��¦ �ʰ� �����ؾ� ����ǰ ������ �ε尡 ����� ��
    }

    /// <summary> ����ǰ ������ �ҷ�����</summary>
    private void LoadData()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(Define.Item)).Length; i++)//������ ������ŭ �ݺ�
        {
            PlayerInventory.GetItem((Define.Item)(i + 1), saveFile.itemAmount[i]);//�� ������ �������� ����� ������ŭ ����ǰâ�� �߰�
        }
        playerInventory.owner.gold = saveFile.gold; //������ ��� �ҷ���
    }

    /// <summary> �� ���� �� ������ �ε� �޼��� ����</summary>
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary> �� ���� �� ������ �ε� �޼��� ��</summary>
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary> ���� �ݺ�</summary>
    void OnUpdate()
    {
        Save();
    }

    /// <summary> ���, ������ ���� ����</summary>
    void Save()
    {
        invendata = new InvenData(); // ����ǰ ������
        int itemId = 0;//���� ã�� ������ ���̵�
        int amount = 0; //���� ã�� �������� �� ����
        for (int i = 0; i < System.Enum.GetValues(typeof(Define.Item)).Length; i++) //������ ������ŭ �ݺ�
        {
            amount = playerInventory.GetItemAmount((Define.Item)itemId + 1);
            invendata.itemAmount[itemId] = amount; //�ش� ������ Ÿ���� �������� ������ ����� �����������̳ʿ� ����
            itemId++;
            invendata.gold = playerInventory.owner.gold; //�÷��̾� ��� ����
        }
        string json = JsonUtility.ToJson(invendata); //����Ǵ� ���ڿ�
        string path = Application.streamingAssetsPath + "/savefile.txt"; //���
        File.WriteAllText(path, json); //������ ��ο� json ���ڿ� ����
    }
}
