using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummySkill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SkillInput();
    }

    void SkillInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Debug.Log("��: ���潺 ����");
            this.GetComponent<Character>().Casting(Define.SkillState.Defense);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Debug.Log("��: ���Ž� ����");
            this.GetComponent<Character>().Casting(Define.SkillState.Smash);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log("��: ī���� ����");
            this.GetComponent<Character>().Casting(Define.SkillState.Counter);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Debug.Log("��: ��ų ���, �ĺ����� ��ȯ");
            this.GetComponent<Character>().Casting(Define.SkillState.Combat);
        }


    }
}
