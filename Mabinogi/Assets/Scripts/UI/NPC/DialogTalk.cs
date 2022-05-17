using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogTalk : MonoBehaviour
{
    public Character currentPlayer;
    public NPC currentNPC;
    Dialog currentDialog;

    /// <summary> NPC �̸� �ؽ�Ʈ </summary>
    public Text textName;
    /// <summary> ��ȭ���� �ؽ�Ʈ </summary>
    public Text textScript;
    /// <summary> �ʻ��ι����� </summary>
    public Image portrait;
    /// <summary> ��ȭâ ���� �ϸ� </summary>
    public GameObject dark;
    /// <summary> ��ȭâ ���׸��� �ܰ��� </summary>
    public GameObject outline;
    /// <summary> ���� ��ư�� 4�� </summary>
    public GameObject[] buttonBackgrounds;
    /// <summary> �����ø </summary>
    public GameObject note;
    /// <summary> ĳ���� ��ų, ���� UI </summary>
    public GameObject UI_Canvas;
    /// <summary> ���� �������� �ڷ�ƾ </summary>
    IEnumerator Cor;

    private void Update()
    {
        if (Input.anyKeyDown && outline.activeInHierarchy)
        {
            DialogNext();
        }
    }

    public void SetDialog(Dialog wantDialog)
    {
        currentDialog = wantDialog;
        portrait.sprite = currentDialog.portrait;
        textName.text = currentDialog.npcName;
        CoroutineStart(TextOutput(currentDialog));
    }

    public void DialogNext()
    {
        if(currentDialog.next == null)
        {
            return;
        }
        else
        {
            SetDialog(currentDialog.next);
        }
    }
    /// <summary> �ؽ�Ʈ �ѱ��ھ� ��� �ڷ�ƾ </summary>
    IEnumerator TextOutput(Dialog wantDialog)
    {
        SelectButtonOff();
        textScript.text = "";
        portrait.sprite = wantDialog.portrait;
        portrait.gameObject.SetActive(wantDialog.portraitActive);
        int i = 0;
        while (textScript.text.Length< wantDialog.currentText.Length)
        {
            textScript.text += wantDialog.currentText[i++];
            yield return new WaitForSeconds(0.05f);          
        }
        if(wantDialog.buttonArray.Length <= 0)
        {
            SetButton(0, "����", Define.TalkButtonType.Next);
        }
        else
        {
            for(int j = 0; j< wantDialog.buttonArray.Length; j++)
            {
                SetButton(j, wantDialog.buttonArray[j].buttonName, wantDialog.buttonArray[j].type);
            }
        }
    }
    
    public void SetButton(int current, string wantText, Define.TalkButtonType wantType)
    {
        buttonBackgrounds[current].SetActive(true);
        buttonBackgrounds[current].GetComponentInChildren<Text>().text = wantText;
        buttonBackgrounds[current].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction targetFuntion;
        switch (wantType)
        {
            default:
                targetFuntion = EndTalkButton;
                break;
            case Define.TalkButtonType.Next:
                targetFuntion = DialogNext;
                break;
            case Define.TalkButtonType.Note:
                targetFuntion = NoteButton;
                break;
            case Define.TalkButtonType.Personal:
                targetFuntion = PersonalStoryButton;
                break;
            case Define.TalkButtonType.Shop:
                targetFuntion = ShopButton;
                break;
            case Define.TalkButtonType.EndTalk:
                targetFuntion = EndTalkButton;
                break;
            case Define.TalkButtonType.ToMain:
                targetFuntion = MainButton;
                break;
        }

        buttonBackgrounds[current].GetComponentInChildren<Button>().onClick.AddListener(targetFuntion);
    }
    
    public void SetTarget(Character wantPlayer, NPC wantNPC)
    {
        currentPlayer = wantPlayer;
        currentNPC = wantNPC;
        OpenTalkCanvas();
        SetDialog(currentNPC.AppearanceDialog);
    }

    /// <summary> ��ȭ ������ ��ư </summary>
    public void EndTalkButton()
    {
        SelectButtonOff();
        CloseTalkCanvas();
        UI_Canvas.SetActive(true);

    }

    /// <summary> ��ȭ ĵ������ ������ҵ��� ���� �� </summary>
    public void CloseTalkCanvas()
    {
        portrait.gameObject.SetActive(false);
        note.SetActive(false);
        dark.SetActive(false);
        outline.SetActive(false);
    }
    public void OpenTalkCanvas()
    {
        dark.SetActive(true);
        outline.SetActive(true);
        UI_Canvas.SetActive(false);
    }
    /// <summary> ���� ��ȭ�������� ���� ��ư </summary>
    public void ShopButton()
    {
        SetDialog(currentNPC.ShopDialog);
    }

    public void MainButton()
    {
        SetDialog(currentNPC.MainDialog);
    }

    /// <summary> �����ø ��ȭ�������� ���� </summary>
    public void NoteButton()
    {
        note.SetActive(true);
        SetDialog(currentNPC.NoteDialog);
    }

    /// <summary> �����ø�� �������� �̾߱� ���� </summary>
    public void PersonalStoryButton()
    {
        SetDialog(currentNPC.PersonalStoryDialog);
    }

    /// <summary> �����ø�� ��ó�� �ҹ� ���� </summary>
    public void RumorsNearbyButton()
    {
        SetDialog(currentNPC.RumorsNearbyDialog);
    }


    /// <summary> �����ø�� ��ų�� ���Ͽ� ���� </summary>
    public void SkillButton()
    {
        SetDialog(currentNPC.SkillDialog);
    }


    /// <summary> �����ø�� �Ƹ�����Ʈ�� ���Ͽ� ���� </summary>
    public void PartTimeJobButton()
    {
        SetDialog(currentNPC.PartTimeJobDialog);
    }


    /// <summary> �ڷ�ƾ ���� �޼��� </summary>
    void CoroutineStart(IEnumerator cor)
    {
        if (Cor != null)
        {
            StopCoroutine(Cor);
        }
        Cor = cor;
        StartCoroutine(Cor);
    }


    /// <summary> ���������� ��ư ���� �� </summary>
    void SelectButtonOff()
    {
        for(int i =0; i< buttonBackgrounds.Length; i++)
        {
            buttonBackgrounds[i].SetActive(false);
        }
    }
}

