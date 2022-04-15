using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput_Old : MonoBehaviour
{
    //�÷��̾� ĳ���͸� �����ϱ� ���� ����� �Է��� ����
    //������ �Է°��� �ٸ� ������Ʈ�� ����� �� �ֵ��� ����
    public string front = "Front";  //�� �������� ���� �Է��� �̸�
    public string back = "Back";  // �� �������� ���� �Է��� �̸�
    public string right = "Right";  //������ �������� ���� �Է��� �̸�
    public string left = "Left";  // ���� �������� ���� �Է��� �̸�
    public string defenseButtonName = "Defense";  // ���潺 ��ų�� ���� �Է� ��ư �̸�
    public string smashButtonName = "Smash";  // ���Ž� ��ų�� ���� �Է� ��ư �̸�
    public string counterButtonName = "CounterAttack"; // ī���;��� ��ų�� ���� �Է� ��ư �̸�
    public bool Front { get; private set; }  //������ �� ������ �Է°�
    public bool Back { get; private set; }  //������ �� ������ �Է°�
    public bool Right { get; private set; }  //������ ������ ������ �Է°�
    public bool Left { get; private set; }  //������ ���� ������ �Է°�
    public bool Defense { get; private set; } //������ ���潺 ��ư �Է°�
    public bool Smash { get; private set; }  //������ ���Ž� ��ư �Է°�
    public bool Counter { get; private set; }  //������ ī���;��� ��ư �Է°�

    // �������� ����� �Է��� ����
    void Update()
    {
        //���ӿ��� ���¿����� ����� �Է��� �������� ����
        if (Manager.manager != null && Manager.manager.isGameover)
        {
            Front = false;
            Back = false;
            Right = false;
            Left = false;
            Defense = false;
            Smash = false;
            Counter = false;
            return;
        }
        //�� �Է� ����
        Front = Input.GetButton(front);
        //�� �Է� ����
        Back = Input.GetButton(back);
        //������ �Է� ����
        Right = Input.GetButton(right);
        //���� �Է� ����
        Left = Input.GetButton(left);
        //���潺 �Է� ����
        Defense = Input.GetButtonDown(defenseButtonName);
        //���Ž� �Է� ����
        Smash = Input.GetButtonDown(smashButtonName);
        //ī���� �Է� ����
        Counter = Input.GetButtonDown(counterButtonName);
    }
}