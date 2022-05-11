using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    /// <summary>�÷��̾� ĳ���� �� �ϳ�</summary>
    public GameObject player;
    Character playerCharacter;
    /// <summary>���콺�� Ŭ���� Ÿ��</summary>
    Interactable target;
    /// <summary>Ground ���̾�� Enemy ���̾��� ���̾��ũ</summary>
    int layerMask = 1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Enemy | 1 << (int)Define.Layer.Livestock | 1 << (int)Define.Layer.Player;

    private void Awake()
    {   //���� �����Ҷ� ĳ���� �÷��̾� �����ϴ� ����
        PlayerSetting();
    }
    private void Start()
    {
        //�÷��̾� �±� ã�Ƽ� ������
        
        //������Ʈ �Ŵ����� Update�޼��忡 �����ֱ�
        GameManager.update.UpdateMethod -= OnUpdate;
        GameManager.update.UpdateMethod += OnUpdate;
    }

    void OnUpdate()
    {
        if(playerCharacter.die == true)
        {
            return;
        }
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
            playerCharacter.SetOffensive();
        };
    }

    /// <summary>���콺 �Է� �̵��̳� Ÿ�� ����</summary>
    void MouseInput()
    {

        if (EventSystem.current.IsPointerOverGameObject()) //UI ��ư ���� ��� ����
        {
            return;
        }

        //���� ��Ʈ�� Ű�� ���� ���·� ĳ���͸� ���콺 ��Ŭ���ϸ� �� ĳ���Ͱ� �÷��̾ ��
        if (Input.GetMouseButtonDown((int)Define.mouseKey.LeftClick) && Input.GetKey(KeyCode.LeftControl))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //ī�޶󿡼� ���콺��ǥ�� ���̸� ��
            RaycastHit hit;  //�浹 ��ü ���� �޾ƿ� ������ �����̳�

            if (Physics.Raycast(ray, out hit, 100f, layerMask))//����, �浹 ����, ���� �Ÿ�, ���̾��ũ
            {
                target = hit.collider.GetComponent<Character>();  //�浹�� ����� ĳ���͸� Ÿ�ٿ� �Ҵ� �õ�

                if (target != null)
                {
                    player.GetComponent<EnemyDummyAI>().enabled = true; //���� �÷��̾� ĳ������ �ΰ����� ����
                    if(player.tag == "Enemy")
                    {
                        player.layer = (int)Define.Layer.Enemy;  //���� �÷��̾� ĳ������ ���̾ ������ �ٲ�
                    }else if(player.tag == "Friendly")
                    {
                        player.layer = (int)Define.Layer.Livestock;//�����̸� ĳ������ ���̾ �������� �ٲ�
                    }
                    player.GetComponentInChildren<SkillUI>().GetComponent<Button>().enabled = false; //���� ĳ������ ��ǳ�� ������ ��ų����ϴ� ��� ����
                    player = hit.collider.gameObject;  //���콺 ��Ŭ������ ������ �÷��̾� ĳ���͸� �÷��̾�� ����
                    player.GetComponent<EnemyDummyAI>().stopCoroutine();//�ΰ����� �ڷ�ƾ ����
                    PlayerSetting();

                }
            };
            return;
        }

        if (Input.GetMouseButtonDown((int)Define.mouseKey.LeftClick))  //���콺 ��Ŭ�� �ԷµǸ�
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //ī�޶󿡼� ���콺��ǥ�� ���̸� ��
            RaycastHit hit;  //�浹 ��ü ���� �޾ƿ� ������ �����̳�

            if (Physics.Raycast(ray, out hit, 100f, layerMask))//����, �浹 ����, ���� �Ÿ�, ���̾��ũ
            {
                target = hit.collider.GetComponent<Interactable>();  //�浹�� ����� ĳ���͸� Ÿ�ٿ� �Ҵ� �õ�

                //ĳ���� ��� �Ҵ� ����            �浹�� ����� ���̸�
                if(!playerCharacter.SetTarget(target) && hit.collider.gameObject.layer == (int)Define.Layer.Ground)
                {
                    playerCharacter.MoveTo(hit.point);  //�浹�� ��ǥ�� �÷��̾� ĳ���� �̵�
                };
            };
        };
    }

    /// <summary>�÷��̾� ĳ���ͷ� ��ȯ</summary>
    void PlayerSetting()
    {
        player.layer = (int)Define.Layer.Player;  //�÷��̾��� ���̾ �÷��̾�� ����
        playerCharacter = player.GetComponent<Character>();  //�÷��̾��� ĳ���� ������Ʈ�� ������
        player.GetComponentInChildren<SkillUI>().GetComponent<Button>().enabled = true; //�÷��̾��� ��ǳ�� ������ ��ų����ϴ� ��� ����
        player.GetComponent<EnemyDummyAI>().enabled = false; //�ΰ����� ����
        GetComponentInChildren<CameraPivot>().following_object = player.transform; //ī�޶� �÷��̾ �����ϵ��� ��
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

            Vector3 calculatedLocation = cameraForward*5 + cameraRight*5;  //���� ��ǥ�� 5�� ������. ����޽� StoppingDistance�� 2�̹Ƿ� 2 �̻��̾����
            calculatedLocation += player.transform.position; //���� ��ǥ�� �÷��̾��� ���� ��ġ�� ����

            playerCharacter.SetTarget(null);//Ű���� �̵����̹Ƿ� ������ Ÿ���� ���
            playerCharacter.MoveTo(calculatedLocation); //�÷��̾���ġ���� ���� ��ǥ�� ���ݾ� �̵�
        };
    }

    /// <summary>Ű���� �Է����� ��ų ����</summary>
    void SkillInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("�÷��̾�: ���潺 ����");
            playerCharacter.Casting(Define.SkillState.Defense);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("�÷��̾�: ���Ž� ����");
            playerCharacter.Casting(Define.SkillState.Smash);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("�÷��̾�: ī���� ����");
            playerCharacter.Casting(Define.SkillState.Counter);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("�÷��̾�: ��ų ���, �ĺ����� ��ȯ");
            playerCharacter.Casting(Define.SkillState.Combat);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("�÷��̾�: ��ų ���, �ĺ����� ��ȯ");
            playerCharacter.Casting(Define.SkillState.Combat);
        }
    }
}


