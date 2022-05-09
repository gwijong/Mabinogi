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
        List<Character> result = new List<Character>();
        //������ range¥�� ���׶� �ݶ��̴� �����
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach(Collider current in colliders)
        {
            Character currentCharacter = current.GetComponent<Character>();
            if (currentCharacter != null) result.Add(currentCharacter);
        }
        return result;
    }

    /// <summary> ���� ���� �� ����Ʈ ����</summary>
    public List<Character> GetEnemyInRange(float range)
    {
        List<Character> result = new List<Character>(); 
        List<Character> from = GetCharactersInRange(range);

        foreach(Character current in from)
        {
            if(Interactable.IsEnemy(character, current))
            {
                result.Add(current);
            };
        };

        return result;
    }
}
