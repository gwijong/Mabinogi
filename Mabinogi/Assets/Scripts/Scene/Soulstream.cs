using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> ���� ���� �� ����</summary>
public class Soulstream : MonoBehaviour
{
    /// <summary> ���� NPC ���ӿ�����Ʈ </summary>
    public GameObject nao;
    /// <summary> ���̵�ƿ��� �̹��� </summary>
    public Image whiteImage;
    /// <summary> ������� ��� ���ӿ�����Ʈ </summary>
    public GameObject bgmPlayer;
    /// <summary> ȿ���� ��� ���ӿ�����Ʈ </summary>
    public GameObject sfxPlayer;
    /// <summary> ���� ���� ������� </summary>
    public AudioClip naoAppear;
    /// <summary> �ξ��� �Ҹ� </summary>
    public AudioClip naoStage;
    void Start()
    {
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
        StartCoroutine(NaoAppear());
    }

    void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //escŰ ������ ��ŵ
        {
            LoadingScene.NextSceneName = "Tutorial"; //Ʃ�丮�� ������ �̵�
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }
    }
    /// <summary> ���� ���� </summary>
    IEnumerator NaoAppear()
    {
        yield return new WaitForSeconds(0.5f);
        sfxPlayer.GetComponent<AudioSource>().clip = naoStage;//�ξ��� �Ҹ� ���
        sfxPlayer.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(7.0f);
        bgmPlayer.GetComponent<AudioSource>().clip = naoAppear; //���� ���� ������� ���
        bgmPlayer.GetComponent<AudioSource>().Play();
        for (int i = 0; i < 20; i++)  //���̵�ƿ�
        {
            whiteImage.color = new Color(1, 1, 1, 0f + (float)i / 20);
            yield return new WaitForSeconds(0.01f);
        }
        whiteImage.color = new Color(1, 1, 1, 1);
        GameObject go = Instantiate(nao);
        go.transform.position = new Vector3(0, 0, 0);
        for (int i = 0; i < 100; i++) //���̵���
        {
            whiteImage.color = new Color(1, 1, 1, 1f - (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        whiteImage.color = new Color(1, 1, 1, 0);
    }

    /// <summary> ���� ���� </summary>
    public IEnumerator NaoDisappear()
    {
        //yield return new WaitForSeconds(0f);
        for (int i = 0; i < 100; i++)
        {
            whiteImage.color = new Color(1, 1, 1, 0f + (float)i / 100); //���̵�ƿ�
            yield return new WaitForSeconds(0.01f);
        }
        whiteImage.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.5f);
        LoadingScene.NextSceneName = "Tutorial"; //Ʃ�丮�� �� �ε�
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }
}
