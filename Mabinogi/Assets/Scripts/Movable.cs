using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary> �̵� ������ ������Ʈ</summary>
[RequireComponent(typeof(NavMeshAgent))]
public class Movable : Hitable
{
    /// <summary> ����޽�</summary>
    protected NavMeshAgent agent;
    /// <summary> �ȴ°�?</summary>
    protected bool walk = false;
    /// <summary> �̵� ����</summary>
    [Tooltip("�̵� ����")]
    public Define.MoveState state = Define.MoveState.Runnable;

    /// <summary> �̵� �ӵ� </summary>
    [SerializeField] [Tooltip("�޸��� �ӵ�")] protected float runSpeed;  //���� Ŭ������ Start�޼��忡�� ��ũ���ͺ� ������Ʈ �������� ����޽� ���ǵ� �Ҵ�
    [SerializeField] [Tooltip("�ȴ� �ӵ�")] protected float walkSpeed;  //���� Ŭ������ Start�޼��忡�� ��ũ���ͺ� ������Ʈ �������� ����޽� ���ǵ� �Ҵ�


    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); //�� ĳ������ ����޽� ������Ʈ �Ҵ�       
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