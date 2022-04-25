using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>�÷��̾� ĳ���� �� �ϳ�</summary>
    Character player;
    /// <summary>���콺�� Ŭ���� Ÿ��</summary>
    Character target;
    /// <summary>Ground ���̾�� Enemy ���̾��� ���̾��ũ</summary>
    int layerMask = 1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Enemy;

    private void Start()
    {      
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();  //�÷��̾� �±� ã�Ƽ� ������
    }

    void Update()
    {
        SkillInput();
        MouseInput();        
        KeyMove();
        SpaceOffensive();
    }

    /// <summary>�����̽��� �Է¹޾� �ϻ�, ������� ��ȯ</summary>
    void SpaceOffensive()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetOffensive();
        };
    }

    /// <summary>���콺 �Է� �̵��̳� Ÿ�� ����</summary>
    void MouseInput()
    {
        if (Input.GetMouseButtonDown((int)Define.mouseKey.LeftClick))  //���콺 ��Ŭ�� �ԷµǸ�
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //ī�޶󿡼� ���콺��ǥ�� ���̸� ��
            RaycastHit hit;  //�浹 ��ü ���� �޾ƿ� ������ �����̳�

            if (Physics.Raycast(ray, out hit, 100f, layerMask))//����, �浹 ����, ���� �Ÿ�, ���̾��ũ
            {
                target = hit.collider.GetComponent<Character>();  //�浹�� ����� ĳ���͸� Ÿ�ٿ� �Ҵ� �õ�

                //ĳ���� ��� �Ҵ� ����            �浹�� ����� ���̸�
                if(!player.SetTarget(target) && hit.collider.gameObject.layer == (int)Define.Layer.Ground)
                {
                    player.MoveTo(hit.point);  //�浹�� ��ǥ�� �÷��̾� ĳ���� �̵�
                };
            };
        };
    }

    /// <summary>Ű���� �Է� �̵�</summary>
    void KeyMove()
    {   //                                              ����                         ����
        Vector2 keyInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (keyInput.magnitude > 1.0f) //Ű �Է� ����2 ��ǥ ���̰� 1���� ū ���  ex)�밢�� ���̰� ��Ʈ2�� �� ���
        {
            keyInput.Normalize(); //1�� ����ȭ
        };

        if(keyInput.magnitude > 0.1f)  //Ű �Է� ���� 2 ���̰� 0.1���� ū ��� �̵�
        {
            Vector3 cameraForward = Camera.main.transform.forward;  //ī�޶� �� ����
            cameraForward.y = 0.0f;//���� �� ����
            cameraForward.Normalize();//1�� ����ȭ
        
            Vector3 cameraRight = Camera.main.transform.right;  //ī�޶� ������ ����
            cameraRight.y = 0.0f;//���� �� ����
            cameraRight.Normalize();//1�� ����ȭ

            cameraForward *= keyInput.y;  //Ű���� WS �Է°� ����
            cameraRight *= keyInput.x;  //Ű���� AD �Է°� ����

            Vector3 calculatedLocation = cameraForward + cameraRight;  //���� ��ǥ
            calculatedLocation += player.transform.position; //���� ��ǥ�� �÷��̾��� ���� ��ġ�� ����

            player.SetTarget(null);//Ű���� �̵����̹Ƿ� ������ Ÿ���� ���
            player.MoveTo(calculatedLocation); //�÷��̾���ġ���� ���� ��ǥ�� ���ݾ� �̵�
        };
    }

    /// <summary>Ű���� �Է����� ��ų ����</summary>
    void SkillInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("�÷��̾�: ���潺 ����");
            player.Casting(Define.SkillState.Defense);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("�÷��̾�: ���Ž� ����");
            player.Casting(Define.SkillState.Smash);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("�÷��̾�: ī���� ����");
            player.Casting(Define.SkillState.Counter);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("�÷��̾�: ��ų ���, �ĺ����� ��ȯ");
            player.Casting(Define.SkillState.Combat);
        }


    }
}


