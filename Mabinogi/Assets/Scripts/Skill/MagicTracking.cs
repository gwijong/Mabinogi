using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ��ǥ���� ���� ���ư��� ���� </summary>
public class MagicTracking : MonoBehaviour
{
    public Transform target;
    void Start()
    {
        //������Ʈ �Ŵ����� Update�޼��忡 �����ֱ�
        GameManager.update.UpdateMethod -= OnUpdate;
        GameManager.update.UpdateMethod += OnUpdate;
    }

    void OnUpdate()
    {
        if (target != null)
        {
            Follow();//��� ����
        }

    }

    /// <summary> ����ٴ� Ÿ�� ������Ʈ�� �� ������Ʈ ��� �̵�</summary>
    public void Follow()
    {
        Vector3 followPos = new Vector3(target.position.x, target.position.y, target.position.z);//���� ��� ��ǥ�� ��ǥ
        transform.position = Vector3.Lerp(gameObject.transform.position, followPos, 8f*Time.deltaTime); //��ǥ���� ���� ��� �ε巴�� ����

        if((followPos - gameObject.transform.position).magnitude < 1) //�Ÿ��� 1 �̳��̸� 
        {
            GameManager.update.UpdateMethod -= OnUpdate; //���� ����
            Destroy(gameObject); //�� ���� ���ӿ�����Ʈ �ı�
        }
    }
}
