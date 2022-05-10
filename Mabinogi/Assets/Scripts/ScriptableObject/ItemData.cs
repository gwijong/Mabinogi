using UnityEngine;

//[CreateAssetMenu(menuName = "�޴� ���",fileName = "�⺻ ���ϸ�"), order = �޴��󿡼� ����]
[CreateAssetMenu(menuName = "Scriptable/Item/Itemdata", fileName = "ItemData")]
public class ItemData : ScriptableObject
{
    [Tooltip("������ �̸�")]
    [SerializeField]
    private string itemName = "���� ����";
    /// <summary> ������ �̸� </summary>
    public string ItemName { get { return itemName; } }

    [Tooltip("���� ����")]
    [SerializeField]
    [Range(1, 10000)]
    private int purchasePrice = 100;
    /// <summary> ���� ���� </summary>
    public int PurchasePrice { get { return purchasePrice; } }

    [Tooltip("�Ǹ� ����")]
    [SerializeField]
    [Range(1, 10000)]
    private int salePrice = 10;
    /// <summary> �Ǹ� ���� </summary>
    public int SalePrice { get { return salePrice; } }

    [Tooltip("������ ĭ �ʺ�")]
    [SerializeField]
    [Range(1, 4)]
    private int width = 1;
    /// <summary> ������ ĭ �ʺ� </summary>����
    public int Width { get { return width; } }

    [Tooltip("������ ĭ ����")]
    [SerializeField]
    [Range(1, 4)]
    private int height = 1;
    /// <summary> ������ ĭ ���� </summary>
    public int Height { get { return height; } }

    [Tooltip("������ ����")]
    [SerializeField]
    private string description = "����ǰâ ���� �Ϲ� ������";
    /// <summary> ������ ���� </summary>
    public string Description { get { return description; } }
}
