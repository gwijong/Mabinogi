using System;
using UnityEngine;

/// <summary> ������Ʈ �޼���� Ŭ���� </summary>
public class UpdateManager
{
    /// <summary> ��� ������Ʈ�� �����Ǵ� �׼� </summary>
    public Action UpdateMethod = null; 

    public void OnUpdate()
    {
        if (UpdateMethod != null)
        {
            UpdateMethod.Invoke();
        }
    }
}
