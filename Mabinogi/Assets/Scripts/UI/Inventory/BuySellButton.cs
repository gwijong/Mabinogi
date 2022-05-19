using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySellButton : MonoBehaviour
{
    /// <summary> ���� ��ư </summary>
    public void Buy()
    {

        Inventory[] inven = FindObjectsOfType<Inventory>(); 
        StoreInventory store = FindObjectOfType<StoreInventory>();
        inven[0].GetItem(store.item, 1); //�������� ���������� ���콺 Ŭ���� ������ �����ؼ� �κ��丮�� �߰�
        transform.parent.gameObject.SetActive(false); //����â ����
    }
    /// <summary> �Ǹ� ��ư</summary>
    public void Sell()
    {
        Inventory.mouseItem.Clear(); //���콺�� �����ִ� ������ ���
        transform.parent.gameObject.SetActive(false);  //�Ǹ�â ����
    }
}
