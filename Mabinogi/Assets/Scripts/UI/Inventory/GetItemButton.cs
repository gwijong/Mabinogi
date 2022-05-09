using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemButton : MonoBehaviour
{
    /// <summary> �ٴڿ� ������ ������ ����</summary>
    public Define.Item item = Define.Item.Fruit;
    /// <summary> ������ ������ ����</summary>
    public int amount = 1;

    /// <summary> ������ ȹ�� ��ư �޼���</summary>
    public void GetItem()
    {
        if (GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<Inventory>().
                      GetItem(item, amount)==0) //����ǰâ�� ���鼭 �κ��丮�� �о�ֱ� �õ�
        {  //�������� ����ǰâ�� �о�ִµ� ����������
            Destroy(gameObject.transform.parent.gameObject);//�ֿ� �Ծ����Ƿ� �ٴڿ� ������ ������ ����
        }
        else
        {
            Destroy(gameObject.transform.parent.gameObject);//�ֿ� �Ա� ���������� �ٴڿ� ������ ������ ����
        }
    }
}
