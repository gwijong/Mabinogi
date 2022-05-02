using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellInfo  //�� ���� ������ ��
{
    Define.Item itemType = Define.Item.None;
    public int amount; //������ ����
    public Image buttonImage = null;
    public Image itemImage = null;
    public Text amountText= null;

    public Define.Item GetItem()
    {
        return itemType;
    }
    public void SetItem(Define.Item wantItem)
    {

    }
}

public class Inventoty : MonoBehaviour
{
    public int width;
    public int height;
    public RectTransform cellAnchor;
    public GameObject cell;
    public GameObject parent;

    Vector2Int overedCellLocation = new Vector2Int(-1, -1);//���콺�� �÷��� �� ��ġ

    CellInfo[,] infoArray; //ĭ ����

    void Start()
    {
        
        infoArray = new CellInfo[height, width];

        for (int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                GameObject currentCell = Instantiate(cell) ; //���� �� �����
                infoArray[i, j] = new CellInfo(); //Ŭ���� CellInfo �ν��Ͻ� �����

                infoArray[i, j].amountText = currentCell.GetComponentInChildren<Text>();//������ �ؽ�Ʈ ������Ʈ ��������
                infoArray[i, j].buttonImage = currentCell.GetComponent<Image>(); //������ ��ư �̹��� ������Ʈ ��������
                infoArray[i, j].itemImage = currentCell.transform.GetChild(0).GetComponent<Image>();//������ ������ �̹��� ������Ʈ ��������

                infoArray[i, j].buttonImage.color = new Color(0.6f, 0.6f, 0.6f);//��ư�̹��� ���� ȸ������ �ٲ�
                infoArray[i, j].itemImage.color = new Color(0.0f, 0.0f, 1f);//������ �̹��� ���� �Ķ������� �ٲ�
                RectTransform childRect = currentCell.GetComponent<RectTransform>();
                currentCell.transform.SetParent(parent.transform);
                Vector2 pos = cellAnchor.anchoredPosition + new Vector2((48 * j), (-48 * i));
                childRect.anchoredPosition = pos;
            }
        }
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition -= cellAnchor.position;
        mousePosition.y *= -1;

        if (overedCellLocation.x >= 0 && overedCellLocation.y >= 0)
        {
            infoArray[overedCellLocation.y, overedCellLocation.x].buttonImage.color = new Color(0.6f, 0.6f, 0.6f);
        }

        if (mousePosition.x < 0 || mousePosition.x > width * 48
            ||mousePosition.y < 0 || mousePosition.y > height * 48)
        {
            //0��ĭ�� ���ݾ� -1��ĭ�� ���� ���콺�� �÷��� ���� ���ٴ� ����
            overedCellLocation = Vector2Int.one * -1;
        }
        else
        {
            overedCellLocation.x = (int)mousePosition.x / 48;
            overedCellLocation.y = (int)mousePosition.y / 48;
            infoArray[overedCellLocation.y, overedCellLocation.x].buttonImage.color = new Color(1f, 1f, 1f);
            int overlap = 0;
            Debug.Log(CanPlace(overedCellLocation, new Vector2Int(2, 3), out overlap));
        };
    }

    bool CanPlace(Vector2Int position, Vector2Int itemSize, out int OverlapTime)
    {
        bool result = true;
        OverlapTime = 0;

        if (infoArray[overedCellLocation.y, overedCellLocation.x] == null) return false;
        Vector2Int rightBottom = position + itemSize - Vector2Int.one;
        if(position.x < 0 || position.y < 0 || rightBottom.x>= width||rightBottom.y >= height) //��ǥ�� 0���� �۾Ƽ� ����ǰ â ���� ����ų�, ������ ������ �Ʒ� ��ǥ�� ����ǰâ ũ�⺸�� ũ��
        {
            return false;
        }
        for(int y = 0; y< itemSize.y; y++)
        {
            for(int x = 0; x < itemSize.x; x++)
            {
                if(infoArray[position.y + y, position.x + x].GetItem() != Define.Item.None)
                {
                    ++OverlapTime;
                    result = false;
                };
            };
        };

        return result;
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