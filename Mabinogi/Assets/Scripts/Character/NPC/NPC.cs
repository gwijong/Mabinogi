using System.Collections;
using System.Collections.Generic;
using UnityEngine;

delegate void DialogButtonFuntion(Character other);

public class NPC : Character
{
    [SerializeField] Dialog[] AppearanceDialogArray;
    [SerializeField] Dialog[] MainDialogArray;
    [SerializeField] Dialog[] PersonalDialogArray;
    [SerializeField] Dialog[] ShopDialogArray;
    public Sprite portrait;
    public Dialog Appearance { get; private set; }
    public Dialog MainDialog { get; private set; }
    public Dialog PersonalDialog { get; private set; }
    public Dialog ShopDialog { get; private set; }

    public override Define.InteractType Interact(Interactable other)
    {
        return Define.InteractType.Talk; //��ȭ ����
    }

    private void Start()
    {
        Appearance = Dialog.CreateDialogList(portrait, characterName, AppearanceDialogArray);
    }

    public virtual void PersonalTalk(string wantText) //npc�鸶�� �������� �̾߱� ��ó�� �ҹ� ���� ������ �ִ°� wantText�� ����ġ ������ ���θ��� ���̾�α� �Ѱ� ��
    {
        //
    }
}
[System.Serializable]
/// <summary> ��ȭ�ϴ� ��� ���� </summary>
public class Dialog
{
    public Sprite portrait;
    public string npcName;
    public string currentText;
    public Dialog next;
    public DialogButtonInfo[] buttonArray;
    public bool portraitActive;
    /// <summary> ��ȭ�ϴ� ��� ���� �� </summary>
    public Dialog(string wantCurrentText, Sprite wantPortrait = null, string wantNpcName = null,  DialogButtonInfo[] wantButtonArray = null, Dialog wantNext = null)
    {
        currentText = wantCurrentText;
        portrait = wantPortrait;
        npcName = wantNpcName;
        buttonArray = wantButtonArray;
        next = wantNext;
    }

    public static Dialog CreateDialogList(Sprite wantPortrait, string wantNpcName, Dialog[] dialogArray)
    {
        if(dialogArray.Length <= 0)
        {
            return null;
        }

        int i = 0;
        for (; i< dialogArray.Length -1; i++)
        {
            if(dialogArray[i].portrait == null)
            {
                dialogArray[i].portrait = wantPortrait;
            }
            if (dialogArray[i].npcName == null || dialogArray[i].npcName == "")
            {
                dialogArray[i].npcName = wantNpcName;
            }
            dialogArray[i].next = dialogArray[i + 1];
        }
        if (dialogArray[i].portrait == null)
        {
            dialogArray[i].portrait = wantPortrait;
        }
        if (dialogArray[i].npcName == null || dialogArray[i].npcName == "")
        {
            dialogArray[i].npcName = wantNpcName;
        }
        return dialogArray[0];
    }
}
[System.Serializable]
public class DialogButtonInfo
{
    public string buttonName;
    public Define.TalkButtonType type;
}

