using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary> �̵� ������ ������Ʈ</summary>
[RequireComponent(typeof(NavMeshAgent))]
public class Movable : Hitable
{
    public NavMeshAgent agent;
    /// <summary> �̵� �ӵ� </summary>
    [SerializeField] float moveSpeed;  //Start�޼��忡 ����޽� ���ǵ� �Ҵ�


    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();       
        agent.speed = moveSpeed;
    }

    /// <summary> ������̼� �̵� �޼��� </summary>
    public void MoveTo(Vector3 goalPosition)  //�Է¿��� �ҷ���
    {
        agent.isStopped = false;
        agent.SetDestination(goalPosition);       
    }

    /// <summary> ������̼� �̵� ���� �޼��� </summary>
    public void MoveStop(bool value) //�̵� ����
    {
        agent.isStopped = value;
    }
}