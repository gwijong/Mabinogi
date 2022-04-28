using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummySkillControll : MonoBehaviour
{
    Character character;//�� ĳ����

    void Start()
    {
        character = GetComponent<Character>();
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
    }

    // Update is called once per frame
    void OnUpdate()
    {
        SkillInput();
    }


    /// <summary> 7�ĺ�  8���潺  9���Ž�  0ī���� </summary>
    void SkillInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            character.Casting(Define.SkillState.Defense);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            character.Casting(Define.SkillState.Smash);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            character.Casting(Define.SkillState.Counter);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            character.Casting(Define.SkillState.Combat);
        }


    }
}
