using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySellButton : MonoBehaviour
{
    /// <summary> ���� ��ư </summary>
    public void Buy()
    {
        Inventory.store.Buy();
        transform.parent.gameObject.SetActive(false); //����â ����
    }
    /// <summary> �Ǹ� ��ư</summary>
    public void Sell()
    {
        Inventory.store.Sell();
        transform.parent.gameObject.SetActive(false);  //�Ǹ�â ����
    }
}
