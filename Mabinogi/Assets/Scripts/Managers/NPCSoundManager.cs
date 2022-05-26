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

    [Tooltip("�� �ٿ�")]
    [SerializeField]
    /// <summary> �� �ٿ� </summary>
    AudioClip dog01_natural_blowaway;

    [Tooltip("�� �±�")]
    [SerializeField]
    /// <summary> �� ¢�� </summary>
    AudioClip dog01_natural_hit;

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

    [Tooltip("�� �ٿ�")]
    [SerializeField]
    /// <summary> �� �ٿ� </summary>
    AudioClip chicken_down;

    [Tooltip("�� �±�")]
    [SerializeField]
    /// <summary> �� �±� </summary>
    AudioClip chicken_hit;

    [Tooltip("�� �������")]
    [SerializeField]
    /// <summary> �� ������� </summary>
    AudioClip bear01_natural_stand_offensive;

    [Tooltip("�� ���Ž�")]
    [SerializeField]
    /// <summary> �� ���Ž� </summary>
    AudioClip bear01_natural_attack_smash;

    [Tooltip("�� ī����")]
    [SerializeField]
    /// <summary> �� ī���� </summary>
    AudioClip bear01_natural_attack_counter;

    [Tooltip("�� �ٿ�")]
    [SerializeField]
    /// <summary> �� �ٿ� </summary>
    AudioClip bear01_natural_blowaway;

    [Tooltip("�� �±�")]
    [SerializeField]
    /// <summary> �� �±� </summary>
    AudioClip bear01_natural_hit;

    [Tooltip("�� �������")]
    [SerializeField]
    /// <summary> �� ������� </summary>
    AudioClip golem01_woo;
    
    //����:GameManager.npcSoundManager.PlaySfxPlayer(Define.NPCSoundEffect.dog01_natural_stand_offensive);//�� ¢�� ȿ����
    /// <summary> ���� ����Ʈ ���</summary>
    public void PlaySfxPlayer(Define.NPCSoundEffect audioClipName)
    {
        switch (audioClipName)
        {
            case Define.NPCSoundEffect.none:
                npcEffectPlayer.clip = null;  //�ƹ� �Ҹ��� �ȳ�
                break;
            case Define.NPCSoundEffect.dog01_natural_stand_offensive:
                npcEffectPlayer.clip = dog01_natural_stand_offensive;  //�� ¢��
                break;
            case Define.NPCSoundEffect.dog01_natural_blowaway:
                npcEffectPlayer.clip = dog01_natural_blowaway;  
                break;
            case Define.NPCSoundEffect.dog01_natural_hit:
                npcEffectPlayer.clip = dog01_natural_hit;  
                break;

            case Define.NPCSoundEffect.wolf01_natural_stand_offensive:
                npcEffectPlayer.clip = wolf01_natural_stand_offensive; //���� ¢��
                break;

            case Define.NPCSoundEffect.sheep:
                npcEffectPlayer.clip = sheep; //�� ���
                break;

            case Define.NPCSoundEffect.chicken_fly:
                npcEffectPlayer.clip = chicken_fly; 
                break;
            case Define.NPCSoundEffect.chicken_down:
                npcEffectPlayer.clip = chicken_down; 
                break;
            case Define.NPCSoundEffect.chicken_hit:
                npcEffectPlayer.clip = chicken_hit; 
                break;

            case Define.NPCSoundEffect.bear01_natural_stand_offensive:
                npcEffectPlayer.clip = bear01_natural_stand_offensive; 
                break;
            case Define.NPCSoundEffect.bear01_natural_attack_smash:
                npcEffectPlayer.clip = bear01_natural_attack_smash; 
                break;
            case Define.NPCSoundEffect.bear01_natural_attack_counter:
                npcEffectPlayer.clip = bear01_natural_attack_counter; 
                break;
            case Define.NPCSoundEffect.bear01_natural_blowaway:
                npcEffectPlayer.clip = bear01_natural_blowaway; 
                break;
            case Define.NPCSoundEffect.bear01_natural_hit:
                npcEffectPlayer.clip = bear01_natural_hit; 
                break;
            case Define.NPCSoundEffect.golem01_woo:
                npcEffectPlayer.clip = golem01_woo;
                break;
        }
        
        npcEffectPlayer.Play();
    }
}
