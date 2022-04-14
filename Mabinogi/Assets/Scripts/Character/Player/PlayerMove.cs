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
        if (character.stiffnessCount != 0 || character.State == Define.State.Die)
        {
			movePos = Vector3.zero;
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
                if (raycastHit.collider.gameObject.layer == 6)
                {
					character.target = raycastHit.collider.GetComponent<Character>();
				}			

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
            if (character.target != null&& character.target.State!=Define.State.Die&&Input.GetMouseButtonDown(0))
			{
				character.Attack();
            }
			gameObject.GetComponent<Animator>().SetBool("Move", false);
            movePos = Vector3.zero;
        }

		

	}
}
/*
  int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

	PlayerStat _stat;
	bool _stopSkill = false;


	public override void Init()
	{
		WorldObjectType = Define.WorldObject.Player;

		_stat = gameObject.GetComponent<PlayerStat>();

		Managers.Input.MouseAction -= OnMouseEvent;
		Managers.Input.MouseAction += OnMouseEvent;

		if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
        {
			Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
		}
		
			
	}


	protected override void UpdateMoving()
	{
		// ���Ͱ� �� �����Ÿ����� ������ ����
		if (_lockTarget != null)
		{
			_destPos = _lockTarget.transform.position;
			float distance = (_destPos - transform.position).magnitude;
			if (distance <= 1)
			{
				State = Define.State.Skill;
				return;
			}
		}

		// �̵�
		Vector3 dir = _destPos - transform.position;
		dir.y = 0;
		if (dir.magnitude < 0.1f)
		{
			State = Define.State.Idle;
		}
		else
		{

			Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized*100, Color.green);
			if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
			{
				if (Input.GetMouseButton(0) == false)
					State = Define.State.Idle;
				return;
			}
			float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
			transform.position += dir.normalized * moveDist;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
		}
	}


	protected override void UpdateSkill()
	{
		if (_lockTarget != null)
		{
			Vector3 dir = _lockTarget.transform.position - transform.position;
			Quaternion quat = Quaternion.LookRotation(dir);
			transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
		}
	}

	void OnHitEvent()
	{


		if(_lockTarget != null)
        {
			Stat targetStat = _lockTarget.GetComponent<Stat>();
			targetStat.OnAttacked(_stat);
		}

		if (_stopSkill)
		{
			State = Define.State.Idle;
		}
		else
		{
			State = Define.State.Skill;
		}
	}

	



	void OnMouseEvent(Define.MouseEvent evt)
	{
		switch (State)
		{
			case Define.State.Idle:
				OnMouseEvent_IdleRun(evt);
				break;
			case Define.State.Moving:
				OnMouseEvent_IdleRun(evt);
				break;
			case Define.State.Skill:
				{
					if (evt == Define.MouseEvent.PointerUp)
						_stopSkill = true;
				}
				break;
		}
	}

	void OnMouseEvent_IdleRun(Define.MouseEvent evt)
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
		Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

		switch (evt)
		{
			case Define.MouseEvent.PointerDown:
				{
					if (raycastHit)
					{
						_destPos = hit.point;
						State = Define.State.Moving;
						_stopSkill = false;

						if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
							_lockTarget = hit.collider.gameObject;
						else
							_lockTarget = null;
					}
				}
				break;
			case Define.MouseEvent.Press:
				{
					if (_lockTarget == null && raycastHit)
						_destPos = hit.point;
				}
				break;
			case Define.MouseEvent.PointerUp:
				_stopSkill = true;
				break;
		}
	}
 */