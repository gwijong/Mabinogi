using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //����� �Է¿� ���� �÷��̾� ĳ���͸� �����̴� ��ũ��Ʈ
    public float moveSpeed = 5f;  //�յ� �������� �ӵ�
    public float rotateSpeed = 180f;  //�¿� ȸ�� �ӵ�

    private PlayerInput playerInput;  //�÷��̾� �Է��� �˷��ִ� ������Ʈ
    private Rigidbody playerRigidbody;  //�÷��̾� ĳ������ ������ٵ�
    private Animator playerAnimator;   //�÷��̾� ĳ������ �ִϸ�����

    void Start()
    {//����� ������Ʈ���� ���� ��������
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    //FixedUpdate�� ���� ���� �ֱ⿡ ���� �����
    void FixedUpdate()
    {//���� ���� �ֱ⸶�� ������ ����
        Move();
        PlayerAni();
    }

    //�Է°��� ���� ĳ���͸� �յڷ� ������
    private void Move()
    {
        Vector3 dir = new Vector3(0,0,0);
        if (playerInput.Front)
        {
            dir = new Vector3 (0, 0, -1);
        }
        else if (playerInput.Back)
        {
            dir = new Vector3(0, 0, 1);
        }
        else if (playerInput.Right)
        {
            dir = new Vector3(-1, 0, 0);
        }
        else if (playerInput.Left)
        {
            dir = new Vector3(1, 0, 0);
        }
        else
        {
            dir = new Vector3(0, 0, 0);
        }

        //��������� �̵��� �Ÿ� ���
        Vector3 MoveDistance = dir * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + MoveDistance);
    }

    void PlayerAni()
    {
        if (playerInput.Front == true || playerInput.Back == true || playerInput.Left == true || playerInput.Right == true)
        {
            playerAnimator.SetBool("Move", true);
        }
        else
        {
            playerAnimator.SetBool("Move", false);
        }
    }

}
