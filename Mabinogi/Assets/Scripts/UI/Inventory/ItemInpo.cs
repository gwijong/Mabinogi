using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �ٴڿ� ������ ������ ����</summary>
public class ItemInpo : MonoBehaviour
{
    /// <summary> �ٴڿ� ������ ������ ����</summary>
    public Define.Item item = Define.Item.Fruit;
    /// <summary> ������ ������ ����</summary>
    public int amount = 1;
    public void GetItem()
    {
        if (GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<PlayerInventory>().
                      GetItem(item, amount) == 0) //����ǰâ�� ���鼭 �κ��丮�� �о�ֱ� �õ�
        {  //�������� ����ǰâ�� �о�ִµ� ����������
            Destroy(transform.parent.gameObject);//�ֿ� �Ծ����Ƿ� �ٴڿ� ������ ������ ����
        }
        else
        {
            Destroy(transform.parent.gameObject);//�ֿ� �Ա� ���������� �ٴڿ� ������ ������ ����
        }
    }
}
