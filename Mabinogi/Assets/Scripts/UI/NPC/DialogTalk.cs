using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

/// <summary> NPC ��ȭ ĵ������ �޸� </summary>
public class DialogTalk : MonoBehaviour
{
    /// <summary> ��ȭ���� �÷��̾� </summary>
    public Character currentPlayer;
    /// <summary> ��ȭ���� NPC </summary>
    public NPC currentNPC;
    /// <summary> ��ȭ �� ��� ������ �����̳� </summary>
    Dialog currentDialog;

    /// <summary> NPC �̸� �ؽ�Ʈ </summary>
    public Text textName;
    /// <summary> ��ȭ���� UI �ؽ�Ʈ </summary>
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
    /// <summary> ���� �κ��丮 </summary>
    InvenOpen inven;

    public string nextScene;

    private void Start()
    {
        inven = GameObject.FindGameObjectWithTag("Inventory").gameObject.GetComponent<InvenOpen>();
    }

    private void Update()
    {
        if (Input.anyKeyDown && outline.activeInHierarchy) //��ȭâ�� Ȱ��ȭ �� �߿� �ƹ� Ű �Է��� ������
        {
            if (currentDialog == null)
            {
                return;
            }
            if(textScript.text.Length < currentDialog.currentText.Length)
            {
                textScript.text = currentDialog.currentText;
            }
            else
            {
                DialogNext();// ���� �ؽ�Ʈ�� �ѱ�
            }
        }
    }

    /// <summary> ���̾�α� ����(��������Ʈ,���)���� ��� ��� �ڷ�ƾ ���� </summary>
    public void SetDialog(Dialog wantDialog)
    {
        currentDialog = wantDialog;
        portrait.sprite = currentDialog.portrait;
        textName.text = currentDialog.npcName;
        CoroutineStart(TextOutput(currentDialog));
    }

    /// <summary> ���� ���̾�α׿��� ���� ���̾�α׷� �ѱ� </summary>
    public void DialogNext()
    {
        if(currentDialog.next == null) //���� ���̾�αװ� ��������� ����
        {
            return;
        }
        else
        {
            SetDialog(currentDialog.next); //���� ���̾�α׷� ����
        }
    }
    /// <summary> �ؽ�Ʈ �ѱ��ھ� ��� �ڷ�ƾ </summary>
    IEnumerator TextOutput(Dialog wantDialog)
    {
        SelectButtonOff();//�ϴ� ��ư �� �� ��
        textScript.text = ""; //��ȭ �ؽ�Ʈ �����
        portrait.sprite = wantDialog.portrait; //NPC �ʻ��ι����� ����
        portrait.gameObject.SetActive(wantDialog.portraitActive); //�ʻ��ι����� Ȱ��ȭ ����
        int i = 0;
        wantDialog.currentText = wantDialog.currentText.Replace("@", "" + System.Environment.NewLine);//����� ������ �ٹٲ�
        while (textScript.text.Length< wantDialog.currentText.Length) //�ؽ�Ʈ�� �� ��µǾ�����
        {
            textScript.text += wantDialog.currentText[i++]; //�ؽ�Ʈ ���
            yield return new WaitForSeconds(0.05f);          
        }
        if(wantDialog.buttonArray.Length <= 0) //��ư�� ���� ������ �ʾ�����
        {
            SetButton(0, "����", Define.TalkButtonType.Next); //���� ��ȭ�� �ѱ�� �⺻ ��ư ����
        }
        else//���� �غ��� ��ư�� ������
        {
            for(int j = 0; j< wantDialog.buttonArray.Length; j++)//��ư�� ���ڸ�ŭ �ݺ�
            {
                //j��° ��ư�� ��ư �̸�, ��ư Ÿ�� ����
                SetButton(j, wantDialog.buttonArray[j].buttonName, wantDialog.buttonArray[j].type);
            }
        }
    }

