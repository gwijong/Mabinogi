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

    [Tooltip("Ʃ�丮�� �������")]
    [SerializeField]
    /// <summary> Ʃ�丮�� ������� </summary>
    AudioClip tutorialBgm;

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

    [Tooltip("���� ī����")]
    [SerializeField]
    /// <summary> ���� ī���� </summary>
    AudioClip wolf01_natural_attack_counter;

    [Tooltip("���� ���Ž�")]
    [SerializeField]
    /// <summary> ���� ���Ž� </summary>
    AudioClip wolf01_natural_attack_smash;

    [Tooltip("���� �ٿ�")]
    [SerializeField]
    /// <summary> ���� �ٿ� </summary>
    AudioClip wolf01_natural_down;

    [Tooltip("���� �±�")]
    [SerializeField]
    /// <summary> ���� �±� </summary>
    AudioClip wolf01_natural_hit;

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

    /// <summary> �ּ� �Ÿ� </summary>
    public float minDistance;
    /// <summary> �ִ� �Ÿ� </summary>
    public float maxDistance;

    private void Start()
    {
        GameManager.soundManager.PlayBgmPlayer((Define.Scene)UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary> npc�� ���� ������� ���</summary>
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

    /// <summary> ���� ���� ������� ���</summary>
    public void PlayBgmPlayer(Define.Scene audioClipName)
    {
        switch (audioClipName)
        {
            case Define.Scene.Soulstream:
                bgmPlayer.clip = null;
                break;
            case Define.Scene.Tutorial:
                bgmPlayer.clip = tutorialBgm;
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
    /// <summary> �Ÿ��� ���� ���� ũ�⸦ ���� ���� ����Ʈ ���</summary>
    public void PlaySfxPlayer(Define.SoundEffect audioClipName, Vector3 pos)
    {

        /*
        �ּҰŸ� 1        1
        �ִ�Ÿ� 0       20
        1 ~ 0   1 ~ 20
        result = input
            0 ~ 19
        result = input - min
        1 ~ 0    0 ~ 1
        result = (input - min) / (max - min); 

        1 ~ 0    1 ~ 0
        result = 1 - ((input - min) / (max - min));
         */
        float distanceVolume = (Camera.main.transform.position - pos).magnitude; //ī�޶�� �Ҹ��� ���� Ÿ�� ������ �Ÿ�
        distanceVolume = 1 - (distanceVolume - minDistance) / (maxDistance - minDistance); //������ Ÿ�� �Ÿ����� �ּ��� �۰� ����
        distanceVolume = Mathf.Clamp(distanceVolume, 0, 1); //0���� 1 ���� ������ Ư��
        PlaySfxPlayer(audioClipName, distanceVolume);
    }

    /// <summary> ����� ȿ���� ����</summary>
    public AudioClip GetClipByName(Define.SoundEffect audioClipName)
    {
        switch (audioClipName)
        {
            case Define.SoundEffect.punch_hit:                  return punch_hit;
            case Define.SoundEffect.punch_blow:                 return punch_blow;
            case Define.SoundEffect.guard:                      return guard;
            case Define.SoundEffect.emotion_success:            return emotion_success;
            case Define.SoundEffect.emotion_fail:               return emotion_fail;
            case Define.SoundEffect.eatfood:                    return eatfood;
            case Define.SoundEffect.down:                       return down;
            case Define.SoundEffect.drinkpotion:                return drinkpotion;
            case Define.SoundEffect.dungeon_monster_appear1:    return dungeon_monster_appear1;
            case Define.SoundEffect.skill_cancel:               return skill_cancel;
            case Define.SoundEffect.skill_standby:              return skill_standby;
            case Define.SoundEffect.skill_ready:                return skill_ready;
            case Define.SoundEffect.inventory_open:             return inventory_open;
            case Define.SoundEffect.inventory_close:            return inventory_close;
            case Define.SoundEffect.gen_button_down:            return gen_button_down;
            case Define.SoundEffect.character_levelup:          return character_levelup;
            case Define.SoundEffect.dungeon_door:               return dungeon_door;
            case Define.SoundEffect.magic_ready:                return magic_ready;
            case Define.SoundEffect.magic_standby:              return magic_standby;
            case Define.SoundEffect.magic_lightning:            return magic_lightning;
            case Define.SoundEffect.dog01_natural_stand_offensive: return dog01_natural_stand_offensive;
            case Define.SoundEffect.dog01_natural_blowaway: return dog01_natural_blowaway;
            case Define.SoundEffect.dog01_natural_hit: return dog01_natural_hit;
            case Define.SoundEffect.wolf01_natural_stand_offensive: return wolf01_natural_stand_offensive;
            case Define.SoundEffect.wolf01_natural_attack_counter: return wolf01_natural_attack_counter;
            case Define.SoundEffect.wolf01_natural_attack_smash: return wolf01_natural_attack_smash;
            case Define.SoundEffect.wolf01_natural_hit: return wolf01_natural_hit;
            case Define.SoundEffect.sheep: return sheep;
            case Define.SoundEffect.chicken_fly: return chicken_fly;
            case Define.SoundEffect.chicken_down: return chicken_down;
            case Define.SoundEffect.chicken_hit: return chicken_hit;
            case Define.SoundEffect.bear01_natural_stand_offensive: return bear01_natural_stand_offensive;
            case Define.SoundEffect.bear01_natural_attack_smash: return bear01_natural_attack_smash;
            case Define.SoundEffect.bear01_natural_attack_counter: return bear01_natural_attack_counter;
            case Define.SoundEffect.bear01_natural_blowaway: return bear01_natural_blowaway;
            case Define.SoundEffect.bear01_natural_hit: return bear01_natural_hit;
            case Define.SoundEffect.golem01_woo: return golem01_woo;
            default:                                            return null;
        }
    }
    /// <summary> ȿ���� ���</summary>
    public void PlaySfxPlayer(Define.SoundEffect audioClipName, float volume = 1.0f)
    {
        AudioClip clip = GetClipByName(audioClipName);
        PlaySfxPlayer(clip, volume);
    }

    /// <summary> ȿ���� ���</summary>
    public void PlaySfxPlayer(AudioClip clip, float volume = 1.0f)
    {
        if (clip != null)
        {
            effectPlayer.PlayOneShot(clip, volume * effectPlayer.volume);
        };
    }
}