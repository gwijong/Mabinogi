using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ��ȣ�ۿ��ϴ� ������Ʈ���� �ֻ��� �θ� ������Ʈ</summary>
public class Interactable : MonoBehaviour
{
    public virtual Define.InteractType Interact() 
    { 
        return Define.InteractType.None; //Ŭ���ϰų� AI�� ���°�
    }
}
