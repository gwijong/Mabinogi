using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Inventory
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GetItem(Define.Item.Wool, 10); //���� �κ��丮 ����� �⺻ ������
    }
}
