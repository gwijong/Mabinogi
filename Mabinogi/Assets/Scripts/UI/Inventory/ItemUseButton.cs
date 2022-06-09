using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ������ ��Ŭ�� �� ������ ��ư�� </summary>
public class ItemUseButton : MonoBehaviour
{
    /// <summary> ������ ��� ��ư </summary>
    public void Use()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.eatfood);//��ư �ٿ� ȿ����
        FindObjectOfType<PlayerInventory>().Use();
    }
    /// <summary> ������ ������ ��ư </summary>
    public void Divide()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//��ư �ٿ� ȿ����
        FindObjectOfType<PlayerInventory>().Divide();
    }
    /// <summary> ������ ������ ��ư </summary>
    public void Drop()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//��ư �ٿ� ȿ����
        FindObjectOfType<PlayerInventory>().Drop();
    }
}
