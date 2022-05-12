using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    /// <summary> �ΰ����� ���� �� �ڽ� </summary>
    protected Character character;

    protected virtual void Start()
    {
        character = GetComponent<Character>();
    }

    /// <summary> ���� ���� ĳ���� ��������</summary>
    public List<Character> GetCharactersInRange(float range)
    {
        List<Character> result = new List<Character>();//���� ���� ĳ���� ����Ʈ
        //������ range¥�� ���׶� �ݶ��̴� ���� �浹�ϴ� �ݶ��̴��� colliders �迭�� �� �ִ´�.
        Collider[] colliders = Physics.OverlapSphere(transform.position, range); 
        foreach (Collider current in colliders) //colliders�迭 ũ�⸸ŭ �ݺ��ϸ鼭 ��� �ݶ��̴��� �˻��Ѵ�.
        {
            Character currentCharacter = current.GetComponent<Character>(); //���� �ݶ��̴��� Character��ũ��Ʈ ������Ʈ �Ҵ� �õ�
            if (currentCharacter != null) result.Add(currentCharacter);//���� ĳ���Ϳ� Character������Ʈ�� ������ result����Ʈ�� �߰�
        }
        return result; //ĳ���� ����Ʈ ��ȯ
    }

    /// <summary> ���� ���� �� ����Ʈ ����</summary>
    public List<Character> GetEnemyInRange(float range)
    {
        List<Character> result = new List<Character>(); //�� ����Ʈ�� ��(Enemy) ĳ���͸� �߰���
        List<Character> from = GetCharactersInRange(range);//���� ���� ��� ĳ����

        foreach(Character current in from)//���� ���� ��� ĳ���� ���ڸ�ŭ �ݺ�
        {
            if(Interactable.IsEnemy(character, current)) //�� ĳ���� �ڽŰ� ������ ��ȣ�ۿ��� ���̸�(IsEnemy�� true��)
            {
                result.Add(current);//�� ����Ʈ�� �߰�
            };
        };
        return result;
    }
}
