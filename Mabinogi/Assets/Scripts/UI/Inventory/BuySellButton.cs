using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySellButton : MonoBehaviour
{
    /// <summary> ���� ��ư </summary>
    public void Buy()
    {
        Inventory[] invenArray = FindObjectsOfType<Inventory>();  //���� �κ��丮 ���� ��� �κ��丮�� �� �� ������
        List<Inventory> invenList = new List<Inventory>(); //�迭�� ����Ʈ�� ��ȯ�� ����Ʈ
        StoreInventory store = FindObjectOfType<StoreInventory>(); //���� �κ��丮
        for(int i = 0; i< invenArray.Length; i++)//�迭�� ����Ʈ�� ��ȯ
        {
            invenList.Add(invenArray[i]);
        }

        for (int i = 0; i < invenList.Count; i++)
        {
            if(invenList[i] == store)//�κ��丮 ����Ʈ���� ���� �κ��丮�� ����
            {
                invenList.Remove(invenList[i]);
            }
        }
        if(FindObjectOfType<Gold>().gold>= store.item.GetItemData().SalePrice)//���� ���� ��ǰ ���ݺ��� ũ��
        {
            FindObjectOfType<Gold>().gold -= store.item.GetItemData().SalePrice;//��ǰ ���ݸ�ŭ ���ֱ�
            //invenList���� ���� �κ��丮�� ������ ���� ����ǰâ�� ����
            invenList[0].GetItem(store.item, 1); //�������� ���������� ���콺 Ŭ���� ������ �����ؼ� �κ��丮�� �߰�
            transform.parent.gameObject.SetActive(false); //����â ����
        }

    }
    /// <summary> �Ǹ� ��ư</summary>
    public void Sell()
    {
        FindObjectOfType<Gold>().gold += Inventory.mouseItem.GetItemType().GetItemData().SalePrice* Inventory.mouseItem.amount;//���� ���� �ǸŰ��� ����
        Inventory.mouseItem.Clear(); //���콺�� �����ִ� ������ ���
        transform.parent.gameObject.SetActive(false);  //�Ǹ�â ����
    }
}
