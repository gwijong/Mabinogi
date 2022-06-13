using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> UI�� ī�޶� ���� �ٶ󺸵��� �ϴ� ��ũ��Ʈ</summary>
public class UILookAtCamera : MonoBehaviour
{
    /// <summary> ���� ī�޶�</summary>
    Transform mainCamera;
    /// <summary> ī�޶� ������ ���� ȸ����ų ������Ʈ</summary>
    GameObject UI;



    void Start()
    {
        UI = gameObject; //�� �ڽ� ���ӿ�����Ʈ
        mainCamera = Camera.main.transform;//����ī�޶��� Ʈ������
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
        
    }


    public void OnUpdate()
    {

        if (UI == null) //ȸ����ų ���ӿ�����Ʈ�� �ı��Ǹ� ���� ����
        {
            return;
        }
        //UI�� ȸ������ ī�޶� ȸ�������� ��� ����
        UI.transform.rotation = mainCamera.rotation;
    }
}
