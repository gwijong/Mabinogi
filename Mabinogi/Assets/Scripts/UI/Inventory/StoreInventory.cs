using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class StoreInventory : Inventory
{
    /// <summary> ���� UI ������ </summary>
    public GameObject buyUI;
    /// <summary> ���� UI �ν��Ͻ� </summary>
    GameObject buy;
    /// <summary> �Ǹ� UI ������ </summary>
    public GameObject sellUI;
    /// <summary> �Ǹ� UI �ν��Ͻ� </summary>
    public GameObject sell;

    public Define.Item item;

    ItemData[] itemData; //������ �����͵� ������
    protected override void Start()
    {
        base.Start();
        buy = CreateUI(buy, buyUI);
        sell = CreateUI(sell, sellUI);
        itemData = GameManager.itemManager.data; //������ �����͵� ������
        GetItem(Define.Item.Wool, 1); //�κ��丮 ����� �⺻ ������1
        GetItem(Define.Item.Fruit, 1); //�κ��丮 ����� �⺻ ������2
    }

    /// <summary> ����, �Ǹ� UI ���ӿ�����Ʈ ���� </summary>
    GameObject CreateUI(GameObject go,GameObject prefab)
    {
        if (go == null)//UI�� ������
        {
            go = Instantiate(prefab);//UI����
            go.transform.SetParent(GameObject.FindGameObjectWithTag("Inventory").transform);//����ǰâ�� �ڽ����� ����
            Image image = go.GetComponent<Image>();//�̹��� ������Ʈ ��������
            image.rectTransform.pivot = new Vector2(0, 1); // UI �߽����� 0,1�� ����
            go.SetActive(false);//UI�� �ϴ� ����
            return go;
        }
        return null;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if(buy.activeSelf==true || sell.activeSelf == true) //����â�̳� �Ǹ�â�� Ȱ��ȭ�Ǿ� ������
        {
            inpo.SetActive(false);//����â ����
        }
        CloseUI(buy); //���콺 Ŀ���� UI�� ��� ���¿��� ���콺 �Է� ������ ����â �ݱ�
        CloseUI(sell); //���콺 Ŀ���� UI�� ��� ���¿��� ���콺 �Է� ������ �Ǹ�â �ݱ�
    }

    /// <summary> ���콺 Ŀ���� UI�� ��� ���¸� ����, �Ǹ�â �ݱ� </summary>
    void CloseUI(GameObject ui)
    {
        if(ui == null)
        {
            return;
        }
        //���콺 Ŀ���� �Ǹ�â�� ��� ���¿���
        if (Input.mousePosition.x > ui.transform.position.x + 140
            || Input.mousePosition.x < ui.transform.position.x - 40
            || Input.mousePosition.y > ui.transform.position.y + 30
            || Input.mousePosition.y < ui.transform.position.y - 140)
        {
            if (Input.GetMouseButtonDown(0)) //���콺 ��Ŭ���ϸ�
            {
                ui.SetActive(false); //����, �Ǹ�â �ݱ�
            }
        }
    }

    //��Ŭ��
    public override void LeftClick(Vector2Int pos)
    {
        if (mouseItem.GetItemType() == Define.Item.None)//���콺 Ŀ���� �����ִ� �������� ������
        {
            CellInfo rootCellInfo = CheckItemRoot(pos);//���콺 Ŀ���� ��ġ�� ���� �������� ��Ʈ�� �������� �õ�
            if ((rootCellInfo.GetItemType() != Define.Item.None)) //���콺 Ŀ���� ��ġ�� ���� �������� �����ϸ�
            {
                item = rootCellInfo.GetItemType();
                buy.SetActive(true);
                buy.transform.position = Input.mousePosition; //UI�� ���콺 Ŀ�� ��ǥ�� �̵�

                Text[] text = buy.GetComponentsInChildren<Text>(); //�ڽ� ������Ʈ�� �ؽ�Ʈ ������Ʈ���� �����´�

                for (int i = 0; i < itemData.Length; i++) //��ũ���ͺ������Ʈ ���̸�ŭ �ݺ�
                {
                    if (i == (int)rootCellInfo.GetItemType() - 1) //������ �������� ��ȣ�� ���콺 Ŀ���� ������Ÿ�԰� ������ ã�� ����
                    {
                        text[0].text = itemData[i].ItemName; //0�� �ؽ�Ʈ������Ʈ�� �ؽ�Ʈ�� ������ �̸����� �ٲ۴�
                        text[1].text = "���� : " + itemData[i].SalePrice.ToString() + "Gold";//1�� �ؽ�Ʈ������Ʈ�� �ؽ�Ʈ�� �������� �ٲ۴�
                    }
                }
            }

        }
        else //���콺�� �����ִ� �������� ������
        {
            sell.SetActive(true);
            sell.transform.position = Input.mousePosition; //UI�� ���콺 Ŀ�� ��ǥ�� �̵�

            Text[] text = sell.GetComponentsInChildren<Text>(); //�ڽ� ������Ʈ�� �ؽ�Ʈ ������Ʈ���� �����´�

            for (int i = 0; i < itemData.Length; i++) //��ũ���ͺ������Ʈ ���̸�ŭ �ݺ�
            {
                if (i == (int)mouseItem.GetItemType() - 1) //������ �������� ��ȣ�� ���콺 Ŀ���� ������Ÿ�԰� ������ ã�� ����
                {
                    text[0].text = itemData[i].ItemName; //0�� �ؽ�Ʈ������Ʈ�� �ؽ�Ʈ�� ������ �̸����� �ٲ۴�
                    text[1].text = "���� : " + itemData[i].SalePrice.ToString() + "Gold";//1�� �ؽ�Ʈ������Ʈ�� �ؽ�Ʈ�� �������� �ٲ۴�
                }
            }
        }
    }

    //��Ŭ���� ��Ŭ���� �Ȱ���
    public override void RightClick(Vector2Int pos)
    {
        LeftClick(pos); //��Ŭ�� �޼��� ����
    }

    /// <summary> ����, �Ǹ�â Ȱ��ȭ�ϰ� �����۵����Ϳ��� ���� �̾ƿ� ���� </summary>
    GameObject SetUI(GameObject go)
    { 
        go.transform.position = Input.mousePosition; //UI�� ���콺 Ŀ�� ��ǥ�� �̵�

        Text[] text = go.GetComponentsInChildren<Text>(); //�ڽ� ������Ʈ�� �ؽ�Ʈ ������Ʈ���� �����´�

        for (int i = 0; i < itemData.Length; i++) //��ũ���ͺ������Ʈ ���̸�ŭ �ݺ�
        {
            if (i == (int)mouseItem.GetItemType()-1) //������ �������� ��ȣ�� ���콺 Ŀ���� ������Ÿ�԰� ������ ã�� ����
            {
                Debug.Log(mouseItem.GetItemType() - 1);
                text[0].text = itemData[i].ItemName; //0�� �ؽ�Ʈ������Ʈ�� �ؽ�Ʈ�� ������ �̸����� �ٲ۴�
                text[1].text = "���� : " + itemData[i].SalePrice.ToString() + "Gold";//1�� �ؽ�Ʈ������Ʈ�� �ؽ�Ʈ�� �������� �ٲ۴�
            }
        }
        return go;
    }
}

