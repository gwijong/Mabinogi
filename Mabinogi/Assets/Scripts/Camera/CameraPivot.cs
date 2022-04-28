using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    public Transform following_object; //따라다닐 플레이어 오브젝트

    private void Start()
    {
        following_object = GameObject.FindGameObjectWithTag("Player").transform; 
        GameManager.update.UpdateMethod -= OnUpdate;//업데이트 매니저의 Update 메서드에 일감 몰아주기
        GameManager.update.UpdateMethod += OnUpdate;
    }
    private void OnUpdate()
    {
        Vector3 pos = transform.position;
        //따라다닐 플레이어 오브젝트로 이 오브젝트 계속 이동
        transform.position = Vector3.Lerp(pos, following_object.position, 0.4f); 
    }
}
