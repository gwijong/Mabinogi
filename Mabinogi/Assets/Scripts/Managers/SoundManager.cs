using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �÷��̾� ���� �Ŵ��� </summary>
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    /// <summary> ����� ������ҽ� </summary>
    public AudioSource bgmPlayer;

    [SerializeField]
    /// <summary> ȿ���� ������ҽ� </summary>
    public AudioSource effectPlayer;

    [Tooltip("���� �������")]
    [SerializeField]
    /// <summary> ���� ������� </summary>
    AudioClip naoBgm;

    [Tooltip("��� �������")]
    [SerializeField]
    /// <summary> ��� ������� </summary>
    AudioClip goroBgm;

    [Tooltip("Ÿ����ũ �������")]
    [SerializeField]
    /// <summary> Ÿ����ũ ������� </summary>
    AudioClip tarlachBgm;

    [Tooltip("�ҿｺƮ�� �������")]
    [SerializeField]
    /// <summary> �ҿｺƮ�� ������� </summary>
    AudioClip soulstreamBgm;

    [Tooltip("��Ʈ�� �������")]
    [SerializeField]
    /// <summary> ��Ʈ�� ������� </summary>
    AudioClip introBgm;

    [Tooltip("���� �������")]
    [SerializeField]
    /// <summary> ���� ������� </summary>
    AudioClip worldBgm;

    [Tooltip("���� �������")]
    [SerializeField]
    /// <summary> ���� ������� </summary>
    AudioClip dungeonBgm;

    [Tooltip("���� �������")]
    [SerializeField]
    /// <summary> ���� ������� </summary>
    AudioClip bossBgm;

    [Tooltip("��� �������")]
    [SerializeField]
    /// <summary> ��� ������� </summary>
    AudioClip dieBgm;

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

    [Tooltip("���� �� ����")]
    [SerializeField]
    /// <summary> ���� �� ���� </summary>
    AudioClip dungeon_door;

    [Tooltip("���� ����")]
    [SerializeField]
    /// <summary> ���� ���� </summary>
    AudioClip magic_standby;

    [Tooltip("���� �غ� �Ϸ�")]
    [SerializeField]
    /// <summary> ���� �غ� �Ϸ� </summary>
    AudioClip magic_ready;

    [Tooltip("���� �߻�")]
    [SerializeField]
    /// <summary> ���� �߻� </summary>
    AudioClip magic_lightning;

    private void Start()
    {
        GameManager.soundManager.PlayBgmPlayer((Define.Scene)UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    public void PlayBgmPlayer(Define.NPC audioClipName)
    {
        switch (audioClipName)
        {
            case Define.NPC.Nao:
                bgmPlayer.clip = naoBgm;
                break;
            case Define.NPC.Goro:
                bgmPlayer.clip = goroBgm;
                break;
            case Define.NPC.Tarlach:
                bgmPlayer.clip = tarlachBgm;
                break;
            default:
                bgmPlayer.clip = null;
                break;
        }
        bgmPlayer.Play();
    }

    public void PlayBgmPlayer(Define.Scene audioClipName)
    {
        switch (audioClipName)
        {
            case Define.Scene.Soulstream:
                bgmPlayer.clip = null;
                break;
            case Define.Scene.Intro:
                bgmPlayer.clip = introBgm;
                break;
            case Define.Scene.World:
                bgmPlayer.clip = worldBgm;
                break;
            case Define.Scene.Dungeon:
                bgmPlayer.clip = dungeonBgm;
                break;
            case Define.Scene.Boss:
                bgmPlayer.clip = bossBgm;
                break;
            case Define.Scene.Die:
                bgmPlayer.clip = dieBgm;
                break;
            default:
                bgmPlayer.clip = null;
                break;
        }
        bgmPlayer.Play();
    }
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
            case Define.SoundEffect.dungeon_door:
                effectPlayer.clip = dungeon_door;
                break;
            case Define.SoundEffect.magic_ready:
                effectPlayer.clip = magic_ready;
                break;
            case Define.SoundEffect.magic_standby:
                effectPlayer.clip = magic_standby;
                break;
            case Define.SoundEffect.magic_lightning:
                effectPlayer.clip = magic_lightning;
                break;
            default:
                effectPlayer.clip = null;
                break;
        }
        effectPlayer.Play();
    }
}