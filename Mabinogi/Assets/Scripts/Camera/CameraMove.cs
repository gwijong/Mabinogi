using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ī�޶� �̵� </summary>
public class CameraMove : MonoBehaviour
{
    Camera mainCamera;//���� ī�޶� �ڽ�
    public Transform cameraPivot; //�÷��̾� ĳ���Ϳ� ���ӵǾ��ִ� ī�޶� ������ ������Ʈ
    public float speed = 20; //ȸ�� �ӵ�
    Vector3 cameraRotator = Vector3.forward * 70; //z��ǥ�� �⺻������ 70���� �ڷ� ��ܼ� �ָ� ���̰� ��

    private void Start()
    {
        mainCamera = GetComponent<Camera>(); //�ڽ��� ī�޶� ������Ʈ ��������
        GameManager.update.UpdateMethod -= OnUpdate;
        GameManager.update.UpdateMethod += OnUpdate;
    }

    void OnUpdate()
    {
        if (Input.GetMouseButton(1)) //���콺 ��Ŭ�� �Է� ������
        {
            cameraRotator.x += Input.GetAxis("Mouse X"); //���콺 �¿� �Է� �޾Ƽ� cameraRotator.x ��ǥ�� ����
            cameraRotator.y -= Input.GetAxis("Mouse Y"); //���콺 ���� �Է� �޾Ƽ� cameraRotator.y ��ǥ�� ����

            //ī�޶� ������ �������� �ٶ� ������ �ʹ� ���� ������ ����
            cameraRotator.y = Mathf.Clamp(cameraRotator.y, (int)-25 / speed, (int)60 / speed); 

            //ī�޶� ȸ���ϴ°� �ƴ϶� ī�޶� �޷��ִ� �Ǻ��� ȸ����Ŵ
            cameraPivot.rotation = Quaternion.Euler(cameraRotator.y * speed, cameraRotator.x * speed, 0);
        };

        //���콺 �� �� �Է¹޾Ƽ� ī�޶� ���� �ܾƿ�
        cameraRotator.z += -Input.GetAxis("Mouse ScrollWheel") * speed * 2;
        cameraRotator.z = Mathf.Clamp(cameraRotator.z, 25, 80); //�ʹ� �����ų� ���� �ʵ��� ����
        mainCamera.fieldOfView = cameraRotator.z;
    }
}