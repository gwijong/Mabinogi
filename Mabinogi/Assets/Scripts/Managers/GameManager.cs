using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ���� �� �Ѱ��� �ִ� �Ŵ��� </summary>
public class GameManager : MonoBehaviour
{
    /// <summary> ���� �� �Ѱ��� �ִ� �Ŵ��� </summary>
    public static GameManager manager; //���ӸŴ��� ����ƽ �� �ϳ�
    /// <summary> ������Ʈ �Ŵ��� ��ü </summary>
    UpdateManager _update = new UpdateManager(); //������Ʈ �Ŵ��� ��ü ����
    /// <summary> ������Ʈ �Ŵ��� �б����� ������Ƽ </summary>
    public static UpdateManager update { get { return manager._update; } } //������Ʈ �Ŵ���
    /// <summary> ���� �Ŵ��� �ڵ����� ������Ƽ  </summary>
    public static SoundManager soundManager { get; private set; } //���� �Ŵ���
    /// <summary> NPC���� �Ŵ��� �ڵ����� ������Ƽ  </summary>
    public static NPCSoundManager npcSoundManager { get; private set; } //���� �Ŵ���
    /// <summary> ������ �Ŵ��� �ڵ����� ������Ƽ  </summary>
    public static ItemManager itemManager { get; private set; } //������Ʈ �Ŵ���
    void Awake() 
    {
        soundManager = GetComponent<SoundManager>(); //����Ŵ��� ������Ʈ �Ҵ�
        itemManager = GetComponent<ItemManager>(); //�����۸Ŵ��� ������Ʈ �Ҵ�
        npcSoundManager = GetComponent<NPCSoundManager>(); //NPC����Ŵ��� ������Ʈ �Ҵ�
        //�̱��� üũ
        if (manager == null) //�Ŵ����� ������
        {
            manager = this;  //�� GameManager ������Ʈ�� �Ŵ�����
        }
        else //�Ŵ����� �̹� ������
        {
            Destroy(gameObject);//�� ������Ʈ�� �ı��Ѵ�.
        }
    }

    /// <summary> �� ������Ʈ�� �� �ϳ� �ִ� ������Ʈ �޼��� </summary>
    private void Update()
    {
        update.OnUpdate();//��� OnUpdate �޼��尡 �����
    }
}
