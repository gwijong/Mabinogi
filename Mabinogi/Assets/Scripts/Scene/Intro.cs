using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary> �𸮾� ���� �� ����</summary>
public class Intro : MonoBehaviour
{
    /// <summary> ȭ�� �ϴ� �𸮾��� ���� ���� </summary>
    public string[] talk;
    /// <summary> ��縦 ����� �ؽ�Ʈ ������Ʈ </summary>
    public Text text;
    /// <summary> ȭ�� �ϴ��� ���� �׶��̼�</summary>
    public Image dark;
    void Start()
    {
        StartCoroutine(Progress()); //���� ���۵Ǹ� �ڷ�ƾ �ѹ� ����
    }

    private void Update()
    {
        //ī�޶� ��ġ�� ���ݾ� z������ ����
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+(0.7f*Time.deltaTime));

        if(Input.GetKeyDown(KeyCode.Escape)) //ESCŰ ������ ��ŵ
        {
            LoadingScene.NextSceneName = "World";
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }
    }

    IEnumerator Progress()
    {
        dark.color = new Color(1, 1, 1, 1); //���̵� �� ȿ��
        for (int i = 0; i < 100; i++)
        {
            dark.color = new Color(1, 1, 1, 1f - (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        dark.color = new Color(1, 1, 1, 0);

        for (int i = 0; i<talk.Length; i++) //�ݺ� ��� ���
        {
            text.text = talk[i];
            for (int j = 0; j < 10; j++)
            {
                text.color = new Color(1, 1, 1, 0f + (float)j / 10);//�ؽ�Ʈ�� ������ ���������� ���ݾ� ����
                yield return new WaitForSeconds(0.1f);
            }
            text.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(1f);//�ؽ�Ʈ�� 1�ʰ� ������ �������� ���
            for (int j = 0; j < 10; j++)
            {
                text.color = new Color(1, 1, 1, 1f - (float)j / 10);//�ؽ�Ʈ�� �������� ���ݾ� ����
                yield return new WaitForSeconds(0.1f);
            }
            text.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.5f);//�ؽ�Ʈ�� ������ ���¿��� 0.5�� ��⸦ �� ������ ����
        }

        for (int i = 0; i < 100; i++) //���̵�ƿ� ȿ��
        {
            dark.color = new Color(1, 1, 1, 0f + (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        dark.color = new Color(1, 1, 1, 1); 
        yield return new WaitForSeconds(0.5f);
        LoadingScene.NextSceneName = "World"; //���� ������ ��ȯ
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }
}
