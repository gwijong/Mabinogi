using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> �� ���� ������ �� </summary>
public class CellInfo  
{
    CellInfo root = null; //�������� ã������ ��Ʈ�� ���� ã���ش� ��Ʈ�� null�̸� �̳��� ���������̴�
    /// <summary> ������ Ÿ�� </summary>
    Define.Item itemType = Define.Item.None;
    /// <summary> ������ ���� </summary>
    public int amount;
    /// <summary> ������ ����� �Ǵ� ��ư �̹��� </summary>
    public Image buttonImage = null;
    /// <summary> ������ �̹��� </summary>
    public Image itemImage = null;
    /// <summary> ������ ���� ǥ���ϴ� �ؽ�Ʈ </summary>
    public Text amountText= null;
    /// <summary> �κ��丮 �ȿ��� �� ��ġ </summary>
    Vector2Int location;

    static Color normalColor = new Color(0.9f,0.9f,0.9f);
    static Color filledColor = new Color(0.7f,0.7f,0.7f);//���� ������� �� �÷�

    /// <summary> �����̼� �����ϴ� ������ </summary>
    public CellInfo(Vector2Int wantLocation)
    {
        location = wantLocation;
        root = this;
    }

    /// <summary> ������ Ÿ�� ��ȯ </summary>
    public Define.Item GetItemType()
    {
        return itemType;
    }

    /// <summary> ������ ���� </summary>
    public void SetItem(Define.Item wantItem)
    {
        itemType = wantItem;
        CalculateColor();
    }


    /// <summary> ��Ʈ �� </summary>
    public void SetRoot(CellInfo setRoot)
    {
        root = setRoot;
        CalculateColor();
    }

    public void SetColor(Color wantColor)
    {
        buttonImage.color = wantColor;
    }
    
    public void CalculateColor()
    {
        if (IsEmpty())
        {
            SetColor(normalColor);
        }
        else
        {
            SetColor(filledColor);
        };
    }

    /// <summary> ��Ʈ ��ȯ </summary>
    public CellInfo GetRoot()
    {
        return root;
    }

    public Vector2Int GetLocation()
    {
        return location;
    }

    /// <summary> ĭ�� ����ִ��� </summary>
    public bool IsEmpty()
    {
        return (root == null || root == this) && itemType == Define.Item.None; 
    }

    public void Clear()
    {
        root = this;
        SetItem(Define.Item.None);
    }

    
}

public class Inventoty : MonoBehaviour
{
    /// <summary> ����ǰâ ���� </summary>
    public int width;
    /// <summary> ����ǰâ ���� </summary>
    public int height;
    /// <summary> ������â ĭ���� ���� ������</summary>
    public RectTransform cellAnchor;
    /// <summary> ������â �� ĭ ������</summary>
    public GameObject cell;
    /// <summary> �κ��丮 â UI �̹���</summary>
    public GameObject parent;
    /// <summary> ���콺�� �÷��� �� ��ġ</summary>
    Vector2Int overedCellLocation = new Vector2Int(-1, -1);
    /// <summary> [?,?] ĭ�� ����</summary>
    CellInfo[,] infoArray;

    //ItemData bottle = Resources.Load<ItemData>("Data/ItemData/Bottle");

