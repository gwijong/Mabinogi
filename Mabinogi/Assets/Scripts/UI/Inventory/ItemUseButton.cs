using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseButton : MonoBehaviour
{
    /// <summary> ������ ��� ��ư </summary>
    public void Use()
    {
        FindObjectOfType<Inventory>().Use();
    }
    /// <summary> ������ ������ ��ư </summary>
    public void Divide()
    {
        FindObjectOfType<Inventory>().Divide();
    }
    /// <summary> ������ ������ ��ư </summary>
    public void Drop()
    {
        FindObjectOfType<Inventory>().Drop();
    }
}
