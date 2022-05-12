using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  �����۰� �÷��̾�� ���� �浹�� ���ϰ� �� </summary>
public class CollisionOff : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(Off());//������ ���� �� �ڷ�ƾ ����
    }

/// <summary> ������ ���� 3�ʵڿ� �ʵ� �������� ������ٵ�� �ݶ��̴��� ������ �浹 ���� </summary>
    IEnumerator Off()
    {
        yield return new WaitForSeconds(3.0f); //3�� ���
        gameObject.GetComponent<Rigidbody>().useGravity = false; //������ٵ��� �߷� ��Ȱ��ȭ
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); //������ٵ��� �ӵ� �ʱ�ȭ
        gameObject.GetComponent<BoxCollider>().isTrigger = true; //�ڽ��ݶ��̴��� Ʈ���� üũ
    }
}
