using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ���� �� �Ѱ��� �ִ� �Ŵ��� </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager manager; 

    UpdateManager _update = new UpdateManager(); //������Ʈ �Ŵ��� ��ü ����
    public static UpdateManager update { get { return manager._update; } } //������Ʈ �Ŵ��� ��ü �б����� ������Ƽ
    public static SoundManager soundManager { get; private set; } //���� �Ŵ���
    public static ItemManager itemManager { get; private set; } //������Ʈ �Ŵ��� ��ü �б����� ������Ƽ
    void Awake() 
    {
        soundManager = gameObject.GetComponent<SoundManager>(); //����Ŵ��� ������Ʈ �Ҵ�
        itemManager = gameObject.GetComponent<ItemManager>(); //�����۸Ŵ��� ������Ʈ �Ҵ�
        //�̱��� üũ
        if (manager == null)
        {
            manager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        update.OnUpdate();
    }
}
