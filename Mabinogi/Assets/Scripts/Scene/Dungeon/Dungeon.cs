using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
      
            if(enemyList[i].GetComponent<Character>().die == false) //���� �Ѹ����� ��������� ����
            {
                return;
            }
        }
        //���� ������ ���
        gate[progress - 1].GetComponent<BoxCollider>().enabled = false;
        leftDoor[progress - 1].SetActive(false);
        rightDoor[progress - 1].SetActive(false);
        enemyList.Clear();
    }

    /// <summary> ���� ���� </summary>
    public void Spawn()
    {
        for(int i = 0; i< spawnAmount[progress]; i++)
        {
            Vector3 pos = spawnPos[progress].position;
            enemy = Instantiate(enemys[progress], spawnPos[progress]);
            //������ ��ǥ���� ��¦ ������ ��ġ�� ���� ������
            enemy.transform.position = new Vector3(pos.x+Random.Range(-spawnAmount[progress], spawnAmount[progress])*2, pos.y, pos.z + Random.Range(-spawnAmount[progress], spawnAmount[progress])*2);
            enemyList.Add(enemy); //�����ߴ��� üũ�ϴ� ����Ʈ�� �߰�
        }
        progress++;//���� ���൵ ���ϱ�
    }

}
