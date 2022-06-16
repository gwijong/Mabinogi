using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary> �� �ε� ȭ�� ��ũ��Ʈ</summary>
public class LoadingScene : MonoBehaviour
{
    /// <summary> �� �ε��� ������ �Ǿ����� �����ִ� ������ �� </summary>
    public Image GaugeBar;
    /// <summary> �� �ε��� ������ �Ǿ����� �����ִ� �ؽ�Ʈ </summary>
    public Text text;
    /// <summary> �ε��� �� �̸� </summary>
    public static string NextSceneName;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Loading");//�ϴ� �ε� �ڷ�ƾ ����
    }
    /// <summary> �� �ε� </summary>
    IEnumerator Loading()
    {
        if (NextSceneName == null)//�� �̸��� ������
        {
            NextSceneName = "Soulstream";//�� �ε� ���ʰ��� �ҿｺƮ��
        }

        AsyncOperation oper = SceneManager.LoadSceneAsync(NextSceneName);//�񵿱� �� �ε�
        oper.allowSceneActivation = false;//����� �غ�� ��� ����� Ȱ��ȭ�Ǵ� ���� ������� ����.

        while (!oper.isDone)//�ε��� �� ������
        {
            GaugeBar.fillAmount = oper.progress;  //���������� ��ġ�� �ø�
            text.text = (int)(oper.progress * 100f) + 10f + "%"; //�� �ۼ�Ʈ ����ƴ��� �ؽ�Ʈ ǥ��
            if (oper.progress >= 0.9f)//�ε� ���൵�� 90% �̻��� ���
            {
                text.text =  "100%"; //�ε� 100���� �ؽ�Ʈ
                GaugeBar.fillAmount = 1f; //���������� ��ġ �ִ�ġ�� ǥ��
                oper.allowSceneActivation = true; //����� �غ�� ��� ����� Ȱ��ȭ ���
            }
            yield return new WaitForSeconds(0.01f);//�ݺ��� �ʹ� ���� ���� �ʰ� ���
        }
    }
}
