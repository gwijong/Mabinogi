using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ���� �� �Ѱ��� �ִ� �Ŵ��� </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager manager; 

    UpdateManager _update = new UpdateManager(); //������Ʈ �Ŵ��� ��ü ����
    public static UpdateManager update { get { return manager._update; } } //������Ʈ �Ŵ��� ��ü �б����� ������Ƽ

    void Awake() 
    {
        //�̱��� üũ
        if (manager == null)
        {
            manager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        update.OnUpdate();
    }
}
