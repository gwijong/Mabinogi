using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemButton : MonoBehaviour
{
    /// <summary> �ٴڿ� ������ ������ ����</summary>
    public Define.Item Item = Define.Item.Fruit;
    /// <summary> ������ ������ ����</summary>
    public int amount = 1;

    /// <summary> ������ ȹ�� ��ư �޼���</summary>
    public void GetItem()
    {
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 6; x++)
            {
                if (GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<Inventory>().
                    PutItem(Vector2Int.zero + new Vector2Int (x,y) , Item, amount)) //����ǰâ�� ���鼭 �κ��丮�� �о�ֱ� �õ�
                {  //�������� ����ǰâ�� �о�ִµ� ����������
                    Destroy(gameObject.transform.parent.gameObject);//�ֿ� �Ծ����Ƿ� �ٴڿ� ������ ������ ����
                    return;//�ݺ��� Ż��
                }
            }
        }      
    }
}
