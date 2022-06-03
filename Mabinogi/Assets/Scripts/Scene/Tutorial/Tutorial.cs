using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour
{
    /// <summary> ������ �ΰ� ĳ���� </summary>
    public Character Player;
    /// <summary> �������� ���� ĳ���� </summary>
    public Character Wolf;
    /// <summary> �ΰ� ĳ������ ���� ��ġ</summary>
    Vector3 characterPos;
    /// <summary> ���� ĳ������ ���� ��ġ </summary>
    Vector3 wolfPos;
    /// <summary> ���� �� Ʃ�丮�� ���� �̹��� </summary>
    public GameObject image;
    /// <summary> Ʃ�丮�� ���� �ؽ�Ʈ </summary>
    public GameObject text;
    /// <summary> ȭ�� ���� "Ʃ�丮��" �̶�� �빮¦���ϰ� �� �ؽ�Ʈ </summary>
    public GameObject startText;
    /// <summary> ���� �� ���� �̹��� ��������Ʈ�� </summary>
    public Sprite[] sprites;
    /// <summary> ���̵��� ���̵�ƿ� �� �̹��� </summary>
    public Image whiteImage;

    void Start()
    {
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
        characterPos = Player.transform.position; //�ΰ� ���� ��ġ �Է�
        wolfPos = Wolf.transform.position; //���� ���� ��ġ �Է�
        StartCoroutine(Progress()); // �̺�Ʈ ���� �ڷ�ƾ ����
    }

    void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//ESCŰ ������ ��ŵ
        {
            LoadingScene.NextSceneName = "Intro";
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }
    }

    /// <summary> �ΰ��� ���븦 �ٽ� ���� </summary>
    void Reset()
    {
        Player.transform.position = characterPos; //���� ��ġ�� �̵�
        Wolf.transform.position = wolfPos;//���� ��ġ�� �̵�
        Wolf.MoveStop(true); //���� �̵� ����
        Player.MoveStop(true); //�ΰ� �̵� ����
        image.SetActive(false); //���� ���� �̹��� ��Ȱ��ȭ
        text.SetActive(false); //���� ���� �ؽ�Ʈ ��Ȱ��ȭ
    }

    /// <summary> ���� ��Ģ ���� �̹����� �ؽ�Ʈ Ȱ��ȭ </summary>
    void ImageActive(int spriteNumber)
    {
        image.SetActive(true); //���� ���� �̹��� Ȱ��ȭ
        image.GetComponent<Image>().sprite = sprites[spriteNumber];//���� ���� �̹����� ��������Ʈ�� ����
        text.SetActive(true); //���� ���� �ؽ�Ʈ Ȱ��ȭ
    }
    IEnumerator Progress()
    {
        for (int i = 0; i < 100; i++)//ȭ�� ��ü�� ä�� ��� �̹����� �������� ���� ����, ���̵���
        {
            whiteImage.color = new Color(1, 1, 1, 1f - (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        whiteImage.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(3.0f);
        startText.SetActive(false);
        ImageActive(5);
        text.GetComponent<Text>().text = "�������� ���� ��Ģ�� ������������ ����մϴ�.";
        yield return new WaitForSeconds(3.0f);
        text.SetActive(false);
        image.SetActive(false);

        yield return new WaitForSeconds(2.0f);
        Wolf.Casting(Define.SkillState.Defense); //���� ���潺 ����
        ImageActive(0);
        text.GetComponent<Text>().text = "���潺�� �⺻ ������ ���� ������ ���߰� ����ϴ�.";
        yield return new WaitForSeconds(2.0f);
        Player.SetTarget(Wolf);
        yield return new WaitForSeconds(1.0f);
        Player.SetTarget(null);
        yield return new WaitForSeconds(4.0f);
        Reset();

        yield return new WaitForSeconds(2.0f);
        Player.Casting(Define.SkillState.Smash); //�ΰ� ���Ž� ����
        Wolf.Casting(Define.SkillState.Defense); //���� ���潺 ����
        ImageActive(1);
        text.GetComponent<Text>().text = "���Žô� ���潺�� ���� ������ �ٿ��ŵ�ϴ�.";
        yield return new WaitForSeconds(2.0f);
        Player.SetTarget(Wolf);
        yield return new WaitForSeconds(1.0f);
        Player.SetTarget(null);
        yield return new WaitForSeconds(4.0f);
        Reset();

        yield return new WaitForSeconds(2.0f);
        Player.Casting(Define.SkillState.Combat); //�ΰ� ��Ÿ ����
        Wolf.Casting(Define.SkillState.Smash); //���� ���Ž� ����
        ImageActive(2);
        text.GetComponent<Text>().text = "���Žô� ������ Ŀ�� �⺻ ���ݿ� �����ϴ�.";
        yield return new WaitForSeconds(2.0f);
        Player.SetTarget(Wolf);
        Wolf.SetTarget(Player);
        yield return new WaitForSeconds(1.0f);
        Player.SetTarget(null);
        Wolf.SetTarget(null);
        yield return new WaitForSeconds(4.0f);
        Reset();

        yield return new WaitForSeconds(2.0f);
        Player.Casting(Define.SkillState.Smash); //�ΰ� ���Ž� ����
        Wolf.Casting(Define.SkillState.Counter); //���� ī���� ����
        ImageActive(3);
        text.GetComponent<Text>().text = "ī���� ������ ���� ������ �Ϻ��ϰ� ȸ�� �� �ݰ��մϴ�.";
        yield return new WaitForSeconds(2.0f);
        Player.SetTarget(Wolf);
        yield return new WaitForSeconds(1.0f);
        Player.SetTarget(null);
        Wolf.SetTarget(null);
        yield return new WaitForSeconds(4.0f);
        Reset();

        yield return new WaitForSeconds(2.0f);
        Player.Casting(Define.SkillState.Icebolt); //�ΰ� ���̽���Ʈ ����
        Wolf.Casting(Define.SkillState.Counter); //���� ī���� ����
        ImageActive(4);
        text.GetComponent<Text>().text = "�׷��� ī���� ������ ������ �� �����ϴ�.";
        yield return new WaitForSeconds(3.5f);
        Player.SetTarget(Wolf);
        yield return new WaitForSeconds(1.0f);
        Player.SetTarget(null);
        Wolf.SetTarget(null);
        yield return new WaitForSeconds(3.0f);
        for (int i = 0; i < 100; i++) //ȭ���� ä�� ���� �̹����� ���� ������� ����, ���̵�ƿ�
        {
            whiteImage.color = new Color(1, 1, 1, 0f + (float)i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        whiteImage.color = new Color(1, 1, 1, 1);

        LoadingScene.NextSceneName = "Intro";//��Ʈ�� �� �ε�
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }
}
