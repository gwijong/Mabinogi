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
        AsyncOperation oper = SceneManager.LoadSceneAsync(NextSceneName);
        oper.allowSceneActivation = false;

        float timer = 0.01f;
        while (!oper.isDone)//�ε��� �� ������
        {
            yield return new WaitForSeconds(0.01f);
            timer += Time.deltaTime;

            if (oper.progress >= 0.9f)//�ε� ���൵�� 90% �̻��� ���
            {
                GaugeBar.fillAmount = Mathf.Lerp(GaugeBar.fillAmount, 1f, timer); //���������� ��ġ�� �ε巴�� �ø�
                text.text = (oper.progress * 100f) + 10f + "%";//�� �ۼ�Ʈ ����ƴ��� �ؽ�Ʈ ǥ��

                if (GaugeBar.fillAmount == 1.0f)
                    oper.allowSceneActivation = true; //����� �غ�� ��� ����� Ȱ��ȭ ���
            }
            else
            {
                text.text = (oper.progress * 100f) + 10f + "%"; //�� �ۼ�Ʈ ����ƴ��� �ؽ�Ʈ ǥ��
                GaugeBar.fillAmount = Mathf.Lerp(GaugeBar.fillAmount, oper.progress, timer);  //���������� ��ġ�� �ε巴�� �ø�
                if (GaugeBar.fillAmount >= oper.progress)
                {
                    timer = 0f; //Ÿ�̸� �ʱ�ȭ
                }
            }
        }
    }
}
