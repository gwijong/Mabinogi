using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    /// <summary> ����� ������ҽ� </summary>
    AudioSource bgmPlayer;

    [SerializeField]
    /// <summary> ȿ���� ������ҽ� </summary>
    AudioSource effectPlayer;
    
    [Tooltip("�ָ� ��Ÿ")]
    [SerializeField]
    /// <summary> �ָ� ��Ÿ </summary>
    AudioClip punch_hit;
    
    [Tooltip("�ٿ� �Ǵ� ��")]
    [SerializeField]
    /// <summary> �ٿ� �Ǵ� �� </summary>
    AudioClip punch_blow;
    
    [Tooltip("���潺 ���")]
    [SerializeField]
    /// <summary> ���潺 ��� </summary>
    AudioClip guard;
    
    [Tooltip("���� ����")]
    [SerializeField]
    /// <summary> ���� ���� </summary>
    AudioClip emotion_success;
    
    [Tooltip("���� ����")]
    [SerializeField]
    /// <summary> ���� ���� </summary>
    AudioClip emotion_fail;
    
    [Tooltip("���� �Ա�")]
    [SerializeField]
    /// <summary> ���� �Ա� </summary>
    AudioClip eatfood;
    
    [Tooltip("�ٿ� �ٴڿ� ������")]
    [SerializeField]
    /// <summary> �ٿ� �ٴڿ� ������ </summary>
    AudioClip down;
    
    [Tooltip("���� ���ñ�")]
    [SerializeField]
    /// <summary> ���� ���ñ� </summary>
    AudioClip drinkpotion;
    
    [Tooltip("���� ����")]
    [SerializeField]
    /// <summary> ���� ���� </summary>
    AudioClip dungeon_monster_appear1;
    
    [Tooltip("��ų ���")]
    [SerializeField]
    /// <summary> ��ų ��� </summary>
    AudioClip skill_cancel;
    
    [Tooltip("��ų ����")]
    [SerializeField]
    /// <summary> ��ų ���� </summary>
    AudioClip skill_standby;
    
    [Tooltip("��ų �غ� �Ϸ�")]
    [SerializeField]
    /// <summary> ��ų �غ� �Ϸ� </summary>
    AudioClip skill_ready;

    [Tooltip("����ǰâ ����")]
    [SerializeField]
    /// <summary> ����ǰâ ���� </summary>
    AudioClip inventory_open;

    [Tooltip("����ǰâ �ݱ�")]
    [SerializeField]
    /// <summary> ����ǰâ �ݱ� </summary>
    AudioClip inventory_close;

    [Tooltip("��ư Ŭ��")]
    [SerializeField]
    /// <summary> ��ư Ŭ�� </summary>
    AudioClip gen_button_down;

    [Tooltip("������")]
    [SerializeField]
    /// <summary> ������ </summary>
    AudioClip character_levelup;

    //����: GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.skill_ready);//��ų �غ� �Ϸ� ȿ����
    /// <summary> ���� ����Ʈ ���</summary>
    public void PlaySfxPlayer(Define.SoundEffect audioClipName)
    {
        switch (audioClipName)
        {
            case Define.SoundEffect.punch_hit:
                effectPlayer.clip = punch_hit;
                break;
            case Define.SoundEffect.punch_blow:
                effectPlayer.clip = punch_blow;
                break;
            case Define.SoundEffect.guard:
                effectPlayer.clip = guard;
                break;
            case Define.SoundEffect.emotion_success:
                effectPlayer.clip = emotion_success;
                break;
            case Define.SoundEffect.emotion_fail:
                effectPlayer.clip = emotion_fail;
                break;
            case Define.SoundEffect.eatfood:
                effectPlayer.clip = eatfood;
                break;
            case Define.SoundEffect.down:
                effectPlayer.clip = down;
                break;
            case Define.SoundEffect.drinkpotion:
                effectPlayer.clip = drinkpotion;
                break;
            case Define.SoundEffect.dungeon_monster_appear1:
                effectPlayer.clip = dungeon_monster_appear1;
                break;
            case Define.SoundEffect.skill_cancel:
                effectPlayer.clip = skill_cancel;
                break;
            case Define.SoundEffect.skill_standby:
                effectPlayer.clip = skill_standby;
                break;
            case Define.SoundEffect.skill_ready:
                effectPlayer.clip = skill_ready;
                break;
            case Define.SoundEffect.inventory_open:
                effectPlayer.clip = inventory_open;
                break;
            case Define.SoundEffect.inventory_close:
                effectPlayer.clip = inventory_close;
                break;
            case Define.SoundEffect.gen_button_down:
                effectPlayer.clip = gen_button_down;
                break;
            case Define.SoundEffect.character_levelup:
                effectPlayer.clip = character_levelup;
                break;
        }
        effectPlayer.Play();
    }
}