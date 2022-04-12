using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //�÷��̾� ĳ���͸� �����ϱ� ���� ����� �Է��� ����
    //������ �Է°��� �ٸ� ������Ʈ�� ����� �� �ֵ��� ����
    public string forthBack = "Vertical";  //�յ� �������� ���� �Է��� �̸�
    public string leftRight = "Horizontal";  // �¿� �������� ���� �Է��� �̸�
    public string defenseButtonName = "Defense";  // ���潺 ��ų�� ���� �Է� ��ư �̸�
    public string smashButtonName = "Smash";  // ���Ž� ��ų�� ���� �Է� ��ư �̸�
    public string counterButtonName = "CounterAttack"; // ī���;��� ��ų�� ���� �Է� ��ư �̸�
    public float wsMove { get; private set; }  //������ �յ� ������ �Է°�
    public float adMove { get; private set; }  //������ �¿� ȸ�� �Է°�
    public bool defense { get; private set; } //������ ���潺 ��ư �Է°�
    public bool smash { get; private set; }  //������ ���Ž� ��ư �Է°�
    public bool counter { get; private set; }  //������ ī���;��� ��ư �Է°�

    // �������� ����� �Է��� ����
    void Update()
    {
        //���ӿ��� ���¿����� ����� �Է��� �������� ����
        if (Manager.manager != null && Manager.manager.isGameover)
        {
            wsMove = 0;
            adMove = 0;
            defense = false;
            smash = false;
            counter = false;
            return;
        }
        //�յ� �Է� ����
        wsMove = Input.GetAxis(forthBack);
        //�¿� �Է� ����
        adMove = Input.GetAxis(leftRight);
        //���潺 �Է� ����
        defense = Input.GetButton(defenseButtonName);
        //���Ž� �Է� ����
        smash = Input.GetButtonDown(smashButtonName);
        //ī���� �Է� ����
        counter = Input.GetButtonDown(counterButtonName);
    }
}