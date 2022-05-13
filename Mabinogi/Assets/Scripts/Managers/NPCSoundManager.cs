using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> NPC ���� �Ŵ��� </summary>
public class NPCSoundManager : MonoBehaviour
{
    [SerializeField]
    /// <summary> NPC ȿ���� ������ҽ� </summary>
    AudioSource npcEffectPlayer;

    [Tooltip("�� ¢��")]
    [SerializeField]
    /// <summary> �� ¢�� </summary>
    AudioClip dog01_natural_stand_offensive;

    [Tooltip("���� ¢��")]
    [SerializeField]
    /// <summary> ���� ¢�� </summary>
    AudioClip wolf01_natural_stand_offensive;

    [Tooltip("�� ���")]
    [SerializeField]
    /// <summary> �� ��� </summary>
    AudioClip sheep;

    [Tooltip("�� ����")]
    [SerializeField]
    /// <summary> �� ���� </summary>
    AudioClip chicken_fly;

    //����:GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.dog01_natural_stand_offensive);//�� ¢�� ȿ����
    /// <summary> ���� ����Ʈ ���</summary>
    public void PlaySfxPlayer(Define.NPCSoundEffect audioClipName)
    {
        switch (audioClipName)
        {
            case Define.NPCSoundEffect.dog01_natural_stand_offensive:
                npcEffectPlayer.clip = dog01_natural_stand_offensive;
                break;
            case Define.NPCSoundEffect.wolf01_natural_stand_offensive:
                npcEffectPlayer.clip = wolf01_natural_stand_offensive;
                break;
            case Define.NPCSoundEffect.sheep:
                npcEffectPlayer.clip = sheep;
                break;
            case Define.NPCSoundEffect.chicken_fly:
                npcEffectPlayer.clip = chicken_fly;
                break;
        }

        npcEffectPlayer.Play();
    }
}
