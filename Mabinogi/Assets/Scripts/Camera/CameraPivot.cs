using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    public Transform following_object; //����ٴ� �÷��̾� ������Ʈ

    private void Start()
    {
        following_object = GameObject.FindGameObjectWithTag("Player").transform; 
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
    }
    private void OnUpdate()
    {
        Vector3 pos = transform.position;
        //����ٴ� �÷��̾� ������Ʈ�� �� ������Ʈ ��� �̵�
        transform.position = Vector3.Lerp(pos, following_object.position, 0.4f); 
    }
}
