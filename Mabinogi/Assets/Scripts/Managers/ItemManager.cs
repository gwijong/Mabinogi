using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // ����޽� ���� �ڵ�

public class ItemManager : MonoBehaviour
{
    /// <summary> ������ ������ ��ũ���ͺ� ������Ʈ </summary>
    public ItemData[] data;
    /// <summary> �÷��̾� ��ġ���� �������� ��ġ�� �ִ� �ݰ� </summary>
    public float maxDistance = 3f;
    /// <summary> �������� ������ ���� </summary>
    public float yPos = 2f;

    /// <summary> ������ ������ </summary>
    public void DropItem(Define.Item item, int currentAmount)
    {
        GameObject dropitem = Instantiate(Resources.Load<GameObject>("Prefabs/Item/ItemPrefab"));//���� ������

        if (dropitem == null) return; //������ �ҷ����� �����ϸ� Ż��

        dropitem.GetComponent<CreateItem>().amount = currentAmount; //�ٴڿ� ������ �������� ������ �Ű����� currentAmount�� �����̴�.
        dropitem.GetComponent<CreateItem>().item = item; //CreateItem ��ũ��Ʈ�� item ������ �Ű����� item�� ����

        //�÷��̾� ��ó���� ����޽� ���� ���� ��ġ ��������
        Vector3 spawnPosition = GetRandomPointOnNavMesh(GameObject.FindGameObjectWithTag("Player").transform.position, maxDistance);
        //�ٴڿ��� yPos��ŭ y��ǥ ���� �ø���
        spawnPosition += Vector3.up * yPos;
        dropitem.transform.position = spawnPosition;//�÷��̾� ��ǥ�� �߽����� maxDistance���� �ȿ� yPos ���� ��ǥ�� ������ ����
    }

    //����޽� ���� ������ ��ġ�� ��ȯ�ϴ� �޼���
    /// <summary> center�� �߽����� distance �ݰ� �ȿ����� ������ ��ġ�� ã�� </summary>
    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        // center�� �߽����� �������� maxDistance�� �� �ȿ����� ������ ��ġ �ϳ��� ����
        // Random.insideUnitSphere�� �������� 1�� �� �ȿ����� ������ �� ���� ��ȯ�ϴ� ������Ƽ
        Vector3 randomPos = (Random.insideUnitSphere * distance) + center;

        //����޽� ���ø��� ��� ������ �����ϴ� ����
        NavMeshHit hit;
        //maxDistance �ݰ� �ȿ��� randomPos�� ���� ����� ����޽� ���� �� ���� ã��
        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);//out = ������� �Ű�����
        //ã�� �� ��ȯ
        return hit.position;
    }
}
