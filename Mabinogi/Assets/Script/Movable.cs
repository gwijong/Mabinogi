using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary> 이동 가능한 오브젝트</summary>
[RequireComponent(typeof(NavMeshAgent))]
public class Movable : Hitable
{
    public NavMeshAgent agent;
    [SerializeField] float speed;


    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 goalPosition)  //입력에서 불러옴
    {

        agent.SetDestination(goalPosition);
        
    }
}