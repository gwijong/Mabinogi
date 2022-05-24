using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary> ��ȭ�� �ʿ��� ���� ��ҵ� ���� Ŭ���� </summary>
public class NPC : Character
{
    /// <summary> ��ȭ ó�� ���� �� �������� ��ȭ </summary>
    [SerializeField] Dialog[] appearanceDialogArray;
    /// <summary> ���� ������ ��ȭ</summary>
    [SerializeField] Dialog[] mainDialogArray;
    /// <summary> �����ø ��ȭ </summary>
    [SerializeField] Dialog[] noteDialogArray;
    /// <summary> ���� ��ȭ </summary>
    [SerializeField] Dialog[] shopDialogArray;
    /// <summary> �������� �̾߱�(�����ø) ��ȭ </summary>
    [SerializeField] Dialog[] personalStoryDialogArray;
    /// <summary> ��ó�� �ҹ�(�����ø) ��ȭ </summary>
    [SerializeField] Dialog[] rumorsNearbyDialogArray;
    /// <summary> �Ƹ�����Ʈ�� ���Ͽ�(�����ø) ��ȭ </summary>
    [SerializeField] Dialog[] partTimeJobDialogArray;
    /// <summary> ��ų�� ���Ͽ�(�����ø) ��ȭ </summary>
    [SerializeField] Dialog[] skillDialogArray;
    /// <summary> �ۺ� ��ȭ </summary>
    [SerializeField] Dialog[] farewellDialogArray;
    /// <summary> NPC �ʻ��ι����� </summary>
    public Sprite portrait;


    /// <summary> ��ȭ ó�� ���� �� �������� ��ȭ </summary>
    public Dialog AppearanceDialog { get; private set; }
    /// <summary> ���� ������ ��ȭ </summary>
    public Dialog MainDialog { get; private set; }
    /// <summary> �����ø ��ȭ </summary>
    public Dialog NoteDialog { get; private set; }
    /// <summary> ���� ��ȭ </summary>
    public Dialog ShopDialog { get; private set; }
    /// <summary> ���� ��ȭ </summary>

    /// <summary> �������� �̾߱�(�����ø) ��ȭ </summary>
    public Dialog PersonalStoryDialog { get; private set; }
    /// <summary> ��ó�� �ҹ�(�����ø) ��ȭ </summary>
    public Dialog RumorsNearbyDialog { get; private set; }
    /// <summary> �Ƹ�����Ʈ�� ���Ͽ�(�����ø) ��ȭ</summary>
    public Dialog PartTimeJobDialog { get; private set; }
    /// <summary> ��ų�� ���Ͽ�(�����ø) ��ȭ </summary>
    public Dialog SkillDialog { get; private set; }
    /// <summary> �ۺ� ��ȭ </summary>
    public Dialog FarewellDialog { get; private set; }

    public override Define.InteractType Interact(Interactable other)
    {
        return Define.InteractType.Talk; //��ȭ ����
    }

    private void Start()
    {
        AppearanceDialog = Dialog.CreateDialogList(portrait, characterName, appearanceDialogArray);//���� ���� ���
        MainDialog = Dialog.CreateDialogList(portrait, characterName, mainDialogArray); //���� ������ ���
        NoteDialog = Dialog.CreateDialogList(portrait, characterName, noteDialogArray); //�����ø ���
        ShopDialog = Dialog.CreateDialogList(portrait, characterName, shopDialogArray); //���� ȣ�� ���

        PersonalStoryDialog = Dialog.CreateDialogList(portrait, characterName, personalStoryDialogArray); //�������� �̾߱�
        RumorsNearbyDialog = Dialog.CreateDialogList(portrait, characterName, rumorsNearbyDialogArray);  //��ó�� �ҹ�
        PartTimeJobDialog = Dialog.CreateDialogList(portrait, characterName, partTimeJobDialogArray);  //�Ƹ�����Ʈ�� ����
        SkillDialog = Dialog.CreateDialogList(portrait, characterName, skillDialogArray); //��ų�� ����
        FarewellDialog = Dialog.CreateDialogList(portrait, characterName, farewellDialogArray); //�ۺ� �λ�
    }

