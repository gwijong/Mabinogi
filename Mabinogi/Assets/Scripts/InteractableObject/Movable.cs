using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary> �̵� ������ ������Ʈ</summary>
[RequireComponent(typeof(NavMeshAgent))]
public class Movable : Hitable
{
    protected NavMeshAgent agent;
    protected bool walk = false;
    /// <summary> �̵� ����</summary>
    public Define.MoveState state = Define.MoveState.Runnable;

    /// <summary> �̵� �ӵ� </summary>
    [SerializeField] protected float runSpeed;  //���� Ŭ������ Start�޼��忡�� ��ũ���ͺ� ������Ʈ �������� ����޽� ���ǵ� �Ҵ�
    [SerializeField] protected float walkSpeed;  //���� Ŭ������ Start�޼��忡�� ��ũ���ͺ� ������Ʈ �������� ����޽� ���ǵ� �Ҵ�


    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();       
        agent.speed = runSpeed;  //�⺻�� �޸��� �ӵ�
    }

    /// <summary> ������̼� �̵� �޼��� </summary>
    public virtual void MoveTo(Vector3 goalPosition, bool isWalk = false)  //�Է¿��� �ҷ���
    {
        walk = isWalk;
        agent.isStopped = false;
        agent.SetDestination(goalPosition);  //�������� goalPosition ��ǥ
    }

    /// <summary> ������̼� �̵� ���� �޼��� </summary>
    public void MoveStop(bool value) //�̵� ����
    {
        agent.isStopped = value;
        if(value) agent.SetDestination(agent.transform.position);//�������� �� �ڽ� ��ġ�� ����
    }
}