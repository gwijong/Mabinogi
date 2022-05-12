using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummyAI : AI
{
    //public LayerMask whatIsTarget;
    /// <summary> ���� ��� ���̾� ����ũ </summary>
    int layerMask = 1 << (int)Define.Layer.Enemy | 1 << (int)Define.Layer.Livestock | 1 << (int)Define.Layer.Player;    
    /// <summary> ��(ĳ���� ������Ʈ) </summary>
    Character enemyCharacter; 
    /// <summary> �ΰ����� ���� üũ </summary>
    bool aiStart = false; 
    /// <summary> �ΰ������� ������ ���� ��ų ��ȣ </summary>
    int skillNum; 
    /// <summary> AI �ڷ�ƾ �Ҵ��� ���� </summary>
    IEnumerator dummyAICoroutine;
    /// <summary> ���� �ڷ�ƾ �Ҵ��� ���� </summary>
    IEnumerator searchCoroutine;
    private void OnEnable()//������Ʈ�� Ȱ��ȭ�Ǹ�
    {
        aiStart = false;//aiStart�� false�� ���� �ΰ������� ������ �� �ְ� �Ѵ�.
        dummyAICoroutine = DummyAI(); //�ڷ�ƾ ������ AI �ڷ�ƾ �Ҵ�
        searchCoroutine = UpdatePath();
    }
    protected override void Start()
    {
        base.Start();
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
        //player = GameObject.FindGameObjectWithTag("Player"); //�÷��̾� ������Ʈ ã�ƿ�
        //playerCharacter = player.GetComponent<Character>();//�÷��̾� ĳ���� ��������
        StartCoroutine(searchCoroutine);//UpdatePath �ڷ�ƾ�� �ѹ� ���۵Ǹ� 1�� �������� ���� �ݺ� ����� 
    }
    void OnUpdate()
    {

        if(enemyCharacter == null) //���� ������ Ż��
        {
            return;
        }

        if (enemyCharacter.die == true) //���� ������
        {
            Reset();
            return;
        }
        //�ڽ� ĳ���� ����ϸ� �������� ����
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
            dummyAICoroutine = DummyAI(); //�ΰ����� �ڷ�ƾ �Ҵ�
            StartCoroutine(dummyAICoroutine); //�ΰ����� �ڷ�ƾ ����
        }
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
        yield return new WaitForSeconds(Random.Range(0.5f,3.0f)); //�켱 2�� ���
        skillNum = Random.Range(0, 4);  //��ų ��
        if (enemyCharacter != null)
        {
            character.Casting((Define.SkillState)skillNum);  //��ų ����
        }
        yield return new WaitForSeconds(Random.Range(2f, 6.0f));   // 4�� ���

        if (skillNum == (int)Define.SkillState.Defense)//������ ��ų�� ���潺�̸�
        {
            yield return new WaitForSeconds(1.5f); //1.5�� ���
            character.SetTarget(enemyCharacter); //���� �õ�
            yield return new WaitForSeconds(1.5f); //1.5�� ���
            character.SetTarget(enemyCharacter); //���� �õ�
            yield return new WaitForSeconds(1.5f); //1.5�� ���
            character.SetTarget(enemyCharacter); //���� �õ�
        }

        if (skillNum != (int)Define.SkillState.Counter)  // ī���;����� �ƴϸ� ���� �����̹Ƿ�
        {

            if (enemyCharacter != null)
            {
                character.SetTarget(enemyCharacter);  //������ų�̸� ����
            }
        }
        yield return new WaitForSeconds(Random.Range(0.5f, 3.0f)); //2�� ���
        character.SetTarget(null); //Ÿ�� ����
        aiStart = false; //�ڷ�ƾ ����� �غ�

    }



    /// <summary> �÷��̾� ����� AI ��ų �⺻������ ����� �ڷ�ƾ </summary>
    void Reset()
    {
        enemyCharacter = null; //�� ĳ���͸� ����
        character.SetTarget(null); //Ÿ�� ����
        character.Casting(Define.SkillState.Combat); //�ĺ� ��ų�� ��ȯ
        character.SetOffensive(false); //�ϻ���� ��ȯ
    }

    /// <summary> 5�ʸ��� �ݺ�����Ǵ� �÷��̾ ã�� �ڷ�ƾ </summary>
    private IEnumerator UpdatePath()
    {
        while(!character.die)//�� ĳ����(Enemy)�� ��� ������
        {
            //�÷��̾ �ʹ� �ָ� ��������
            if ((enemyCharacter != null && (enemyCharacter.transform.position - gameObject.transform.position).magnitude > 30)) 
            {
                Reset();
            }
            List<Character> enemyList = GetEnemyInRange(10f);//������ 10�� �� �ȿ� �� ĳ���͸� ����Ʈ�� ��ƿ�
            if (enemyList.Count > 0) //���� ������
            {
                enemyCharacter = enemyList[0]; //�� ����Ʈ�� 0��° ���� enemyCharacter�� �Ҵ���              
            }         
             yield return new WaitForSeconds(1f); //1�ʸ��� �ݺ� ����
        }
    }
}
