using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    /// <summary> ������ ������ ��ũ���ͺ� ������Ʈ </summary>
    public ItemData[] data;

    /// <summary> ������ ������ </summary>
    public void DropItem(Define.Item item, int mouseAmount)
    {
        GameObject dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/ItemPrefab"));//���� ������

        if (dropitem == null) return;

        dropitem.GetComponent<CreateItem>().amount = mouseAmount; //�ٴڿ� ������ �������� ������ ���콺�� ���� �ִ� �������� �����̴�.
        dropitem.GetComponent<CreateItem>().item = item;
        dropitem.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 3, 0);//�÷��̾� ��ǥ���� y�� 3 ���̿��� ����
    }
}
