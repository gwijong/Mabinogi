using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary> �̵� ������ ������Ʈ</summary>
[RequireComponent(typeof(NavMeshAgent))]
public class Movable : Hitable
{
    public NavMeshAgent agent;
    [SerializeField] float speed;  //Start�޼��忡 ����޽� ���ǵ� �Ҵ�


    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 goalPosition)  //�Է¿��� �ҷ���
    {
        agent.SetDestination(goalPosition);
    }
}