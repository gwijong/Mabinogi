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
        GameObject dropitem = null; //���� ������
        switch (item)
        {
            case Define.Item.None: //���� ������ Ÿ���� none�̸� ���� �������� �����Ƿ� Ż��
                return;
            case Define.Item.Fruit:
                dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/Fruit")); //���� ������ ����
                break;
            case Define.Item.Wool:
                dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/Wool")); //���� ������ ����
                break;
            case Define.Item.Egg:
                dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/Egg")); //�ް� ������ ����
                break;
            case Define.Item.Firewood:
                dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/Firewood")); //���� ������ ����
                break;
            case Define.Item.LifePotion:
                dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/LifePotion")); //����� ���� ������ ����
                break;
            case Define.Item.ManaPotion:
                dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/ManaPotion")); //���� ���� ������ ����
                break;
            case Define.Item.Bottle:
                dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/Bottle")); //�� ������ ����
                break;
            case Define.Item.BottleWater:
                dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/BottleWater")); //���� ������ ����
                break;
        }
        dropitem.GetComponent<ItemInpo>().amount = mouseAmount; //�ٴڿ� ������ �������� ������ ���콺�� ���� �ִ� �������� �����̴�.
        dropitem.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 3, 0);//�÷��̾� ��ǥ���� y�� 3 ���̿��� ����
    }
}
