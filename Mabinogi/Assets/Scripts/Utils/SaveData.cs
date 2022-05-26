using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;
[Serializable]
public class InvenData
{
    /// <summary> �÷��̾��� ���</summary>
    public int gold;
    /// <summary> �ش� [�����۾��̵�] �������� ����</summary>
    public int[] itemAmount = new int[System.Enum.GetValues(typeof(Define.Item)).Length];
}

public class SaveData : MonoBehaviour
{
    /// <summary> ����� �����������̳�</summary>
    InvenData invendata;
    /// <summary> �÷��̾� �κ��丮</summary>
    PlayerInventory playerInventory;

    string readJson;
    InvenData saveFile;
    void Start()
    {
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
        playerInventory = FindObjectOfType<PlayerInventory>();

        readJson = File.ReadAllText(Application.streamingAssetsPath + "/savefile.txt");
        saveFile = JsonUtility.FromJson<InvenData>(readJson);

        //LoadData();

    }
    /// <summary> �� ���� �� ����</summary>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Invoke("LoadData", 0.1f);
    }

    private void LoadData()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(Define.Item)).Length; i++)//������ ������ŭ �ݺ�
        {
            PlayerInventory.GetItem((Define.Item)(i + 1), saveFile.itemAmount[i]);//�� ������ �������� ����� ������ŭ ����ǰâ�� �߰�
        }
        playerInventory.owner.gold = saveFile.gold; //������ ��� �ҷ���
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

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
        invendata = new InvenData();
        int itemId = 0;//���� ã�� ������ ���̵�
        int amount = 0; //���� ã�� �������� �� ����
        for (int i = 0; i < System.Enum.GetValues(typeof(Define.Item)).Length; i++) //������ ������ŭ �ݺ�
        {
            for (int y = 0; y < playerInventory.height; y++) //����ǰâ ���̸�ŭ �ݺ�
            {
                for (int x = 0; x < playerInventory.width; x++) //����ǰâ ���̸�ŭ �ݺ�
                {
                    if (playerInventory.infoArray[y, x].GetItemType() == (Define.Item)itemId + 1) //�κ��丮 ĭ ������ Ÿ���� ���� ã�� ������ Ÿ�԰� ������
                    {
                        amount += playerInventory.infoArray[y, x].amount; //���� ������ Ÿ�Գ��� ������ ����
                    }
                }
            }
            invendata.itemAmount[itemId] = amount; //�ش� ������ Ÿ���� �������� ������ ����� �����������̳ʿ� ����
            itemId++;
            amount = 0; //�ٸ� ������ Ÿ���� ������ �����ϱ� ���� �ʱ�ȭ
            invendata.gold = playerInventory.owner.gold; //�÷��̾� ��� ����
        }
        string json = JsonUtility.ToJson(invendata);
        string path = Application.streamingAssetsPath + "/savefile.txt";
        File.WriteAllText(path, json);
    }
}
