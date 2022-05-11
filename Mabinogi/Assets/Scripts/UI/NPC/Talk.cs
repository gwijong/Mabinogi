using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Talk : MonoBehaviour
{
    //GameObject talkCanvas;
    public string []script;
    Queue<string> sentences = new Queue<string>();

    void Start()
    {
        //talkCanvas = GameObject.FindGameObjectWithTag("Dialog").gameObject;
        for(int i = 0; i< script[0].Length; i++)
        {
            sentences.Enqueue(script[0][i].ToString());
        }
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        for(int i = 0; i< sentences.Count; i++)
        {
            Debug.Log(sentences.Peek());
            sentences.Dequeue();
            yield return new WaitForSeconds(0.05f);
        }
    }
}

/*
 * [System.Serializable]
public class Dialogue
{
    public List<string> sentences;
}

public class DialogueSystem : MonoBehaviour
{
    public Text txtSentence;
    public Dialogue info;
    Queue<string> sentences = new Queue<string>();

    private void Start()
    {
        Begin(info);
    }
    //��ȭ ����
    public void Begin(Dialogue info)
    {
        sentences.Clear();

        foreach(var sentence in info.sentences)
        {
            sentences.Enqueue(sentence);
        }

        Next();
    }
    //��ư Ŭ�� �� ���� ��ȭ�� �Ѿ
    public void Next()
    {
        if(sentences.Count == 0)
        {
            End();
            return;
        }

        txtSentence.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentences.Dequeue()));
    }
    //Ÿ���� ��� �Լ�
    IEnumerator TypeSentence(string sentence)
    {
        foreach(var letter in sentence)
        {
            txtSentence.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }
    //��ȭ ��
    private void End()
    {
        if (sentences != null)
        {
            Debug.Log("end");
        }
    }
}

 * 
 */