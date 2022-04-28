using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ų UI ��ư�� ������ ��ų�� ĳ���õǵ��� �ϴ� ��ũ��Ʈ
public class SkillButton : MonoBehaviour
{
    public int skillNumber = 0; //Define�� ��ų enum

    private Character player;//�÷��̾�

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>(); //�÷��̾� ã�ƿ���
    }
    public void Casting() //��ų ������ ���� �� ���� Ȱ��ȭ
    {
        player.Casting((Define.SkillState)skillNumber);  //�ش� �������� ��ȣ ��ų ����
    }
}
