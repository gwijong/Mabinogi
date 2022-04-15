using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�̰� �� �ϳ��� �����
//ĳ���Ϳ� ���� ���� ����
public class PlayerController : MonoBehaviour
{
    [SerializeField] Character target;  //Player ĳ���� �� �ϳ�
    [SerializeField] Define.mouseKey mouse;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
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
