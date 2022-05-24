using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary> 인벤토리 창 열고 닫음</summary>
public class InvenOpen : MonoBehaviour
{
    /// <summary> 인벤토리 창</summary>
    public GameObject inven;
    /// <summary> 인벤토리 창이 열려있는지 체크</summary>
    public bool isOpen = false;
    // Update is called once per frame
    RectTransform invenPos;
    private void Start()
    {
        invenPos = inven.GetComponent<RectTransform>();
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
        StartCoroutine(InvenClose());
    }
    void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.I)) //I키를 누르면 소지품창 오픈
        {
            Open();
        }
    }

    /// <summary> 인벤토리 열기</summary>
    public void Open()
    {
        if (isOpen) //소지품창이 열려있으면 닫기
        {
            GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.inventory_close);//닫기 효과음
            Inventory.use.SetActive(false);//사용창 닫기
        }
        else //소지품창이 닫혀있으면 열기
        {
            GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.inventory_open);//열기 효과음
        }
        isOpen = !isOpen;
        inven.SetActive(isOpen);
    }

    public void Close()
    {

        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.inventory_close);//닫기 효과음
        Inventory.use.SetActive(false);//사용창 닫기
        inven.SetActive(false);
        isOpen = false;

    }

    IEnumerator InvenClose()
    {
        yield return null;
        Close();
    }
}
