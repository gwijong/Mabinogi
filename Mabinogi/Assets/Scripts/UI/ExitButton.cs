using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ���� ���� ��ư </summary>
public class ExitButton : MonoBehaviour
{
    /// <summary> ���� ���� </summary>
    public void Exit()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//��ư �ٿ� ȿ����
        Application.Quit();
    }
}
