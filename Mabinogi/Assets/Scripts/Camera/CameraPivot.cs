using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    public Transform following_object; //����ٴ� �÷��̾� Ʈ������
    public float Ypos = 2;
    private void Start()
    {
        following_object = GameObject.FindGameObjectWithTag("Player").transform; //�÷��̾� Ʈ������ �Ҵ�
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
    }
    private void OnUpdate()
    {
        Vector3 pos = transform.position;//���� ��ġ
        Vector3 followPos = new Vector3 (following_object.position.x, following_object.position.y+ Ypos, following_object.position.z);
        //����ٴ� �÷��̾� ������Ʈ�� �� ������Ʈ ��� �̵�
        transform.position = Vector3.Lerp(pos, followPos, 0.4f); //���� ��ġ���� �÷��̾� ��ġ�� �ε巴�� �̵�
    }
}
