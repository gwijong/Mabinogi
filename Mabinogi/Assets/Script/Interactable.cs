using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ��ȣ�ۿ��ϴ� ������Ʈ���� �ֻ��� �θ� ������Ʈ</summary>
public class Interactable : MonoBehaviour
{
    public virtual Define.InteractType Interact(Interactable other) 
    { 
        return Define.InteractType.None; //Ŭ���ϰų� AI�� ���°�
    }

    public static bool IsEnemy(Interactable A, Interactable B)
    {
        //        ^  xor
        //�����̴�  �����̴�   ��ȭ   x
        //�����̴�  ���̴�     ����   o
        //���̴�    �����̴�   ����   o
        //���̴�    ���̴�     ��ȭ   x

        //���� ������ �ٸ� ��쿡 ���̶�� ������
        return HasGoodWill(A) ^ HasGoodWill(B);
    }

    //���̾ enemy�� �ƴϸ� ���� ������ ������ �ִٰ� ��
    public static bool HasGoodWill(Interactable target)
    {
        return target.gameObject.layer != (int)Define.Layer.Enemy;
    }
}