    /// <summary> npc�鸶�� �������� �̾߱� ��ó�� �ҹ� ���� ������ �ִ°� wantText�� ����ġ ������ ���θ��� ���̾�α� �Ѱ� �� </summary>
    public virtual Dialog NoteTalk(string wantText) 
    {
        Dialog dialog;
        switch (wantText)
        {
            default:
                dialog = PersonalStoryDialog;
                break;
            case "�������� �̾߱�":
                dialog = PersonalStoryDialog;
                break;
            case "��ó�� �ҹ�":
                dialog = RumorsNearbyDialog;
                break;
            case "��ų�� ���Ͽ�":
                dialog = SkillDialog;
                break;
            case "�Ƹ�����Ʈ�� ���Ͽ�":
                dialog = PartTimeJobDialog;
                break;
        }
        return dialog;
    }
}
[System.Serializable]
/// <summary> ��ȭ�ϴ� ��� ���� �ϳ� </summary>
public class Dialog
{
    /// <summary> NPC �ʻ��ι����� </summary>
    public Sprite portrait;
    /// <summary> NPC �̸�</summary>
    public string npcName;
    /// <summary> ���� ��� �ؽ�Ʈ </summary>
    public string currentText;
    /// <summary> ���� ��ȭ ���� </summary>
    public Dialog next;
    /// <summary> ��ư �������� </summary>
    public DialogButtonInfo[] buttonArray;
    /// <summary> �ʻ��ι����� Ȱ��ȭ ���� </summary>
    public bool portraitActive;
    /// <summary> ��ȭ�ϴ� ��� ���� �ϳ� �� </summary>
    public Dialog(string wantCurrentText, Sprite wantPortrait = null, string wantNpcName = null,  DialogButtonInfo[] wantButtonArray = null, Dialog wantNext = null)
    {
        currentText = wantCurrentText; //���ϴ� ���
        portrait = wantPortrait; //NPC ��������Ʈ
        npcName = wantNpcName; //NPC �̸�
        buttonArray = wantButtonArray; //��ư ��������
        next = wantNext; //���� ��ȭ
    }

    /// <summary> ���̾�α� �迭 ���� </summary>
    public static Dialog CreateDialogList(Sprite wantPortrait, string wantNpcName, Dialog[] dialogArray)
    {
        if(dialogArray.Length <= 0) // dialogArray �迭�� ���̰� 0�̸� ����
        {
            return null;
        }

        int i = 0;
        for (; i< dialogArray.Length -1; i++)  // dialogArray �迭�� ����-1 ��ŭ �ݺ�
        {
            if(dialogArray[i].portrait == null) //�ʻ��ι������� ���������
            {
                dialogArray[i].portrait = wantPortrait; //�⺻ �ʻ��ι����� ����
            }
            if (dialogArray[i].npcName == null || dialogArray[i].npcName == "") //NPC�̸��� ���������
            {
                dialogArray[i].npcName = wantNpcName; //�⺻ NPC �̸� ����
            }
            dialogArray[i].next = dialogArray[i + 1];//���� ��ȭ������ ���� ��ȭ���� ����;
        }

        //dialogArray�� ������ ��° ���̾�α׿��� �⺻���� ����. �������̹Ƿ� next�� ����
        if (dialogArray[i].portrait == null)
        {
            dialogArray[i].portrait = wantPortrait;
        }
        if (dialogArray[i].npcName == null || dialogArray[i].npcName == "")
        {
            dialogArray[i].npcName = wantNpcName;
        }
        return dialogArray[0];//ù��° ��ȭ ��ȯ
    }
}

/// <summary> ��ư ���� </summary>
[System.Serializable] //�ش� Ŭ������ �ν����� â�� �����
public class DialogButtonInfo
{
    /// <summary> ��ȭ�߿� ��ư �ؽ�Ʈ�� ǥ�õ� ��ư �̸� </summary>
    public string buttonName;
    /// <summary> ��ư ������ ���� �Ѿ�� ���ϴ� ��ư Ÿ�� </summary>
    public Define.TalkButtonType type;
}

