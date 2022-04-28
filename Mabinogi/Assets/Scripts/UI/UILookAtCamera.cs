using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI�� ī�޶� ���� �ٶ󺸵� �ϴ� ��ũ��Ʈ
public class UILookAtCamera : MonoBehaviour
{
    Transform mainCamera; //���� ī�޶�
    GameObject UI; // ī�޶� ������ ���� ȸ����ų ������Ʈ

    void Start()
    {
        UI = gameObject;
        mainCamera = Camera.main.transform;
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
    }


    void OnUpdate()
    {
        if (UI == null) //ȸ����ų ���ӿ�����Ʈ�� �ı��Ǹ� ���� ����
        {
            return;
        }
        //UI�� ȸ������ ī�޶� ȸ�������� ��� ����
        UI.transform.rotation = mainCamera.rotation;
    }
}
