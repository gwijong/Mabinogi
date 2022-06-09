using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ����, �Ǹ� ��ư UI </summary>
public class BuySellButton : MonoBehaviour
{
    /// <summary> ���� ��ư </summary>
    public void Buy()
    {
        Inventory.store.Buy();
        transform.parent.gameObject.SetActive(false); //����â ����
    }
    /// <summary> �Ǹ� ��ư</summary>
    public void Sell()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.gen_button_down);//��ư �ٿ� ȿ����
        Inventory.store.Sell();
        transform.parent.gameObject.SetActive(false);  //�Ǹ�â ����
    }
}
