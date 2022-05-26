using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseButton : MonoBehaviour
{
    /// <summary> ������ ��� ��ư </summary>
    public void Use()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.eatfood);//��ư �ٿ� ȿ����
        FindObjectOfType<Inventory>().Use();
    }
    /// <summary> ������ ������ ��ư </summary>
    public void Divide()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//��ư �ٿ� ȿ����
        FindObjectOfType<Inventory>().Divide();
    }
    /// <summary> ������ ������ ��ư </summary>
    public void Drop()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//��ư �ٿ� ȿ����
        FindObjectOfType<Inventory>().Drop();
    }
}
