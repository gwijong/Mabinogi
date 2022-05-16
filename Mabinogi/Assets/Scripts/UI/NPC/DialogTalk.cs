using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogTalk : MonoBehaviour
{
    /// <summary> �� ó�� NPC ���� �����ϴ� ���� </summary>
    public string firstScript;
    /// <summary> ��ȭ, �ŷ� ���� ���� </summary>
    public string chooseSciript;
    /// <summary> �����ø ��ȭ���� </summary>
    public string noteScript;
    /// <summary> ���� ��ȭ���� </summary>
    public string shopScript;
    /// <summary> �������� �̾߱� ��ȭ���� </summary>
    public string[] personalstory;

    /// <summary> NPC �̸� �ؽ�Ʈ </summary>
    public Text name;
    /// <summary> ��ȭ���� �ؽ�Ʈ </summary>
    public Text textScript;
    /// <summary> �ʻ��ι����� </summary>
    public GameObject portrait;
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
    /// <summary> ��ȭ ���� ��Ȳ </summary>
    bool[] progress = { false, false, false, false};
    /// <summary> ���� �������� �ڷ�ƾ </summary>
    IEnumerator Cor;

    public void StartTalk()
    {
        FirstTyping();
    }

    private void Update()
    {
        if(progress[0] ==true && progress[1] == false && Input.anyKeyDown)
        {
            ChooseTyping();
        }
        if(progress[2] == true && Input.anyKeyDown)
        {
            progress[2] = false;
            PersonalStoryNext();
        }
    }
    /// <summary> �ؽ�Ʈ �ѱ��ھ� ��� �ڷ�ƾ </summary>
    IEnumerator TextOutput(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            textScript.text += text[i];
            yield return new WaitForSeconds(0.05f);
        }
    }

    /// <summary> �� ó�� NPC ���� �����ϴ� ���� </summary>
    void FirstTyping()
    {
        portrait.SetActive(false);
        dark.SetActive(true);
        outline.SetActive(true);
        textScript.text = "";
        UI_Canvas.SetActive(false);
        CoroutineStart(TextOutput(firstScript));
        progress[0] = true;
    }

    /// <summary> ��ȭ�� �ŷ� �������� </summary>
    void ChooseTyping()
    {
        portrait.SetActive(true);
        SelectButtonOff();
        buttonBackgrounds[0].SetActive(true);
        buttonBackgrounds[1].SetActive(true);
        buttonBackgrounds[0].GetComponentInChildren<Text>().text = "��ȭ�� �Ѵ�";
        buttonBackgrounds[1].GetComponentInChildren<Text>().text = "�ŷ��� �Ѵ�";
        buttonBackgrounds[0].GetComponentInChildren<Button>().onClick.RemoveListener(EndTalkButton);
        buttonBackgrounds[0].GetComponentInChildren<Button>().onClick.RemoveListener(NoteButton);
        buttonBackgrounds[1].GetComponentInChildren<Button>().onClick.RemoveListener(ShopButton);
        buttonBackgrounds[0].GetComponentInChildren<Button>().onClick.AddListener(NoteButton);
        buttonBackgrounds[1].GetComponentInChildren<Button>().onClick.AddListener(ShopButton);
        progress[1] = true;
        textScript.text = "";
        CoroutineStart(TextOutput(chooseSciript));
    }

    /// <summary> �����ø ��ȭ���� </summary>
    void NoteTyping()
    {
        SelectButtonOff();
        buttonBackgrounds[0].SetActive(true);
        buttonBackgrounds[0].GetComponentInChildren<Text>().text = "��ȭ ������";
        buttonBackgrounds[0].GetComponentInChildren<Button>().onClick.AddListener(EndTalkButton);
        note.SetActive(true);
        textScript.text = "";
        CoroutineStart(TextOutput(noteScript));
    }

    /// <summary> ���� ��ȭ���� </summary>
    void ShopTyping()
    {
        SelectButtonOff();
        buttonBackgrounds[0].SetActive(true);
        buttonBackgrounds[0].GetComponentInChildren<Text>().text = "��ȭ ������";
        buttonBackgrounds[0].GetComponentInChildren<Button>().onClick.AddListener(EndTalkButton);
        textScript.text = "";
        CoroutineStart(TextOutput(shopScript));
    }
    /// <summary> ��ȭ ��� NPC���Լ� ��ȭ �������� ������ </summary>
    public void SetText(string first, string choose, string note, string shop, string[] personalStory)
    {
        firstScript = first;
        chooseSciript = choose;
        noteScript = note;
        shopScript = shop;
        personalstory = personalStory;
    }

    /// <summary> ��ȭ ������ ��ư </summary>
    public void EndTalkButton()
    {
        SelectButtonOff();
        CloseTalkCanvas();
        UI_Canvas.SetActive(true);
        progress[0] = false;
        progress[1] = false;
    }

    /// <summary> ��ȭ ĵ������ ������ҵ��� ���� �� </summary>
    public void CloseTalkCanvas()
    {
        portrait.SetActive(false);
        note.SetActive(false);
        dark.SetActive(false);
        outline.SetActive(false);
    }

    /// <summary> ���� ��ȭ�������� ���� ��ư </summary>
    public void ShopButton()
    {
        ShopTyping();
    }

    /// <summary> �����ø ��ȭ�������� ���� </summary>
    public void NoteButton()
    {
        NoteTyping();
    }

    /// <summary> �����ø�� �������� �̾߱� ���� </summary>
    public void PersonalStoryButton()
    {
        PersonalStory();
    }

    /// <summary> �������� �̾߱� �ؽ�Ʈ ��� </summary>
    void PersonalStory()
    {
        note.SetActive(false);
        buttonBackgrounds[0].SetActive(false);
        textScript.text = "";
        CoroutineStart(TextOutput(personalstory[0]));
        progress[2] = true;
    }

    /// <summary> �������� �̾߱� ���� ���� ��ȭ���� �� ���� ��� </summary>
    void PersonalStoryNext()
    {
        note.SetActive(true);
        SelectButtonOff();
        buttonBackgrounds[0].SetActive(true);
        textScript.text = "";
        CoroutineStart(TextOutput(personalstory[1]));
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

