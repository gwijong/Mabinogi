using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �÷��̾� ��� UI </summary>
public class PlayerDie : MonoBehaviour
{
    /// <summary> �������� ��Ȱ </summary>
    public void ReviveInTown()
    {
        LoadingScene.NextSceneName = "World";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        gameObject.SetActive(false);
    }

    /// <summary> �� �ڸ����� ��Ȱ </summary>
    public void ReviveHere()
    {
        GameManager.soundManager.PlayBgmPlayer((Define.Scene)UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);//���� ���� ����������� ��ȯ
        FindObjectOfType<Player>().Respawn();//�÷��̾� ��Ȱ
        gameObject.SetActive(false);
    }

    /// <summary> ���α׷� ���� </summary>
    public void EndTheProgram()
    {
        Application.Quit();
    }
}
