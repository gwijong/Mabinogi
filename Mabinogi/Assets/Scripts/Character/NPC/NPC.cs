using System.Collections;
using System.Collections.Generic;
using UnityEngine;

delegate void DialogButtonFuntion(Character other);

public class NPC : Character
{
    [SerializeField] Dialog[] appearanceDialogArray;
    [SerializeField] Dialog[] mainDialogArray;
    [SerializeField] Dialog[] noteDialogArray;
    [SerializeField] Dialog[] shopDialogArray;
    [SerializeField] Dialog[] personalStoryDialogArray;
    [SerializeField] Dialog[] rumorsNearbyDialogArray;
    [SerializeField] Dialog[] partTimeJobDialogArray;
    [SerializeField] Dialog[] skillDialogArray;
    public Sprite portrait;
    public Dialog AppearanceDialog { get; private set; }
    public Dialog MainDialog { get; private set; }
    public Dialog NoteDialog { get; private set; }
    public Dialog ShopDialog { get; private set; }

    public Dialog PersonalStoryDialog { get; private set; }
    public Dialog RumorsNearbyDialog { get; private set; }
    public Dialog PartTimeJobDialog { get; private set; }
    public Dialog SkillDialog { get; private set; }

    public override Define.InteractType Interact(Interactable other)
    {
        return Define.InteractType.Talk; //��ȭ ����
    }

    private void Start()
    {
        AppearanceDialog = Dialog.CreateDialogList(portrait, characterName, appearanceDialogArray);
        MainDialog = Dialog.CreateDialogList(portrait, characterName, mainDialogArray);
        NoteDialog = Dialog.CreateDialogList(portrait, characterName, noteDialogArray);
        ShopDialog = Dialog.CreateDialogList(portrait, characterName, shopDialogArray);

        PersonalStoryDialog = Dialog.CreateDialogList(portrait, characterName, personalStoryDialogArray);
        RumorsNearbyDialog = Dialog.CreateDialogList(portrait, characterName, rumorsNearbyDialogArray);
        PartTimeJobDialog = Dialog.CreateDialogList(portrait, characterName, partTimeJobDialogArray);
        SkillDialog = Dialog.CreateDialogList(portrait, characterName, skillDialogArray);
    }

    public virtual void PersonalTalk(string wantText) //npc�鸶�� �������� �̾߱� ��ó�� �ҹ� ���� ������ �ִ°� wantText�� ����ġ ������ ���θ��� ���̾�α� �Ѱ� ��
    {

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

