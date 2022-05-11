using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ��ȣ�ۿ��ϴ� ������Ʈ���� �ֻ��� �θ� ������Ʈ</summary>
public class Interactable : MonoBehaviour
{
    /// <summary>��ȣ�ۿ� �ϴ� ����� Ÿ�� ��ȯ None,Talk,Attack,Get,Sheeping</summary>
    public virtual Define.InteractType Interact(Interactable other)
    {
        if(this.gameObject.layer == (int)Define.Layer.Item)
        {
            return Define.InteractType.Get;
        }
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
        if((HasGoodWill(A) ^ HasGoodWill(B)) == true)  //���� �� ������ �ٸ� ���
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
        if(target.gameObject.layer == (int)Define.Layer.Enemy || target.gameObject.layer == (int)Define.Layer.Tree) //Ÿ���� ���̾ Enemy(��)�̰ų� ���� ����ΰ�?
        {
            return false; //���̸� ���۳� false
        }
        else
        {
            return true;  //���� �ƴϸ� ���ѳ� true
        }     
    }

    /// <summary> ���� �÷��̾�� ������ ������ üũ�Ѵ�</summary>
    public static bool IsSheep(Interactable player, Interactable sheep)
    {
        Sheep sp = sheep.GetComponent<Sheep>();
        if (player.tag == "Player" && (sp != null))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsItem(Interactable item)
    {
        if(item.gameObject.layer == (int)Define.Layer.Item)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
