using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> �� ���� ������ �� </summary>
public class CellInfo  
{
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

    /// <summary> ������ Ÿ�� ��ȯ </summary>
    public Define.Item GetItemType()
    {
        return itemType;
    }

    /// <summary> ������ ���� </summary>
    public void SetItem(Define.Item wantItem)
    {

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

    void Start()
    {
        
        infoArray = new CellInfo[height, width]; //2���� �迭�� ũ�⸦ ����ǰâ ũ��� ����

        for (int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                GameObject currentCell = Instantiate(cell) ; //���� �� �����
                currentCell.transform.SetParent(parent.transform); //���� ���� �θ� ������Ʈ�� �ڽ� ������Ʈ�� ����

                infoArray[y, x] = new CellInfo(); //Ŭ���� CellInfo �ν��Ͻ� �����

                infoArray[y, x].amountText = currentCell.GetComponentInChildren<Text>();//������ �ؽ�Ʈ ������Ʈ ��������
                infoArray[y, x].buttonImage = currentCell.GetComponent<Image>(); //������ ��ư �̹��� ������Ʈ ��������
                infoArray[y, x].itemImage = currentCell.transform.GetChild(0).GetComponent<Image>();//������ ������ �̹��� ������Ʈ ��������

                infoArray[y, x].buttonImage.color = new Color(0.6f, 0.6f, 0.6f);//��ư�̹��� ���� ȸ������ �ٲ�
                infoArray[y, x].itemImage.color = new Color(0.0f, 0.0f, 1f);//������ �̹��� ���� �Ķ������� �ٲ�                

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
            infoArray[overedCellLocation.y, overedCellLocation.x].buttonImage.color = new Color(0.6f, 0.6f, 0.6f);
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

            //int overlap = 0;
            //Debug.Log(CanPlace(overedCellLocation, new Vector2Int(2, 3), out overlap));//2,3�� �������� �� �������� üũ
        };

        //CursorPickItem();
        if (Input.GetMouseButtonDown(0))
        {
            PutItem(overedCellLocation,new Vector2Int(2,3));
        }
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
                if(infoArray[position.y + y, position.x + x].GetItemType() != Define.Item.None)
                {
                    ++OverlapTime;
                    result = false;
                };
            };
        };
        return result;
    }






    public GameObject holdingItemCell;
    public void CursorPickItem()
    {
        holdingItemCell.transform.position = Input.mousePosition;
    }
    //������ ����
    void PutItem(Vector2Int overedCellLocation, Vector2Int size)
    {
        int overlap;
        if (CanPlace(overedCellLocation, size, out overlap) == true)
        {
            for(int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    infoArray[overedCellLocation.y + y, overedCellLocation.x + x].itemImage.color = new Color(1.0f, 1.0f, 1f);
                }
            }         
        }
    }
}


/*
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