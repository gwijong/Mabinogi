using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ȭ�� ���� ��� ��ų UI ��ư�� ������ ��ų�� ĳ���õǵ��� �ϴ� ��ũ��Ʈ </summary>
public class SkillButton : MonoBehaviour
{
    public int skillNumber = 0; //Define�� ��ų enum

    private Character player;//�÷��̾�

    public void Casting() //��ų ������ ���� �� ���� Ȱ��ȭ
    {
        player = GameManager.manager.GetComponent<PlayerController>().player.GetComponent<Character>(); //�������� ĳ���� ã�ƿ���
        player.Casting((Define.SkillState)skillNumber);  //�ش� �������� ��ȣ ��ų ����
    }
}
