using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummyAI : MonoBehaviour
{
    Character character;//�ΰ����� ���� �� �ڽ�
    GameObject player; //��(�÷��̾�)
    Character playerCharacter; //��(�÷��̾� ĳ���� ������Ʈ)
    bool aiStart = false; //�ΰ����� ���� üũ
    int skillNum; //�ΰ������� ������ ���� ��ų ��ȣ
    IEnumerator coroutine;

    private void OnEnable()
    {
        aiStart = false;
        coroutine = DummyAI();
    }
    private void Start()
    {
        character = gameObject.GetComponent<Character>();//�� ���� ĳ���� ��������
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
        player = GameObject.FindGameObjectWithTag("Player"); //�÷��̾� ������Ʈ ã�ƿ�
        playerCharacter = player.GetComponent<Character>();//�÷��̾� ĳ���� ��������       
    }
    void OnUpdate()
    {
        if (character.die)//�ڽ� ĳ���� ����� �������� ����
        {
            aiStart = true;
            stopCoroutine(); //�ΰ����� �ڷ�ƾ ����
            return;
        }
        if (aiStart == false) //�ΰ����� �ڷ�ƾ�� ���۵��� �ʾ�����
        {
            aiStart = true;
            coroutine = DummyAI();
            StartCoroutine(coroutine); //�ΰ����� �ڷ�ƾ ����
        }
        LookAt();//Ÿ���� ��� �Ĵٺ�
    }

    /// <summary> �ڷ�ƾ ���� </summary>
    public void stopCoroutine()
    {
        if(coroutine!=null)
        StopCoroutine(coroutine);
    }

    /// <summary> ���� �ΰ����� </summary>
    IEnumerator DummyAI()
    {
        character.Casting(Define.SkillState.Combat);
        yield return new WaitForSeconds(2.0f); //�켱 2�� ���
        skillNum = Random.Range(0, 4);  //��ų ��
        character.Casting((Define.SkillState)skillNum);  //��ų ����
        yield return new WaitForSeconds(4.0f);   // 4�� ���
        if (skillNum != 1 && skillNum != 3)  // ������ų���� üũ
        {
            character.SetTarget(playerCharacter);  //������ų�̸� ����
        }
        yield return new WaitForSeconds(2.0f); //2�� ���
        character.SetTarget(null); //Ÿ�� ����
        aiStart = false; //�ڷ�ƾ ����� �غ�

    }

    /// <summary> ��(�÷��̾�)�� ��� �ٶ󺸴� �޼��� </summary>
    void LookAt()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //�÷��̾� ������Ʈ ã�ƿ�
        playerCharacter = player.GetComponent<Character>();//�÷��̾� ĳ���� ��������
        Vector3 look = player.transform.position;//�÷��̾� ��ġ
        look.y = transform.position.y; //�� �Ʒ��� �Ĵٺ��� ����, ȸ���� ��
        transform.LookAt(look);
    }
}
