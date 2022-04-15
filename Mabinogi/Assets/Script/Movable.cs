using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary> �̵� ������ ������Ʈ</summary>
[RequireComponent(typeof(NavMeshAgent))]
public class Movable : Hitable
{
    protected NavMeshAgent _agent;
    [SerializeField] float _speed;

    bool _movable = true;

    protected virtual void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 goalPosition)  //�Է¿��� �ҷ���
    {
        _agent.SetDestination(goalPosition);
    }
}