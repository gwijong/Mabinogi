using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ȭ�� ��� UI ��ư ��� </summary>
public class UIUsePotion : MonoBehaviour
{
    /// <summary> ����� ���� ��� </summary>
    public void HPUse()
    {
        Inventory.EatItem(Define.Item.LifePotion);
    }

    /// <summary> ���� ���� ��� </summary>
    public void MPUse()
    {
        Inventory.EatItem(Define.Item.ManaPotion);
    }
}
