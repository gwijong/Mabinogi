using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemButton : MonoBehaviour
{
    /// <summary> ������ ȹ�� ��ư �޼���</summary>
    public void GetItem()
    {
        GetComponentInParent<ItemInpo>().GetItem();
    }
}
