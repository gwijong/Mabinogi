using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ���� �ΰ����� </summary>
public class EnemyDummyAI : AI
{
    /// <summary> ��(ĳ���� ������Ʈ) </summary>
    public Character enemyCharacter; 
    /// <summary> �ΰ����� ���� üũ </summary>
    bool aiStart = false; 
    /// <summary> �ΰ������� ������ ���� ��ų ��ȣ </summary>
    int skillNum; 
    /// <summary> AI �ڷ�ƾ �Ҵ��� ���� </summary>
    IEnumerator dummyAICoroutine;
    /// <summary> ���� �ڷ�ƾ �Ҵ��� ���� </summary>
    IEnumerator searchCoroutine;

    float progress = 0;

    protected override void Start()
    {
        base.Start();
        Setting();
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
        //player = GameObject.FindGameObjectWithTag("Player"); //�÷��̾� ������Ʈ ã�ƿ�
        //playerCharacter = player.GetComponent<Character>();//�÷��̾� ĳ���� ��������
        StartCoroutine(searchCoroutine);//UpdatePath �ڷ�ƾ�� �ѹ� ���۵Ǹ� 1�� �������� ���� �ݺ� ����� 
    }

    public void OnUpdate()
    {
        //�÷��̾ �ʹ� �ָ� ��������
        if ((enemyCharacter != null && (enemyCharacter.transform.position - gameObject.transform.position).magnitude > 20))
        {
            Reset();
        }

        if (character.isRespawnAIStart == true) //ĳ���Ͱ� ������������
        {
            character.isRespawnAIStart = false;
            Reset();
            aiStart = false; //�ΰ����� ����
            StartCoroutine(searchCoroutine); //���� ����

        }

        if (character.die == true)//���� ������
        {
            aiStart = true;  //�ΰ����� �ڷ�ƾ�� ������� �ʵ��� �ΰ����� �ڷ�ƾ�� �����ߴٰ� ����
            stopCoroutine(); //�ΰ����� �ڷ�ƾ ����
            Reset();
            return;
        }

        if (aiStart == false) //�ΰ����� �ڷ�ƾ�� ���۵��� �ʾ�����
        {
            aiStart = true; //�ΰ����� ���� bool�� true�� ����
            dummyAICoroutine = null;
            dummyAICoroutine = DummyAI(); //�ΰ����� �ڷ�ƾ �Ҵ�
            StartCoroutine(dummyAICoroutine); //�ΰ����� �ڷ�ƾ ����
        }


        if (enemyCharacter != null && enemyCharacter.die == true) //���� ������
        {
            Reset();
        }

    }
    /// <summary> ���� ���� </summary>
    public void Setting()
    {
        aiStart = false;//aiStart�� false�� ���� �ΰ������� ������ �� �ְ� �Ѵ�.
        dummyAICoroutine = DummyAI(); //�ڷ�ƾ ������ AI �ڷ�ƾ �Ҵ�
        searchCoroutine = UpdatePath();
    }

    /// <summary> �ڷ�ƾ ���� </summary>
    public void stopCoroutine()
    {
        if (dummyAICoroutine != null)
            StopCoroutine(dummyAICoroutine);
        if (searchCoroutine != null)
            StopCoroutine(searchCoroutine);
    }

    /// <summary> ���� �ΰ����� </summary>
    IEnumerator DummyAI()
    {
        character.Casting(Define.SkillState.Combat);//�ϴ� ��ų�� �⺻�������� ����
        skillNum = Random.Range(0, 4);  //��ų ��
        if (enemyCharacter != null)
        {
            character.Casting((Define.SkillState)skillNum);  //��ų ����

            yield return new WaitForSeconds(Random.Range(1f, 3.0f));   // 1�ʺ��� 3�� ���

            switch (skillNum)
            {
                case (int)Define.SkillState.Combat:
                    character.SetTarget(enemyCharacter);  //������ų�̸� ����
                    break;
                case (int)Define.SkillState.Defense:
                    {
                        int patrolTime = Random.Range(200, 500);
                        float randomDistance = Random.Range(8.0f, 12.0f);
                        for(int i= 0; i< patrolTime && character.GetloadedSkill().type == Define.SkillState.Defense && enemyCharacter != null; i++)
                        {
                            Vector3 currentPos = new Vector3(Mathf.Sin(progress), 0, Mathf.Cos(progress));
                            currentPos *= randomDistance;
                            character.MoveTo(enemyCharacter.transform.position + currentPos);
                            progress += 0.015f;
                            yield return new WaitForSeconds(0.02f);
                        }
                        yield return new WaitForSeconds(1.2f);
                        character.SetTarget(enemyCharacter);
                        yield return new WaitForSeconds(1.2f);
                        character.SetTarget(enemyCharacter);
                        yield return new WaitForSeconds(1.2f);
                        character.SetTarget(enemyCharacter);
                        break;
                    }                    
                case (int)Define.SkillState.Smash:
                    character.SetTarget(enemyCharacter);  //������ų�̸� ����
                    break;
                case (int)Define.SkillState.Counter:
                    break;
            }
        }       
        yield return new WaitForSeconds(Random.Range(1f, 3.0f)); //1�ʺ��� 3�� ���
        character.SetTarget(null); //Ÿ�� ����
        aiStart = false; //�ڷ�ƾ ����� �غ�
    }



    /// <summary> �÷��̾� ����� AI ��ų �⺻������ ����� �ڷ�ƾ </summary>
    public void Reset()
    {
        enemyCharacter = null; //�� ĳ���͸� ����
        character.SetTarget(null); //Ÿ�� ����
        character.Casting(Define.SkillState.Combat); //�ĺ� ��ų�� ��ȯ
        character.SetOffensive(false); //�ϻ���� ��ȯ
    }

    /// <summary> ���� </summary>
    void Patrol(float range)
    {
        Vector3 movePos = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range)); //�����ϰ� �̵��� ����
        character.MoveTo(character.spawnPos + movePos); //��Ȱ ������ ���� �̵�    
    }

    /// <summary> 5�ʸ��� �ݺ�����Ǵ� �÷��̾ ã�� �ڷ�ƾ </summary>
    private IEnumerator UpdatePath()
    {      
        while (!character.die )//�� ĳ����(Enemy)�� ��� ������
        {
            if(enemyCharacter == null)
            {
                List<Character> enemyList = GetEnemyInRange(30f);//������ 30�� �� �ȿ� �� ĳ���͸� ����Ʈ�� ��ƿ�
                if (enemyList.Count > 0 ) //���� ������
                {                     
                    enemyCharacter = enemyList[Random.Range(0,enemyList.Count)]; //�� ����Ʈ�� i��° ���� enemyCharacter�� �Ҵ���
                    character.TargetLookAt(enemyCharacter); // ���� ����
                }
                else
                {
                    Reset();
                    Patrol(5.0f);
                }
            };
            yield return new WaitForSeconds(Random.Range(1.0f,4.0f)); //1���� 4�� ���� �ݺ� ����            
        }
    }
}
