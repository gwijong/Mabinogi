using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interactable : MonoBehaviour  //��ȣ�ۿ��ϴ� ������Ʈ���� ���� �θ� ������Ʈ
{
    public virtual Define.InteractType Interact() 
    { 
        return Define.InteractType.None; //Ŭ���ϰų� AI�� ���°�
    }
}
