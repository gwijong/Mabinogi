using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�̰� �� �ϳ��� �����
//ĳ���Ϳ� ���� ���� ����
public class PlayerController : MonoBehaviour
{
    Character target;  //Player ĳ���� �� �ϳ�
    Animator ani;
    float distance;
    Vector3 dest;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        ani = target.GetComponent<Animator>();
    }

    void Update()
    {
        if (target == null) return;

        if (Input.GetMouseButtonDown((int)Define.mouseKey.LeftClick))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool raycastHit = Physics.Raycast(ray, out hit, 100f);
            dest = hit.point;
            target.MoveTo(dest);
            ani.SetBool("Move", true);
        };
        distance = Vector3.Distance(target.transform.position, dest);
        if (distance < 1f)
        {
            target.MoveTo(target.transform.position);
            ani.SetBool("Move", false);
        }

        if (Input.GetKeyDown(KeyCode.Space)&& ani.GetBool("Offensive"))
        {
            ani.SetBool("Offensive", false);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !ani.GetBool("Offensive"))
        {
            ani.SetBool("Offensive", true);
        }
    }
}