    /// <summary> �ش� ��ư �ؽ�Ʈ�� ���� �� ����Ǵ� �̺�Ʈ ���� </summary>
    public void SetButton(int current, string wantText, Define.TalkButtonType wantType)
    {
        buttonBackgrounds[current].SetActive(true); //current��° ��ư Ȱ��ȭ
        buttonBackgrounds[current].GetComponentInChildren<Text>().text = wantText; //��ư ���� �ؽ�Ʈ ����
        buttonBackgrounds[current].GetComponentInChildren<Button>().onClick.RemoveAllListeners(); //��ư�� ���� �̺�Ʈ�� ����
        UnityEngine.Events.UnityAction targetFuntion;//���� ��ư�� ���� ���� �޼��带 ��� �׼�
        switch (wantType)
        {
            default:
                targetFuntion = EndTalkButton; //��ȭ ������
                break;
            case Define.TalkButtonType.Next:
                targetFuntion = DialogNext;  //���� ��ȭ�� �ѱ��
                break;
            case Define.TalkButtonType.Note:
                targetFuntion = NoteButton;  //�����ø ��ȭ
                break;
            case Define.TalkButtonType.Shop:
                targetFuntion = ShopButton;  //���� ��ȭ
                break;
            case Define.TalkButtonType.EndTalk:
                targetFuntion = EndTalkButton;  //��ȭâ �ݱ�
                break;
            case Define.TalkButtonType.ToMain: // ���� ��ȭ��
                targetFuntion = MainButton;  //��ȭ, �ŷ� ���� �� �ִ� ó�� ��������
                break;
            case Define.TalkButtonType.Farewell: // ���� ��ȭ��
                targetFuntion = FarewellButton;  //��ȭ, �ŷ� ���� �� �ִ� ó�� ��������
                break;
        }

        buttonBackgrounds[current].GetComponentInChildren<Button>().onClick.AddListener(targetFuntion); //��ư�� targetFuntion �̺�Ʈ �߰�
    }

    /// <summary> �÷��̾� ��ũ��Ʈ���� �����ϴ�, ��ȭ�� �� �÷��̾�� NPC�� �����ϴ� �޼��� </summary>
    public void SetTarget(Character wantPlayer, NPC wantNPC)
    {
        currentPlayer = wantPlayer; //�÷��̾� ����
        currentNPC = wantNPC; // NPC ����
        OpenTalkCanvas();//��ȭâ ����
        SetDialog(currentNPC.AppearanceDialog);//NPC ���� ��ȭ ����
        GameManager.soundManager.PlayBgmPlayer(currentNPC.npc);//NPC �������;
        GameManager.soundManager.effectPlayer.volume = 0.15f;
    }

    /// <summary> ��ȭ ������ ��ư </summary>
    public void EndTalkButton()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//��ư �ٿ� ȿ����
        GameManager.soundManager.PlayBgmPlayer((Define.Scene)UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        GameManager.soundManager.effectPlayer.volume = 0.5f;
        SelectButtonOff(); //��� ��ư ����
        CloseTalkCanvas(); //��ȭ ĵ���� ����
        UI_Canvas.SetActive(true); //���� UI ����
        inven.StoreClose();
        inven.Close();
        if (FindObjectOfType<Soulstream>() != null)
        {
            StartCoroutine(FindObjectOfType<Soulstream>().NaoDisappear());
        }      
    }

    /// <summary> ��ȭ ĵ������ ������ҵ��� ���� �� </summary>
    public void CloseTalkCanvas()
    {
        portrait.gameObject.SetActive(false); //�ʻ��ι����� ��
        note.SetActive(false); //�����ø ��
        dark.SetActive(false); //�ϸ� ��
        outline.SetActive(false); //��ȭâ ��
    }
    /// <summary> ��ȭ ĵ���� ���� </summary>
    public void OpenTalkCanvas()
    {
        dark.SetActive(true); //�ϸ� ��
        outline.SetActive(true); //��ȭâ ��
        UI_Canvas.SetActive(false);  //���� UI ��
    }
    /// <summary> ���� ��ȭ�������� ���� ��ư </summary>
    public void ShopButton()
    {
        if (inven.isOpen == false)
        {
            inven.Open();
        }
        if (inven.isStoreOpen == false)
        {
            GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//��ư �ٿ� ȿ����
            inven.StoreOpen();
        }
        SetDialog(currentNPC.ShopDialog);
    }

    /// <summary> ��ȭ, �ŷ� ���� �� �ִ� ó�� ������ </summary>
    public void MainButton()
    {
        SetDialog(currentNPC.MainDialog);
    }

    /// <summary> �����ø ��ȭ�������� ���� </summary>
    public void NoteButton()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//��ư �ٿ� ȿ����
        note.SetActive(true); //�����ø ����
        SetDialog(currentNPC.NoteDialog);
    }

    /// <summary> �����ø�� ���� ��ư�� ������ </summary>
    public void NoteKeywordButton()
    {                                                               //��� ����Ű���� ��ư�� �ؽ�Ʈ ������ 
        SetDialog(currentNPC.NoteTalk(EventSystem.current.currentSelectedGameObject.GetComponent<Text>().text));
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//��ư �ٿ� ȿ����
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

    /// <summary> �ۺ��λ� </summary>
    public void FarewellButton()
    {
        note.SetActive(false);
        SetDialog(currentNPC.FarewellDialog);
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//��ư �ٿ� ȿ����
    }

    /// <summary> ���������� ��ư�� ���� �� </summary>
    void SelectButtonOff()
    {
        for(int i =0; i< buttonBackgrounds.Length; i++)
        {
            buttonBackgrounds[i].SetActive(false);
        }
    }
}

