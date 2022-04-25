using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ��ȣ�ۿ��ϴ� ������Ʈ���� �ֻ��� �θ� ������Ʈ</summary>
public class Interactable : MonoBehaviour
{
    /// <summary>��ȣ�ۿ� �ϴ� ����� Ÿ�� ��ȯ None,Talk,Attack,Get</summary>
    public virtual Define.InteractType Interact(Interactable other)
    {
        return Define.InteractType.None; //�⺻��: �� Ŭ���ϰų� AI�� ���°� 
    }

    /// <summary> ������ �ƴ��� üũ(true�� ��)</summary>
    public static bool IsEnemy(Interactable A, Interactable B)
    {
        //        ^  xor
        //�����̴�  �����̴�   ��ȭ   x
        //�����̴�  ���̴�     ����   o
        //���̴�    �����̴�   ����   o
        //���̴�    ���̴�     ��ȭ   x

        //���� ������ �ٸ� ��쿡 ���̶�� ������
        if(HasGoodWill(A) ^ HasGoodWill(B) == true)  //���� �� ������ �ٸ� ���
        {
            return true; //���� �´�
        }
        else  //���� �� ������ ���� ���
        {
            return false; //���� �ƴϴ�
        }
    }


    /// <summary> ���̾ enemy�� �ƴϸ� ���� ������ ������ �ִٰ� �Ѵ�.</summary>
    public static bool HasGoodWill(Interactable target)
    {
        if(target.gameObject.layer == (int)Define.Layer.Enemy) //Ÿ���� ���̾ Enemy(��) �ΰ�?
        {
            return false; //���̸� ���۳� false
        }
        else
        {
            return true;  //���� �ƴϸ� ���ѳ� true
        }
        
    }
}
