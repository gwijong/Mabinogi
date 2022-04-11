using UnityEngine;

//[CreateAssetMenu(menuName = "�޴� ���",fileName = "�⺻ ���ϸ�"), order = �޴��󿡼� ����]
[CreateAssetMenu(menuName = "Scriptable/Item/Itemdata", fileName = "ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private int purchasePrice = 100;
    /// <summary> ���� ���� </summary>
    public int PurchasePrice { get { return purchasePrice; } }

    [SerializeField]
    private int salePrice = 10;
    /// <summary> �Ǹ� ���� </summary>
    public int SalePrice { get { return salePrice; } }

    [SerializeField]
    private int width = 1;
    /// <summary> ������ ĭ �ʺ� </summary>����
    public int Width { get { return width; } }

    [SerializeField]
    private int height = 1;
    /// <summary> ������ ĭ ���� </summary>
    public int Height { get { return height; } }

    [SerializeField]
    private string description = "����ǰâ ���� �Ϲ� ������";
    /// <summary> ������ ���� </summary>
    public string Description { get { return description; } }
}
