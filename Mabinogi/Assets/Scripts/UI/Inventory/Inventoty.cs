using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> �� ���� ������ �� </summary>
public class CellInfo  
{
    /// <summary> �������� ã������ ��Ʈ�� ���� ã���ش� ��Ʈ�� null�̰ų� �� �ڽ� ���̸� �̳��� ���������̴� </summary>
    CellInfo root = null; 
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
    /// <summary> ������ â ĭ�� ��� ���� </summary>
    static Color normalColor = new Color(0.9f,0.9f,0.9f);
    /// <summary> ���� �������� ������� �� ��ο� �÷� </summary>
    static Color filledColor = new Color(0.7f,0.7f,0.7f);
    /// <summary> ���콺 Ŀ���� ��ġ�� ĭ ��� �����ϴ� �÷� </summary>
    public static Color highlightColor = new Color(1, 1, 1);

    /// <summary> ����ǰâ �ȿ����� ������ ��ǥ�� �� �ڽ��� ��Ʈ�� �����ϴ� ������ </summary>
    public CellInfo(Vector2Int wantLocation)
    {
        location = wantLocation; //�κ��丮 �ȿ����� �� ��ġ ����
        root = this; //��Ʈ�� �� �ڽ� ���̴�
    }

    /// <summary> Define.Item ������ Ÿ�� ��ȯ </summary>
    public Define.Item GetItemType()
    {
        return itemType;
    }

    /// <summary> ���� ������ ����� ĭ�� ���� ���� </summary>
    public void SetItem(Define.Item wantItem)
    {
        itemType = wantItem;
        CalculateColor();
    }


    /// <summary> ��Ʈ ����� ĭ�� ���� ���� </summary>
    public void SetRoot(CellInfo setRoot)
    {
        root = setRoot;
        CalculateColor();
    }

    /// <summary> ��ư �̹��� ���ϴ� ���� ���� normalColor, filledColor, highlightColor </summary>
    public void SetColor(Color wantColor)
    {
        buttonImage.color = wantColor;
    }

    /// <summary> ĭ�� ��������� normalColor, ĭ�� �������� filledColor </summary>
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

    /// <summary> �κ��丮 �ȿ����� ���� ��ġ ��ȯ </summary>
    public Vector2Int GetLocation()
    {
        return location;
    }

    /// <summary> ĭ�� ����ִ��� üũ. ��Ʈ�� null�̰ų� �� �ڽ� ���̸鼭 ������Ÿ���� none�̸� true </summary>
    public bool IsEmpty()
    {
        if((root == null || root == this) && itemType == Define.Item.None == true)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    /// <summary> ĭ�� ���. root �� �� �ڽ� ���� �ٲٰ� �������� none���� �ٲ� </summary>
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
            //infoArray[overedCellLocation.y, overedCellLocation.x].CalculateColor();
            CellHighlight(overedCellLocation, false); //���� ���콺 Ŀ���� �ִ� �������� ��ư ������ �⺻������ �ٲ���
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

            
            CellHighlight(overedCellLocation,true); //���콺 Ŀ���� �ִ� �������� ��ư ������ ������ �������� �ٲ���

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

    /// <summary> �ش� ��ġ�� �������� �о� �ִ°� �������� üũ </summary>
    bool CanPlace(Vector2Int position, Vector2Int itemSize, out int OverlapTime)    //�� ��ǥ, ������ũ��,������ ������ , ������ ��ģ Ƚ��
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
                    ++OverlapTime; //������ ��ģ Ƚ�� ����
                    result = false;
                };
            };
        };
        return result;
    }

    /// <summary> ������ �о�ֱ� �õ� </summary>
    void PutItem(Vector2Int position, Define.Item item, int amount)
    {
        Vector2Int size = item.GetSize();//�ش� �������� ������ ��������
        int overlap;//������ Ƚ��
        if (CanPlace(position, size, out overlap) == true) //�ش� ��ǥ�� �ش� �������� �������� �о� ����  �� ������
        {
            infoArray[position.y, position.x].SetItem(item); //y,x ��ǥ�� ������ �о����
            for(int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    if (x == 0 && y == 0) continue;

                    //ù��° ĭ ��Ʈ�� ������ ������ ĭ ��Ʈ���� y,x�� �ٶ󺸰� ����
                    infoArray[position.y + y, position.x + x].SetRoot(infoArray[position.y, position.x]);
                }
            }
        }
    }
    /// <summary> �ش�� ���� ������ ���� ����</summary>
    Define.Item SubItem(Vector2Int position, out int amount) //�� ��ǥ, ������ ����
    {
        CellInfo selectedInfo = CheckItemRoot(position);//�ش� ��ǥ ���� ��Ʈ CellInfo ������
        Vector2Int rootLocation = selectedInfo.GetLocation(); //��Ʈ ��ǥ�� �ش� ��ǥ ���� ��Ʈ�� ��ǥ�̴�

        amount = selectedInfo.amount; //������ ����
        Define.Item result = selectedInfo.GetItemType();//�ش� ���� ������ Ÿ�� ������


        if (result == Define.Item.None) //������ ������ Ÿ���� None�̸�
        {
            return result; // None ��ȯ �ƹ��͵� ����
        }
        else //������ ������ Ÿ���� ������
        {     
            Vector2Int size = result.GetSize(); //�ش� ������ ����� Ȯ��޼��忡�� ������
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    infoArray[rootLocation.y + y, rootLocation.x + x].Clear(); //������ ������ ������ŭ �����
                }
            }            
        }
        return result; //�ش� ���� ������ Ÿ�� ��ȯ
    }

    /// <summary> �ش� ��ǥ ���� ������ Ÿ�� ��ȯ</summary>
    Define.Item CheckItem(Vector2Int position)
    {
        return CheckItemRoot(position).GetItemType();
    }

    /// <summary> �ش�� ĭ�� ��Ʈ�� üũ�ؼ� �� �ڽ� CellInfo�� ������ CellInfo��ȯ</summary>
    CellInfo CheckItemRoot(Vector2Int position)
    {
        return infoArray[position.y, position.x].GetRoot(); //�� �ڽ� ��ȯ�ϰų�, ������ ��ȯ�ϰų�
    }

    /// <summary> ��Ʈ�� ���� ���� ���� ���̶���Ʈ ���� ���ִ� �޼���</summary>
    void CellHighlight(Vector2Int position, bool isHighright)
    {
        CellInfo rootCellInfo = CheckItemRoot(position);
        Define.Item CellItem = CheckItem(position); //�ش� ��ǥ�� ������ �޾ƿ�
        Vector2Int size = CellItem.GetSize(); // ������ ������ �޾ƿ�
        if (isHighright)
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    //������ �����ŭ ���̶���Ʈ ����
                    infoArray[rootCellInfo.GetLocation().y + y, rootCellInfo.GetLocation().x + x].SetColor(CellInfo.highlightColor); 
                }
            }
        }
        else
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    //������ �����ŭ ���� �������� ������
                    infoArray[rootCellInfo.GetLocation().y + y, rootCellInfo.GetLocation().x + x].CalculateColor();
                }
            }
        }
    }   
}


/*
 * 
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
 * 
 * 
 * ���콺�� �÷��� �ִ� ĭ�� ��Ʈ Ȯ��, 
 * ��Ʈ�� ������ Ÿ���� ������ �޾ƿ�, 
 * ������ ������ŭ ���̶���Ʈ ����,
 * ���� ���콺 ���� ������ �־��� ���콺�� üũ�ؾ��� �׷��� ���� ����� ������, 
 * ������ ���� ���콺 ��ġ �ٸ��� ������ Ǯ�������
 */

