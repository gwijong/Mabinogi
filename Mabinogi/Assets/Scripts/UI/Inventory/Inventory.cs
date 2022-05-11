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
    public Text amountText = null;
    /// <summary> �κ��丮 �ȿ��� �� ��ġ </summary>
    Vector2Int location;
    /// <summary> ������ â ĭ�� ��� ���� </summary>
    static Color normalColor = new Color(0.9f, 0.9f, 0.9f);
    /// <summary> ���� �������� ������� �� ��ο� �÷� </summary>
    static Color filledColor = new Color(0.7f, 0.7f, 0.7f);
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
    public void SetItem(Define.Item wantItem, int wantAmount)
    {
        Vector2Int itemSize = wantItem.GetSize();
        itemType = wantItem;
        itemImage.sprite = wantItem.GetItemImage();
        if (itemImage.sprite == null)
        {
            itemImage.color = Color.clear;
        }
        else
        {
            itemImage.color = Color.white;
        }
        itemImage.transform.localScale = new Vector3(itemSize.x, itemSize.y, 1);
        CalculateColor();
        SetAmount(wantAmount);
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
        if ((root == null || root == this) && itemType == Define.Item.None == true)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    /// <summary> ������ 0�̸� "" ó���� </summary>
    public void SetAmount(int wantAmount)
    {
        amount = wantAmount;
        amountText.text = wantAmount > 0 ? wantAmount.ToString() : "";
    }

    /// <summary> ������ ���ؼ� ���� �� ���� </summary>
    public int AddAmount(int wantAmount)
    {
        amount += wantAmount;  //��Ʈ �������� ������ �ݴ� ������ ���� ����                                                    
        int overAmount = Mathf.Max(0, amount - itemType.GetMaxStack()); //������ ��ġ�� �ʰ�ġ ���
        amount -= overAmount;
        SetAmount(amount); //���콺 ������ �ؽ�Ʈ ����

        return overAmount;
    }

    //������ �Ҹ� �� ���� �޼���
    /// <summary> ���� �������� ���ϴ� ������ŭ �� </summary>
    public int SubAmount(int wantAmount)
    {
        int result = Mathf.Min(wantAmount, amount);//���� ������ �ִ� �ͺ��� �ʰ��ؼ� �� ���� ����

        amount -= result;
        SetAmount(amount); //���콺 ������ �ؽ�Ʈ ����

        return result;
    }

    /// <summary> ĭ�� ���. root �� �� �ڽ� ���� �ٲٰ� �������� none���� �ٲ� </summary>
    public void Clear()
    {
        root = this;
        SetItem(Define.Item.None,0);
    }

    
}

public class Inventory : MonoBehaviour
{
    /// <summary> ����ǰâ ���� </summary>
    public int width;
    /// <summary> ����ǰâ ���� </summary>
    public int height;
    /// <summary> ������â ĭ���� ���� ������</summary>
    public RectTransform cellAnchor;
    /// <summary> ������â �� ĭ ������</summary>
    public GameObject cell;
    /// <summary> ���콺 Ŀ�� ����ٴϴ� ĭ ������</summary>
    public GameObject mouseCell;

    /// <summary> ���콺 Ŀ�� ����ٴϴ� ������ ���� UI</summary>
    public GameObject information;
    /// <summary> ������ ���� UI</summary>
    GameObject inpo;

    /// <summary> ������ ��� �������� ������ </summary>
    public GameObject useUI;
    /// <summary> ������ ��� �������� �ν��Ͻ�</summary>
    GameObject use;

    /// <summary> �κ��丮 â UI �̹���</summary>
    public GameObject parent;
    /// <summary> ���콺�� �÷��� �� ��ġ</summary>
    Vector2Int overedCellLocation = new Vector2Int(-1, -1);
    /// <summary> [?,?] ĭ�� ����</summary>
    CellInfo[,] infoArray;
    /// <summary> ���콺�� �����ִ� ������</summary>
    public static CellInfo mouseItem { get; private set; }
    /// <summary> ���콺�� �����ִ� ������ ��ǥ </summary>
    public static RectTransform mouseItemPos;


