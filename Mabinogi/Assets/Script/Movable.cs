using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MoveState
{  //�ӵ��� �ٸ��� ����
   //�Ȱ� �ٴ°� Character ���� ����
    Rooted,
    Walkable,
    Runnable,
}

/// <summary> �̵� ������ ������Ʈ</summary>
[RequireComponent(typeof(NavMeshAgent))]
public class Movable : Hitable
{
    public NavMeshAgent agent;

    public MoveState state = MoveState.Runnable;

    /// <summary> �̵� �ӵ� </summary>
    [SerializeField] float runSpeed;  //Start�޼��忡 ����޽� ���ǵ� �Ҵ�
    [SerializeField] float walkSpeed;  //Start�޼��忡 ����޽� ���ǵ� �Ҵ�


    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();       
        agent.speed = runSpeed;
    }

    /// <summary> ������̼� �̵� �޼��� </summary>
    public virtual void MoveTo(Vector3 goalPosition)  //�Է¿��� �ҷ���
    {
        agent.isStopped = false;
        agent.SetDestination(goalPosition);
    }

    /// <summary> ������̼� �̵� ���� �޼��� </summary>
    public void MoveStop(bool value) //�̵� ����
    {
        agent.isStopped = value;
        if(value) agent.SetDestination(agent.transform.position);
    }
}