using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //����� �Է¿� ���� �÷��̾� ĳ���͸� �����̴� ��ũ��Ʈ
    public float moveSpeed = 5f;  //�յ� �������� �ӵ�
    public float rotateSpeed = 180f;  //�¿� ȸ�� �ӵ�

    private PlayerInput playerInput;  //�÷��̾� �Է��� �˷��ִ� ������Ʈ
    private Rigidbody rigid;  //�÷��̾� ĳ������ ������ٵ�
    private Animator ani;   //�÷��̾� ĳ������ �ִϸ�����
    private Character character;

    void Start()
    {//����� ������Ʈ���� ���� ��������
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
        character = GetComponent<Character>();
    }

    //FixedUpdate�� ���� ���� �ֱ⿡ ���� �����
    void FixedUpdate()
    {//���� ���� �ֱ⸶�� ������ ����
        if (character.stiffnessCount != 0 || character.die)
        {
            return;
        }
        KeyMove();
        PlayerAni();
        MouseMove();

    }

    //�Է°��� ���� ĳ���͸� �յڷ� ������
    void KeyMove()
    {
        Vector3 dir = new Vector3(0,0,0);

        if (playerInput.Front)
        {           
            dir = transform.forward;
        }
        else if (playerInput.Back)
        {
            dir = -transform.forward;
        }
        else if (playerInput.Right)
        {           
            dir = transform.right;
        }
        else if (playerInput.Left)
        {
            dir = -transform.right;
        }
        else
        {
            dir = new Vector3(0, 0, 0);
        }

        //��������� �̵��� �Ÿ� ���
        Vector3 MoveDistance = dir * moveSpeed * Time.deltaTime;
        rigid.MovePosition(rigid.position + MoveDistance);     

    }
    void PlayerAni()
    {
        if (playerInput.Front == true || playerInput.Back == true || playerInput.Left == true || playerInput.Right == true)
        {
            character.AniOff();
            ani.SetBool("Move", true);
        }
        else
        {
            ani.SetBool("Move", false);
        }
    }

    private Vector3 movePos = Vector3.zero;
    private Vector3 moveDir = Vector3.zero;

    void MouseMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // RaycastHit raycastHit;
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                // �̵� ����
                movePos = raycastHit.point;
            }
            Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.green);
        }
        if (movePos != Vector3.zero)
        {
            character.AniOff();
            gameObject.GetComponent<Animator>().SetBool("Move", true);
            // ������ ���Ѵ�. 
            Vector3 dir = movePos - transform.position;

            // ������ �̿��� ȸ������ ���Ѵ�.
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

            // ȸ�� �� �̵� 
            transform.rotation = Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z);
            transform.position = Vector3.MoveTowards(transform.position, movePos, moveSpeed * Time.deltaTime);
        }
        // ������ġ�� ��ǥ��ġ ������ �Ÿ��� ���Ѵ�.
        float dis = Vector3.Distance(transform.position, movePos);

        // ��ǥ���� ���޽� �̵������� �ʱ�ȭ�� �߰����� �������� �����Ѵ�. 
        if (dis <= 2f)
        {
            gameObject.GetComponent<Animator>().SetBool("Move", false);
            movePos = Vector3.zero;
        }
    }
}