    void Start()
    {
        
        infoArray = new CellInfo[height, width]; //2���� �迭�� ũ�⸦ ����ǰâ ũ��� ����

        for (int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                GameObject currentCell = Instantiate(cell) ; //���� �� �����
                currentCell.transform.SetParent(parent.transform); //���� ���� �θ� ������Ʈ�� �ڽ� ������Ʈ�� ����

                infoArray[y, x] = new CellInfo(new Vector2Int(x,y)); //Ŭ���� CellInfo �ν��Ͻ� �����

                infoArray[y, x].amountText = currentCell.GetComponentInChildren<Text>();//������ �ؽ�Ʈ ������Ʈ ��������
                infoArray[y, x].buttonImage = currentCell.GetComponent<Image>(); //������ ��ư �̹��� ������Ʈ ��������
                infoArray[y, x].itemImage = currentCell.transform.GetChild(0).GetComponent<Image>();//������ ������ �̹��� ������Ʈ ��������

                infoArray[y, x].CalculateColor();
                
                RectTransform childRect = currentCell.GetComponent<RectTransform>(); //���� ���� RectTransform ������
                Vector2 pos = cellAnchor.anchoredPosition + new Vector2((48 * x), (-48 * y));//�� ������ ��ġ�� ���� ���������� 48�ȼ� �������� ��ġ��
                childRect.anchoredPosition = pos;//���� ���� ��Ŀ �������� ���� ���������� 48 * x, -48 * y �������� ��ġ��
            }
        }
    }

    private void Update()
    {
        /// <summary> ���콺 ��ǥ </summary>
        Vector3 mousePosition = Input.mousePosition;
        mousePosition -= cellAnchor.position; //���콺 ��ǥ���� ������ ���� �������� ����
        mousePosition.y *= -1;//y���� ������ �ް����� ������ ���� ���ؼ� ����� �ٲ���

        if (overedCellLocation.x >= 0 && overedCellLocation.y >= 0) //���콺 Ŀ���� 0,0 �̻��̸�
        {
            //�ش� ��ǥ ���� ��ư �̹����� �÷��� ȸ������ �ٲ�
            infoArray[overedCellLocation.y, overedCellLocation.x].CalculateColor();
        }

        if (mousePosition.x < 0 || mousePosition.x > width * 48 // ���콺��ǥ�� x�� 0���� �۰ų� ���콺 ��ǥ�� ����ǰâ ĭ ���̸� �Ѿ�ų�
            ||mousePosition.y < 0 || mousePosition.y > height * 48) // ���콺��ǥ�� y�� 0���� �۰ų� ���콺 ��ǥ�� ����ǰâ ĭ ���̸� �Ѿ�ų�
        {

            //overedCellLocation�� (-1,-1)�� �ٲ�
            overedCellLocation = Vector2Int.one * -1;// -1��ĭ�� �����Ƿ� ���콺�� �÷��� ���� ���ٴ� ����
        }
        else //���콺 Ŀ���� ����ǰ â �ȿ� �ִ� ���
        {
            overedCellLocation.x = (int)mousePosition.x / 48; //���콺 ��ǥ���� ĭ ������ 48�� ������ ������ ��ȯ
            overedCellLocation.y = (int)mousePosition.y / 48; //���콺 ��ǥ���� ĭ ������ 48�� ������ ������ ��ȯ

            //���콺 Ŀ���� �ִ� �������� ��ư ������ ������ �������� �ٲ���
            infoArray[overedCellLocation.y, overedCellLocation.x].buttonImage.color = new Color(1f, 1f, 1f); 

            int overlap = 0;
            int amount = 0;
            if (Input.GetMouseButtonDown(1))
                SubItem(overedCellLocation, out amount);
            if (Input.GetMouseButtonDown(0))
            {
                PutItem(overedCellLocation,Define.Item.Bottle, 1);
            }

            

        };

    }

    //��ǥ, ������ũ��,
    bool CanPlace(Vector2Int position, Vector2Int itemSize, out int OverlapTime)
    {
        bool result = true; //true�� ���� �� ����, false �� ���� �� ����
        OverlapTime = 0;

        if (infoArray[overedCellLocation.y, overedCellLocation.x] == null) return false; //��ǥ���� null�� ��� ����
        Vector2Int rightBottom = position + itemSize - Vector2Int.one; //�������� ������ �Ʒ� �𼭸� ��ǥ

        // ��ǥ�� 0���� �۾Ƽ� ����ǰ â ���� ����ų�, ������ ������ �Ʒ� �𼭸� ��ǥ�� ����ǰâ ũ�⺸�� �ʰ��Ǹ�
        if (position.x < 0 || position.y < 0 || rightBottom.x>= width||rightBottom.y >= height) 
        {
            return false;
        }
        for(int y = 0; y< itemSize.y; y++) //�������� yũ�� ��ŭ �ݺ�
        {
            for(int x = 0; x < itemSize.x; x++) //�������� xũ�� ��ŭ �ݺ�
            {
                //������ ũ�⸸ŭ�� ������ ���� �� ���°� �ƴ� ���
                if(infoArray[position.y + y, position.x + x].IsEmpty() == false)
                {
                    ++OverlapTime;
                    result = false;
                };
            };
        };
        return result;
    }
                          //�� ��ǥ
    void PutItem(Vector2Int position, Define.Item item, int amount)
    {
        Vector2Int size = item.GetSize();
        int overlap;
        if (CanPlace(position, size, out overlap) == true)
        {
            infoArray[position.y, position.x].SetItem(item);
            for(int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    if (x == 0 && y == 0) continue;
                    infoArray[position.y + y, position.x + x].SetRoot(infoArray[position.y, position.x]);
                }
            }
        }
    }
    /// <summary> �ش�� ���� ������ ����</summary>
    Define.Item SubItem(Vector2Int position, out int amount)
    {
        CellInfo selectedInfo = CheckItemRoot(position);
        Vector2Int rootLocation = selectedInfo.GetLocation();

        amount = selectedInfo.amount;
        Define.Item result = selectedInfo.GetItemType();


        if (result == Define.Item.None)
        {
            return result;
        }
        else
        {     
            Vector2Int size = result.GetSize();
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    infoArray[rootLocation.y + y, rootLocation.x + x].Clear();
                }
            }            
        }
        return result;
    }

    /// <summary> �ش�� ���� ������ ����</summary>
    Define.Item CheckItem(Vector2Int position)
    {
        return CheckItemRoot(position).GetItemType();
    }

    /// <summary> �ش�� ĭ�� ��Ʈ�� üũ�ϴ� ����</summary>
    CellInfo CheckItemRoot(Vector2Int position)
    {
        return infoArray[position.y, position.x].GetRoot(); //�� �ڽ� ��ȯ�ϰų�, ������ ��ȯ�ϰų�
    }
}


/*
 * �� ��ġ�� �������� �ִ��� ã�ƺ��� ��
 * � �������� �ִ��� üũ�غ���
 * �������� ������ ���ƺ��ߵ�
 * �������� ���� �� �� ��ġ(������)���� �ϳ��� ���� ������ ĭ�� �����ϴ� �ֵ��� �� �ڸ��� ����Ű�� �־�� ��
 * OE___
 * EE___
 * _____
 * _____
 * �������� ������ ���� ������ ���� Ȯ��
 * �� �� �̻� ��ġ�� �ƹ� �͵� ���� �ʱ�
 * �����Ϸ��� �� �� ���� ������ �������̸� ��ġ��
 * ���� ������ �������ε� ��ĥ �� �ִ� ���ں��� ������ ���� ���ڸ�ŭ�� ���콺���ٰ� ����
 */

//���콺�� �÷��� �ִ� ĭ�� ��Ʈ Ȯ��, ��Ʈ�� ������ Ÿ���� ������ �޾ƿ�, ������ ������ŭ ���̶���Ʈ ����,���� ���콺 ���� ������ �־��� ���콺�� üũ�ؾ��� �׷��� ���� ����� ������, ������ ���� ���콺 ��ġ �ٸ��� ������ Ǯ�������