using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary> ������ ���� </summary>
public class CreateItem : MonoBehaviour
{
    /// <summary> ������ ���� </summary>
    public Define.Item item;
    /// <summary> ������ ���� </summary>
    public int amount;
    void Start()
    {
        GameObject currentItem = item.MakePrefab(); //item�� �´� ������ currentItem ����
        currentItem.transform.SetParent(transform); //currentItem��  �� ������Ʈ�� �ڽ����� ����
        currentItem.transform.localPosition = Vector3.zero; //currentItem�� ������ǥ�� �ʱ�ȭ
        Canvas canvas = GetComponentInChildren<Canvas>();  //�� ������Ʈ�� �ڽĵ� �߿� ĵ���� ã�Ƽ� �Ҵ�
        canvas.transform.SetParent(currentItem.transform);  //ĵ������ ����ִ� ������Ʈ�� currentItem�� �ڽ����� ����
        currentItem.GetComponent<ItemInpo>().amount = amount; //currentItem�� ������ ������ amount�� ���� 
        GetComponentInChildren<Text>().text = item.GetItemData().ItemName; //�ؽ�Ʈ ������Ʈ�� �ؽ�Ʈ�� �����۵������� �������̸����� ����
    }
}