    //ItemData bottle = Resources.Load<ItemData>("Data/ItemData/Bottle");

    void Start()
    {
        if (mouseItem == null) //���콺�� �����ִ� �������� ������ �Ʒ����� �Ҵ�����
        {
            GameObject currentCell = Instantiate(mouseCell); //���콺 ��� ����ٴϴ� �� �� ����
            currentCell.transform.SetParent(GameObject.FindGameObjectWithTag("Inventory").transform); //���콺 ����ٴϴ� ���� �θ� �κ��丮�� ����
            currentCell.tag = "MouseCell"; //���콺 ���� �±׸� ����
            mouseItemPos = currentCell.GetComponent<RectTransform>(); //���콺 ���� RectTransform ����
            mouseItem = new CellInfo(new Vector2Int(0,0)); //���콺������ �ν��Ͻ� ����
            mouseItem.amountText = currentCell.GetComponentInChildren<Text>();//������ �ؽ�Ʈ ������Ʈ ��������
            mouseItem.buttonImage = currentCell.GetComponent<Image>(); //������ ��ư �̹��� ������Ʈ ��������
            mouseItem.itemImage = currentCell.transform.GetChild(0).GetComponent<Image>();//������ ������ �̹��� ������Ʈ ��������
            mouseItem.itemImage.rectTransform.pivot = Vector2.zero; // ���콺����ٴϴ� ������ �̹��� �߽����� 0,0�� ����
            mouseItem.SetItem(Define.Item.None,0);//���콺�� ��� �ִ� �������� ����ְ� 0���� ����
            mouseItem.buttonImage.enabled = false; //��ư �̹���(������ ĭ) ��
            currentCell.GetComponent<Button>().enabled = false; //��ư ������Ʈ ��
        }

        if(inpo == null)
        {
            inpo = Instantiate(information); //���콺 ��� ����ٴϴ� ����â ����
            inpo.transform.SetParent(GameObject.FindGameObjectWithTag("Inventory").transform); //���콺 ����ٴϴ� ����â�� �θ� �κ��丮�� ����
            Image inpoImage = inpo.GetComponent<Image>();//����â �̹��� ������Ʈ ��������
            inpoImage.rectTransform.pivot = new Vector2(0,1); // ����â �߽����� 0,1�� ����
            inpo.SetActive(false);
        }

        if(use == null)
        {
            use = Instantiate(useUI);
            use.transform.SetParent(GameObject.FindGameObjectWithTag("Inventory").transform);
            Image useImage = use.GetComponent<Image>();//������ ���â �̹��� ������Ʈ ��������
            useImage.rectTransform.pivot = new Vector2(0, 1); // ����â �߽����� 0,1�� ����
            use.SetActive(false);
        }

        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;

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

                infoArray[y, x].CalculateColor();//ĭ�� ��������� �� ����, ĭ�� �������� ��ο� ����
                
                RectTransform childRect = currentCell.GetComponent<RectTransform>(); //���� ���� RectTransform ������
                Vector2 pos = cellAnchor.anchoredPosition + new Vector2((48 * x), (-48 * y));//�� ������ ��ġ�� ���� ���������� 48�ȼ� �������� ��ġ��
                childRect.anchoredPosition = pos;//���� ���� ��Ŀ �������� ���� ���������� 48 * x, -48 * y �������� ��ġ��
            }
        }
        foreach (CellInfo current in infoArray) //��� ������ ĭ�� ��ȸ
        {
            current.itemImage.transform.SetParent(parent.transform); //������ �̹����� �κ��丮 â�� ���� ������Ʈ�� ����
            current.amountText.transform.SetParent(parent.transform);//������ �ؽ�Ʈ�� �κ��丮 â�� ���� ������Ʈ�� ����
            current.SetItem(Define.Item.None,0); //�κ��丮 â ĭ���� �����
        }
        PutItem(Vector2Int.one, Define.Item.Wool, 3); //�κ��丮 ����� �⺻ ������1
        PutItem(Vector2Int.zero, Define.Item.Fruit, 1); //�κ��丮 ����� �⺻ ������2
        PutItem(Vector2Int.one * 3, Define.Item.Firewood, 1); //�κ��丮 ����� �⺻ ������2
    }

    private void OnUpdate()
    {
        if(mouseItemPos == null) //���콺 ����ٴϴ� ���� ��ǥ�� null�̸� ����
        {
            return;
        }
        mouseItemPos.position = Input.mousePosition; //���콺 ����ٴϴ� ���� ��ǥ�� ���콺 ��ǥ�� ��� ���� ����
        Vector3 mousePosition = Input.mousePosition; //���콺 ��ǥ
        mousePosition -= cellAnchor.position; //���콺 ��ǥ���� ������ ���� �������� ����
        mousePosition.y *= -1;//y���� ������ �ް����� ������ ���� ���ؼ� ����� �ٲ���

        inpo.GetComponent<RectTransform>().position = Input.mousePosition; //������ ����â�� ���콺 ��ǥ�� �׻� ����ٴϰ� ��

        if (overedCellLocation.x >= 0 && overedCellLocation.y >= 0) //���콺 Ŀ���� 0,0 �̻��̸�
        {
            //�ش� ��ǥ ���� ��ư �̹����� �÷��� ȸ������ �ٲ�
            //infoArray[overedCellLocation.y, overedCellLocation.x].CalculateColor();
            CellHighlight(overedCellLocation, false); //���� ���콺 Ŀ���� �ִ� �������� ��ư ������ �⺻������ �ٲ���
        }

        if (mousePosition.x < 0 || mousePosition.x > width * 48 // ���콺��ǥ�� x�� 0���� �۰ų� ���콺 ��ǥ�� ����ǰâ ĭ ���̸� �Ѿ�ų�
            ||mousePosition.y < 0 || mousePosition.y > height * 48) // ���콺��ǥ�� y�� 0���� �۰ų� ���콺 ��ǥ�� ����ǰâ ĭ ���̸� �Ѿ�ų�
        {
            inpo.SetActive(false); //����ǰâ�� ������Ƿ� ������ ����â ����

            //���콺 Ŀ���� ����ǰâ�� ��� ��Ȳ���� ���콺 ��Ŭ���� ������
            if (Input.GetMouseButtonDown(0) && mouseItem!=null && mouseItem.GetItemType() != Define.Item.None) 
            {
                DropItem(); //������ ������
            }
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
            if (Input.GetMouseButtonDown(1))//���콺 ��Ŭ���ϸ�
            {
                inpo.SetActive(false);
                UseUI(overedCellLocation, true);
            }   
            if (Input.GetMouseButtonDown(0)) //���콺 ��Ŭ���ϸ�
            {
                UseUI(overedCellLocation, false);
                LeftClick(overedCellLocation); // ���콺 ��Ŭ�� �޼��� ����
            }
            if(use.activeSelf != true)//���â�� ����������
            {
                ItemInpo(overedCellLocation, true);//���� UI ����
            }
        };
    }

    void UseUI(Vector2Int position, bool active)
    {
        use.transform.position = Input.mousePosition; //��� UI�� ���콺 Ŀ�� ��ǥ�� �̵�
        CellInfo rootCellInfo = CheckItemRoot(position);//���콺 Ŀ���� ��ġ�� ���� �������� ��Ʈ�� �������� �õ�
        if (active && (rootCellInfo.GetItemType() != Define.Item.None)) //��Ʈ �������� �����ϸ�
        {
            use.SetActive(true); //���â�� Ȱ��ȭ�Ѵ�
        }
        else//��Ʈ �������� �������� ������
        {
            use.SetActive(false);//���â ��Ȱ��ȭ
        }
    }

    /// <summary> ������ ������ </summary>
    void DropItem()
    {
        GameManager.itemManager.DropItem(mouseItem.GetItemType(), mouseItem.amount);
        mouseItem.Clear();//���콺 ������ �����
    }

    /// <summary> �ش� ��ġ�� �������� �о� �ִ°� �������� üũ </summary>
    bool CanPlace(Vector2Int position, Vector2Int itemSize, out int OverlapTime)    //�� ��ǥ, ������ũ��,������ ������ , ������ ��ģ Ƚ��
    {
        bool result = true; //true�� ���� �� ����, false �� ���� �� ����
        OverlapTime = 0;

        List<CellInfo> rootList = new List<CellInfo>(); //��ģ ��Ʈ ���� Ȯ��

        if (infoArray[position.y, position.x] == null) return false; //��ǥ���� null�� ��� ����
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
                    CellInfo currentRoot = infoArray[position.y + y, position.x + x].GetRoot(); //���� ��Ʈ�� ������
                    if (!rootList.Contains(currentRoot))//��Ʈ ����Ʈ�� ���� ��Ʈ�� ���� ���� ������
                    {
                        rootList.Add(currentRoot);//��Ʈ ����Ʈ�� ���� ��Ʈ�� �߰��Ѵ�
                    };
                    result = false;
                };
            };
        };
        OverlapTime = rootList.Count; //��ģ Ƚ���� ��Ʈ ����Ʈ�� �����̴�.
        return result; //���� ������ ������ ���� �� ������ true ��ȯ
    }

    /// <summary> ���콺 ��Ŭ�� �� ������ ��ġ��, ������ �ű�� ���� </summary>
    void LeftClick(Vector2Int pos)
    {
        //���콺 Ŭ���� ��ǥ�� null�̰ų� ����ǰâ�� ������ ��� ���
        if (pos == null || pos.x < 0 || pos.y < 0 || pos.x >= width || pos.y >= height)
        {
            return; //Ż��
        }
        int overlapTime = 0; //��ģ Ƚ��
        int currentAmount = 0; //���� ����
        Define.Item currentItem = Define.Item.None; //���� �������� �ϴ� �����

        CellInfo currentCell = CheckItemRoot(pos); //���� ���� ���콺 ��ǥ�� ��Ʈ ����

        if (mouseItem.GetItemType() == Define.Item.None)// ���콺�� ���� �ִ� �������� ���� ���
        {
            currentItem = SubItem(pos, out currentAmount); //���콺 ��ǥ ���� �����۵� ���ش�
            mouseItem.SetItem(currentItem, currentAmount); //���콺�� ���� �ִ� �������� ������ �� ��ŭ �������ش�
        }
        else //���콺�� ������ ���� �ִ� ���
        {
            if(CanPlace(pos, mouseItem.GetItemType().GetSize(), out overlapTime))//���� ������ �ִ� ���
            {
                TryRemovePlace(pos,currentCell.GetLocation() , out currentItem, out currentAmount); //���� �õ�
            }
            else
            {
                if(overlapTime <= 1)
                {
                    if(currentCell.GetItemType() == mouseItem.GetItemType()) //������ ������ ������ �����ֱ� �õ�
                    {                       
                        int maxStack = currentCell.GetItemType().GetMaxStack(); //���� ������ ĭ�� ������ ������ �ִ� ��ġ�� ���� ������                     
                        if(currentCell.amount >= maxStack) // ������ â �������� �ִ�ġ�� �� ���ִ� ���
                        {
                            //mouseItem.amount = mouseItem.amount ^ currentCell.amount;
                            //currentCell.amount = mouseItem.amount ^ currentCell.amount;
                            //mouseItem.amount = mouseItem.amount ^ currentCell.amount;
                            int temp = mouseItem.amount; //���콺 ������ ���� �ӽ� ����
                            mouseItem.amount = currentCell.amount; //���콺 ������ ������ ����ǰâ ������ ������ ����
                            currentCell.amount = temp; //����ǰâ ������ ������ ���콺 ������ ������ ����
                            mouseItem.SetAmount(mouseItem.amount); //���콺 �����ִ� ������ �ؽ�Ʈ ����
                            currentCell.SetAmount(currentCell.amount);//�κ��丮 ������ ĭ �ؽ�Ʈ ����

                        }
                        else // ������ â �������� �� ���ִ� ���
                        {
                            currentCell.amount += mouseItem.amount; //�ϴ� ������ â �����ۿ� ���콺 ������ ���� ����
                            // ������   ���� �� ū ��      ���̳ʽ��� �Ǹ�(�ȳѾ�����) �׳��� ū 0�� �ȴ�
                            int overAmount = Mathf.Max(0, currentCell.amount - maxStack); //�ִ�ġ �ʰ����� ������ 0, �ʰ��ϸ� �ʰ��� ���ڸ�ŭ overAmount ����
                            mouseItem.amount = overAmount; //�ʰ�ġ��ŭ ���콺�� ��� �ִ´�
                            currentCell.amount -= overAmount; //�ʰ�ġ��ŭ ������ â �������� ������ ���ش�
                            mouseItem.SetAmount(mouseItem.amount); //���콺 ������ �ؽ�Ʈ ����
                            currentCell.SetAmount(currentCell.amount); //�κ��丮 ������ �ؽ�Ʈ ����
                            
                        }
                    }
                    else //������ ������ �ٸ��� ���� �ִ� �����۰� ����ǰâ �������� ��������
                    {
                        currentCell = CheckItemRoot(pos, mouseItem.GetItemType().GetSize());
                        if(currentCell != null) //�������� ������
                        {
                            TryRemovePlace(pos, currentCell.GetLocation(), out currentItem, out currentAmount); //���� �õ�
                        };
                    }
                }
            }
        }
        if(mouseItem.amount <= 0) //���콺�� ��� �ִ� ������ ������ 0 �����̸�
        {
            mouseItem.SetItem(Define.Item.None, 0); //���콺�� ����ִ� ������ �����
        }
    }

    /// <summary> ������ ���� �õ� </summary>
    bool TryRemovePlace(Vector2Int pos, Vector2Int currentPos, out Define.Item currentItem, out int currentAmount)
    {
        currentItem = SubItem(currentPos, out currentAmount);//���� ��ǥ���� ���� ������ŭ ���� ���� �����ۿ� ����
        bool result = PutItem(pos, mouseItem.GetItemType(), mouseItem.amount) > 0;//���콺�� ���� �ִ� �������� pos ��ǥ�� �о�ֱ� �õ�
        if (result == false) //�о�ֱⰡ �����ߴٸ�
        {
            PutItem(currentPos, currentItem, currentAmount);//���� ��ǥ�� ���� �������� ���� ������ŭ �о����
        }
        else //�о�ֱⰡ �����ߴٸ�
        {
            mouseItem.SetItem(currentItem, currentAmount); //���콺�� ���� �ִ� �������� ���� ������, ���� ������ ����
        };
        return result; //�о�ֱ� ��� ����
    }

    /// <summary> ������ �о�ֱ� �õ� </summary>
    int PutItem(Vector2Int position, Define.Item item, int amount)
    {
        amount = Mathf.Min(amount, item.GetMaxStack());

        Vector2Int size = item.GetSize();//�ش� �������� ������ ��������
        int overlap;//������ Ƚ��
        if (CanPlace(position, size, out overlap) == true) //�ش� ��ǥ�� �ش� �������� �������� �о� ����  �� ������
        {
            infoArray[position.y, position.x].SetItem(item, amount); //y,x ��ǥ�� ������ �о����
            for(int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    if (x == 0 && y == 0) continue; //0,0�� ��Ʈ�̹Ƿ� �ѱ�

                    //ù��° ĭ ��Ʈ�� ������ ������ ĭ ��Ʈ���� y,x�� �ٶ󺸰� ����
                    infoArray[position.y + y, position.x + x].SetRoot(infoArray[position.y, position.x]);
                }
            }
            return amount;
        };
        return 0;
    }

    /// <summary> ����ǰâ���� ���� ������ ã�Ƽ� ����Ʈ�� ����</summary>
    public List<CellInfo> FindItemList(Define.Item item) //������ ã����
    {
        List<CellInfo> result = new List<CellInfo>(); //�ش� �������� ������ �ִ� ��� �� ����
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                CellInfo root = infoArray[y, x].GetRoot();
                if (root.GetItemType() == item)
                {
                    if (!result.Contains(root))
                    {
                        result.Add(root);
                    }
                }
            }
        }
        return result;
    }
    
    /// <summary> ����Ʈ���� ������ ������ ���� ��� ���ؼ� ����</summary>
    public int GetItemAmount(Define.Item item)
    {
        int result = 0;
        foreach(CellInfo current in FindItemList(item))
        {
            result += current.amount;
        }

        return result;
    }

    public int GetItem(Define.Item item, int amount)  
    {
        List<CellInfo> sameList = FindItemList(item);
        for(int i = 0; i < sameList.Count; i++)
        {
            amount = sameList[i].AddAmount(amount); //�ְ� ������

            if (amount <= 0) //���� �������� 0���� ������
            {
                return 0; //Ʈ�� ��ȯ
            }
        }
        if (amount >= 1)//������ �ߺ��� �����۵� ��ġ�� ���� ���� �������� ������
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //�о���� ������ ������ŭ amount ����
                    amount -= PutItem(Vector2Int.zero + new Vector2Int(x, y), item, amount); 
                    
                    if (amount <= 0) return 0;//���� �������� 0�� ���ų� ������ Ʈ�� ��ȯ                       
                }
            }
        }
        return amount; //�� �о�־����� ��ȯ
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

    /// <summary> �ش� ��ǥ���� ������ �����ŭ�� ������ �������� �ִ��� üũ�ؼ� �������� ������ �ش� ������ ���� ������</summary>
    CellInfo CheckItemRoot(Vector2Int position, Vector2Int size)
    {
        //����ǰâ ���� �Ѿ�� null ����
        if (position.x < 0 || position.y < 0 || position.x + size.x > width || position.y + size.y > height) return null;

        for (int x = 0; x < size.x; x++)
        {   //������ �����ŭ ������ ĭ ��ȸ
            for(int y = 0; y < size.y; y++) 
            {
                if(infoArray[position.y + y, position.x + x].GetItemType() != Define.Item.None) //�� �ȿ� �������� ������
                {
                    return infoArray[position.y + y, position.x + x]; //�� ���� ��ȯ
                };
            };
        };

        return infoArray[position.y,position.x]; //�� �� ���� ��ȯ
    }

    /// <summary> ��Ʈ�� ���� ���� ���� ���̶���Ʈ ���� ���ִ� �޼���</summary>
    void CellHighlight(Vector2Int position, bool isHighright)
    {
        CellInfo rootCellInfo = CheckItemRoot(position);
        Define.Item CellItem = CheckItem(position); //�ش� ��ǥ�� ������ �޾ƿ�
        Vector2Int size = CellItem.GetSize(); // ������ ������ �޾ƿ�
        if (isHighright)//���̶���Ʈ ����� �ϸ�
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
        else//���̶���Ʈ ������� �ϸ�
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
    /// <summary> ���� UI Ȱ��ȭ �޼���</summary>
    void ItemInpo(Vector2Int position, bool active)
    {
        CellInfo rootCellInfo = CheckItemRoot(position);//���콺 Ŀ���� ��ġ�� ���� �������� ��Ʈ�� �������� �õ�
        if(active && (rootCellInfo.GetItemType() != Define.Item.None)) //��Ʈ �������� �����ϸ�
        {
            ItemData[] itemData = GameManager.itemManager.data;
            inpo.SetActive(true); //����â�� Ȱ��ȭ�Ѵ�
            Text[] text = inpo.GetComponentsInChildren<Text>(); //����â�� �ڽ� ������Ʈ�� �ؽ�Ʈ ������Ʈ���� �����´�
            for(int i = 0; i< itemData.Length+1; i++) //��ũ���ͺ������Ʈ ���̸�ŭ �ݺ�
            {
               if(i == (int)rootCellInfo.GetItemType())
                {
                    text[0].text = itemData[i-1].ItemName; //0�� �ؽ�Ʈ������Ʈ�� �ؽ�Ʈ�� �������� ������ �̸����� �ٲ۴�
                    text[1].text = itemData[i-1].Description;//1�� �ؽ�Ʈ������Ʈ�� �ؽ�Ʈ�� �������� ������ �������� �ٲ۴�
                }
            }
            
        }
        else//��Ʈ �������� �������� ������
        {
            inpo.SetActive(false);//����â ��Ȱ��ȭ
        }
        if(active == false)
        {
            inpo.SetActive(false); //����â ��Ȱ��ȭ
        }
    }
}


