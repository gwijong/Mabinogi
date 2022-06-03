using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadCollider : MonoBehaviour
{
    /// <summary> �ε��� �� �̸� </summary>
    public string SceneName;
    /// <summary> Ʈ���� �浹 �� �� �ε� </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == (int)Define.Layer.Player)
        {
            LoadingScene.NextSceneName = SceneName;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");//�ε� �� �� SceneName �� �ε�
        }
    }
}
