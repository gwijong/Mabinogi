using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary> ���� ����</summary>
public class Dungeon : MonoBehaviour
{
    /// <summary> ���� ���൵ </summary>
    int progress = 0;
    /// <summary> �� ������Ʈ�� �浹 </summary>
    public GameObject[] gate;
    /// <summary> ���� ��¦ </summary>
    public GameObject[] leftDoor;
    /// <summary> ������ ��¦ </summary>
    public GameObject[] rightDoor;
    /// <summary> �� ���͵� </summary>
    public GameObject[] enemys;
    /// <summary> ���� ���� ��ǥ�� </summary>
    public Transform[] spawnPos;
    /// <summary> ���� ������ ���� </summary>
    GameObject enemy = null;
    /// <summary> ������ ���� ���� </summary>
    public int[] spawnAmount;
    
    /// <summary> �� ���忡 ������ ��� ���͵� </summary>
    [SerializeField]
    List<GameObject> enemyList = new List<GameObject>();

    void Update()
    {
        if(progress - 1 < 0)  //���൵�� ������ ����
        {
            return;
        }
        for(int i = 0; i< spawnAmount[progress-1]; i++)
        {
            if(enemyList.Count == 0) //���� 0������ ����
            {
                return;
            }
            if(enemyList[i] != null)
            {
                if (enemyList[i].GetComponent<Character>().die == false) //���� �Ѹ����� ��������� ����
                {
                    return;
                }
            }
        }
        //���� ������ ���
        gate[progress - 1].GetComponent<BoxCollider>().enabled = false; //�� ������
        leftDoor[progress - 1].SetActive(false); //���� �� ġ��
        rightDoor[progress - 1].SetActive(false); //������ �� ġ��
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.dungeon_door, gate[progress-1].transform.position); //�� ���� ȿ����
        enemyList.Clear(); //�� ����Ʈ �ʱ�ȭ
    }

    /// <summary> ���� ���� </summary>
    public void Spawn()
    {      
        for (int i = 0; i< spawnAmount[progress]; i++)
        {
            Vector3 pos = spawnPos[progress].position; //������ ��ġ
            enemy = Instantiate(enemys[progress]); //���� ������
            enemy.GetComponent<NavMeshAgent>().enabled = false; //���͸� ���ϴ� ��ġ�� �ű�� ���� ����޽� ��� ��
            //������ ��ǥ���� ��¦ ������ ��ġ�� ���� ������
            pos.x += Random.Range(-1.0f, 1.0f) * spawnAmount[progress];
            pos.z += Random.Range(-1.0f, 1.0f) * spawnAmount[progress];
            enemy.transform.position = pos;
            enemyList.Add(enemy); //�����ߴ��� üũ�ϴ� ����Ʈ�� �߰�
            enemy.GetComponent<Character>().spawnPos = pos;
            enemy.GetComponent<NavMeshAgent>().enabled = true; //����޽� ��
            //���� ���� ����Ʈ
            GameObject Effect = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/SmokeCircleDark"));
            Effect.transform.position = pos;
            Destroy(Effect, 2f);
        }
        progress++;//���� ���൵ ���ϱ�
    }

}
