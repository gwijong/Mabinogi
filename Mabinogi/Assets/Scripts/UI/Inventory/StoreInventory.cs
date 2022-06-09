using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary> NPC ���� �κ��丮 </summary>
public class StoreInventory : Inventory
{
    /// <summary> ���� UI ������ </summary>
    public GameObject buyUI;
    /// <summary> �Ǹ� UI ������ </summary>
    public GameObject sellUI;
    /// <summary> ���� UI �ν��Ͻ� </summary>
    GameObject buyInstance;
    /// <summary> �Ǹ� UI �ν��Ͻ� </summary>
    GameObject sellInstance;

    public Define.Item item;

    ItemData[] itemData; //������ �����͵� ������

 
    //������ Ȱ��ȭ�ɶ� �Ǹ��� �����۵� ������
    private void OnEnable()
    {
        if (FindObjectOfType<PlayerController>().target != null)
        {
            SellList sellList;
            //������ �������� ä���ֱ� ���� 
            if (FindObjectOfType<PlayerController>().target.TryGetComponent(out sellList) == true)
            {
                //�Ǹ� ��� ������
                sellList = FindObjectOfType<PlayerController>().target.GetComponent<SellList>();
                for (int i = 0; i < sellList.sellItemList.Count; i++)
                {
                    AddItem(sellList.sellItemList[i], 1);//������ �Ǹ� ������ �߰�
                }
            }
        }

    }

    //���� �������� ���
    private void OnDisable()
    {
        for (int i = 0; i<width; i++)
        {
            for(int j = 0; j<height; j++)
            {
                infoArray[j, i].SetItem(Define.Item.None, 0);//������ �������� ���
                infoArray[j, i].Clear();//���� �ʱ�ȭ
            }
        }
    }

    /// <summary> �κ��丮 ����Ʈ���� �� �κ��丮�� �� </summary>
    void OnDestroy()
    {
        inventoryList.Remove(this);
    }  

    protected override void Start()
    {
        base.Start();
        store = this;
        playerInventoryList.Remove(this);
        buyInstance = CreateUI(buyInstance, buyUI); //����â �ν��Ͻ� ����
        sellInstance = CreateUI(sellInstance, sellUI); //�Ǹ�â �ν��Ͻ� ����
        itemData = GameManager.itemManager.data; //������ �����͵� ������
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
        if(buyInstance.activeSelf==true || sellInstance.activeSelf == true) //����â�̳� �Ǹ�â�� Ȱ��ȭ�Ǿ� ������
        {
            if (inpo.activeSelf)
            {
                inpo.SetActive(false);//����â ����
            }
        }
        CloseUI(buyInstance); //���콺 Ŀ���� UI�� ��� ���¿��� ���콺 �Է� ������ ����â �ݱ�
        CloseUI(sellInstance); //���콺 Ŀ���� UI�� ��� ���¿��� ���콺 �Է� ������ �Ǹ�â �ݱ�
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

    /// <summary> ���콺 ��Ŭ�� </summary>
    public override void LeftClick(Vector2Int pos)
    {
        if (mouseItem.GetItemType() == Define.Item.None)//���콺 Ŀ���� �����ִ� �������� ������
        {
            CellInfo rootCellInfo = CheckItemRoot(pos);//���콺 Ŀ���� ��ġ�� ���� �������� ��Ʈ�� �������� �õ�
            if ((rootCellInfo.GetItemType() != Define.Item.None)) //���콺 Ŀ���� ��ġ�� ���� �������� �����ϸ�
            {
                GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//��ư �ٿ� ȿ����
                item = rootCellInfo.GetItemType();

                if(Input.GetKey(KeyCode.LeftControl))
                {
                    Buy();
                }
                else
                {
                    buyInstance.SetActive(true);
                    buyInstance.transform.position = Input.mousePosition; //UI�� ���콺 Ŀ�� ��ǥ�� �̵�

                    Text[] text = buyInstance.GetComponentsInChildren<Text>(); //�ڽ� ������Ʈ�� �ؽ�Ʈ ������Ʈ���� �����´�

                    for (int i = 0; i < itemData.Length; i++) //��ũ���ͺ������Ʈ ���̸�ŭ �ݺ�
                    {
                        if (i == (int)rootCellInfo.GetItemType() - 1) //������ �������� ��ȣ�� ���콺 Ŀ���� ������Ÿ�԰� ������ ã�� ����
                        {
                            text[0].text = itemData[i].ItemName; //0�� �ؽ�Ʈ������Ʈ�� �ؽ�Ʈ�� ������ �̸����� �ٲ۴�
                            text[1].text = "���� : " + itemData[i].SalePrice.ToString() + "Gold";//1�� �ؽ�Ʈ������Ʈ�� �ؽ�Ʈ�� �������� �ٲ۴�
                        }
                    }
                };
            }

        }
        else //���콺�� �����ִ� �������� ������
        {
            sellInstance.SetActive(true);
            sellInstance.transform.position = Input.mousePosition; //UI�� ���콺 Ŀ�� ��ǥ�� �̵�

            Text[] text = sellInstance.GetComponentsInChildren<Text>(); //�ڽ� ������Ʈ�� �ؽ�Ʈ ������Ʈ���� �����´�

            for (int i = 0; i < itemData.Length; i++) //��ũ���ͺ������Ʈ ���̸�ŭ �ݺ�
            {
                if (i == (int)mouseItem.GetItemType() - 1) //������ �������� ��ȣ�� ���콺 Ŀ���� ������Ÿ�԰� ������ ã�� ����
                {
                    text[0].text = itemData[i].ItemName; //0�� �ؽ�Ʈ������Ʈ�� �ؽ�Ʈ�� ������ �̸����� �ٲ۴�
                    text[1].text = "���� : " + (itemData[i].SalePrice * mouseItem.amount).ToString() + "Gold";//1�� �ؽ�Ʈ������Ʈ�� �ؽ�Ʈ�� �������� �ٲ۴�
                }
            }
        }
    }

    //��Ŭ���� ��Ŭ���� �Ȱ���
    public override void RightClick(Vector2Int pos)
    {
        LeftClick(pos); //��Ŭ�� �޼��� ����
    }

    /// <summary> ������ ���� </summary>
    public void Buy()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//��ư �ٿ� ȿ����
        if (PlayerController.controller.playerCharacter.gold >= item.GetItemData().SalePrice)//���� ���� ��ǰ ���ݺ��� ũ��
        {
            PlayerController.controller.playerCharacter.gold -= item.GetItemData().SalePrice;//��ǰ ���ݸ�ŭ ���ֱ�
            //invenList���� ���� �κ��丮�� ������ ���� ����ǰâ�� ����
            GetItem(item, 1); //�������� ���������� ���콺 Ŭ���� ������ �����ؼ� �κ��丮�� �߰�
        }
    }

    /// <summary> ������ �Ǹ� </summary>
    public void Sell()
    {
        PlayerController.controller.playerCharacter.gold += mouseItem.GetItemType().GetItemData().SalePrice * mouseItem.amount;//���� ���� �ǸŰ��� ����
        mouseItem.Clear(); //���콺�� �����ִ� ������ ���
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

