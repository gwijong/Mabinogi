using UnityEngine.AI; // ����޽� ���� �ڵ�
using UnityEngine;

public class Tree : Hitable
{
    public GameObject[] items; //������ ������
    
    public float maxDistance = 3f; // �÷��̾� ��ġ���� �������� ��ġ�� �ִ� �ݰ�

    public override bool TakeDamage(Character from)
    {
        //�÷��̾� ��ó���� ����޽� ���� ���� ��ġ ��������
        Vector3 spawnPosition = GetRandomPointOnNavMesh(from.transform.position, maxDistance);//�Ű����� 2��
        //�ٴڿ��� 0.5��ŭ ���� �ø���
        spawnPosition += Vector3.up * 0.5f;

        //������ �� �ϳ��� �������� ��� ���� ��ġ�� ����
        GameObject selectedItem = items[Random.Range(0, items.Length)];
        GameObject item = Instantiate(selectedItem, spawnPosition, Quaternion.identity);

        //������ �������� 5�� �ڿ� �ı�
        Destroy(item, 5f);
        return true;      
    }

    private void Spawn()
    {

    }

    //����޽� ���� ������ ��ġ�� ��ȯ�ϴ� �޼���
    //center�� �߽����� distance �ݰ� �ȿ����� ������ ��ġ�� ã��
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


