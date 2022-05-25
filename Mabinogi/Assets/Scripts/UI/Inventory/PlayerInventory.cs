using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInventory : Inventory
{
    public Text goldText;

    protected override void Start()
    {
        base.Start();
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (owner == null || goldText == null)
        {
            return;
        }
        else
        {
            goldText.text = owner.gold + " G"; //��� ��ġ �ؽ�Ʈ�� ��� ������Ʈ ����
        }
    }
}
