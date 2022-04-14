using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum mouseKey
{
    LeftClick,
    rightClick,
    middleClick
}
//�̰� �� �ϳ��� �����
//ĳ���Ϳ� ���� ���� ����
public class PlayerController : MonoBehaviour
{
    [SerializeField] Pawn target;  //Player ĳ���� �� �ϳ�
    [SerializeField] mouseKey mouse;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Pawn>();
    }

    void Update()
    {
        if (target == null) return;

        if(Input.GetMouseButtonDown((int)mouse))
        {
            target.MoveTo(Vector3.zero);
        };
    }
}
