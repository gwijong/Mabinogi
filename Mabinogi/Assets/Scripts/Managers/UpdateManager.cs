using System;
using UnityEngine;

/// <summary> ������Ʈ �޼���� Ŭ���� </summary>
public class UpdateManager
{
    /// <summary> ��� Update �޼��尡 �����Ǵ� �׼� </summary>
    public Action UpdateMethod = null;

    /// <summary> GameManager�� Update() ���� ����� </summary>
    public void OnUpdate()
    {
        if (UpdateMethod != null) //UpdateMethod�� �����ϴ� �޼��尡 ���� �����ϸ�
        {
            UpdateMethod.Invoke(); //������Ʈ �޼��� ����
        }
    }
}
