using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 상호작용하는 오브젝트들의 최상위 부모 오브젝트</summary>
public class Interactable : MonoBehaviour
{
    /// <summary>상호작용 하는 대상의 타입 반환 None,Talk,Attack,Get,Sheeping</summary>
    public virtual Define.InteractType Interact(Interactable other)
    {
        if(this.gameObject.layer == (int)Define.Layer.Item)//상호작용 대상이 아이템이면
        {
            return Define.InteractType.Get; //상호작용 타입을 줍기 리턴
        }
        return Define.InteractType.None; //기본값: 땅 클릭하거나 AI가 쓰는거 
    }

    /// <summary> 적인지 아닌지 체크(true면 적)</summary>
    public static bool IsEnemy(Interactable A, Interactable B)
    {
        //        ^  xor
        //동맹이다  동맹이다   대화   x
        //동맹이다  적이다     전투   o
        //적이다    동맹이다   전투   o
        //적이다    적이다     대화   x

        //둘의 성향이 다른 경우에 적이라고 간주함
        if((HasGoodWill(A) ^ HasGoodWill(B)) == true)  //적과 내 성향이 다를 경우
        {
            if(A.gameObject.layer == (int)Define.Layer.NPC|| B.gameObject.layer == (int)Define.Layer.NPC)//한쪽이라도 대화 NPC일 경우 적이 아니다
            {
                return false; //적이 아니다
            }
            return true; //적이 맞다
        }
        else  //적과 내 성향이 같을 경우
        {
            return false; //적이 아니다
        }
    }


    /// <summary> 레이어가 enemy가 아니면 좋은 의지를 가지고 있다고 한다.</summary>
    public static bool HasGoodWill(Interactable target)
    {
        if(target.gameObject.layer == (int)Define.Layer.Enemy || target.gameObject.layer == (int)Define.Layer.Tree) //타겟의 레이어가 Enemy(적)이거나 때릴 대상인가?
        {
            return false; //적이면 나쁜놈 false
        }
        else
        {
            return true;  //적이 아니면 착한놈 true
        }     
    }

    /// <summary> 내가 플레이어고 상대방이 양인지 체크한다</summary>
    public static bool IsSheep(Interactable player, Interactable sheep)
    {
        Sheep sp = sheep.GetComponent<Sheep>(); //양 스크립트 가져오기 시도
        if (player.tag == "Player" && (sp != null))//조작중인 캐릭터가 플레이어이고 타겟이 양일 경우
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary> 타겟이 아이템인지 체크한다</summary>
    public bool IsItem(Interactable item)
    {
        if(item.gameObject.layer == (int)Define.Layer.Item) //타겟이 아이템 레이어이면
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
