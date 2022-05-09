using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummyAI : MonoBehaviour
{
    //public LayerMask whatIsTarget;
    /// <summary> ���� ��� ���̾� ����ũ </summary>
    int layerMask = 1 << (int)Define.Layer.Enemy | 1 << (int)Define.Layer.Livestock | 1 << (int)Define.Layer.Player;
    /// <summary> �ΰ����� ���� �� �ڽ� </summary>
    Character character;
    /// <summary> ��(�÷��̾�) </summary>
    GameObject player; 
    /// <summary> ��(�÷��̾� ĳ���� ������Ʈ) </summary>
    Character playerCharacter; 
    /// <summary> �ΰ����� ���� üũ </summary>
    bool aiStart = false; 
    /// <summary> �ΰ������� ������ ���� ��ų ��ȣ </summary>
    int skillNum; 
    /// <summary> AI �ڷ�ƾ �Ҵ��� ���� </summary>
    IEnumerator coroutine;
    private void OnEnable()//������Ʈ�� Ȱ��ȭ�Ǹ�
    {
        aiStart = false;//aiStart�� false�� ���� �ΰ������� ������ �� �ְ� �Ѵ�.
        coroutine = DummyAI(); //�ڷ�ƾ ������ AI �ڷ�ƾ �Ҵ�
    }
    private void Start()
    {
        character = gameObject.GetComponent<Character>();//�� ���� ĳ���� ��������
        GameManager.update.UpdateMethod -= OnUpdate;//������Ʈ �Ŵ����� Update �޼��忡 �ϰ� �����ֱ�
        GameManager.update.UpdateMethod += OnUpdate;
        //player = GameObject.FindGameObjectWithTag("Player"); //�÷��̾� ������Ʈ ã�ƿ�
        //playerCharacter = player.GetComponent<Character>();//�÷��̾� ĳ���� ��������
        StartCoroutine("UpdatePath");//UpdatePath �ڷ�ƾ�� �ѹ� ���۵Ǹ� 0.25�� �������� ���� �ݺ� ����� 
    }
    void OnUpdate()
    {
        if(playerCharacter == null) //���� ������ Ż��
        {
            return;
        }

        if (playerCharacter.die == true) //���� ������
        {
            StartCoroutine("Reset"); //�� ���� �ڷ�ƾ ����
        }
        //�ڽ� ĳ���� ����ϸ� �������� ����
        if (character.die == true)//���� ������
        {
            aiStart = true;  //�ΰ����� �ڷ�ƾ�� ������� �ʵ��� �ΰ����� �ڷ�ƾ�� �����ߴٰ� ����
            stopCoroutine(); //�ΰ����� �ڷ�ƾ ����
            StartCoroutine("Reset"); //�� ���� �ڷ�ƾ ����
            return;
        }
        
        if (aiStart == false) //�ΰ����� �ڷ�ƾ�� ���۵��� �ʾ�����
        {
            aiStart = true; //�ΰ����� ���� bool�� true�� ����
            coroutine = DummyAI(); //�ΰ����� �ڷ�ƾ �Ҵ�
            StartCoroutine(coroutine); //�ΰ����� �ڷ�ƾ ����
        }
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
        yield return new WaitForSeconds(Random.Range(0.5f,3.0f)); //�켱 2�� ���
        skillNum = Random.Range(0, 4);  //��ų ��
        if (playerCharacter != null)
        {
            character.Casting((Define.SkillState)skillNum);  //��ų ����
        }
        yield return new WaitForSeconds(Random.Range(2f, 6.0f));   // 4�� ���

        if (skillNum == (int)Define.SkillState.Defense)//������ ��ų�� ���潺�̸�
        {
            yield return new WaitForSeconds(1.5f); //1.5�� ���
            character.SetTarget(playerCharacter); //���� �õ�
            yield return new WaitForSeconds(1.5f); //1.5�� ���
            character.SetTarget(playerCharacter); //���� �õ�
            yield return new WaitForSeconds(1.5f); //1.5�� ���
            character.SetTarget(playerCharacter); //���� �õ�
        }

        if (skillNum != (int)Define.SkillState.Counter)  // ī���;����� �ƴϸ� ���� �����̹Ƿ�
        {

            if (playerCharacter != null)
            {
                character.SetTarget(playerCharacter);  //������ų�̸� ����
            }
        }
        yield return new WaitForSeconds(Random.Range(0.5f, 3.0f)); //2�� ���
        character.SetTarget(null); //Ÿ�� ����
        aiStart = false; //�ڷ�ƾ ����� �غ�

    }

    /// <summary> ��(�÷��̾�)�� ��� �ٶ󺸴� �޼��� </summary>
    void LookAt()
    {
        if (gameObject.layer == (int)Define.Layer.Player) //�� �ڽ��� �÷��̾��̸� ����(����)�� ��� �Ĵٺ��� �ʴ´�
        {
            return;
        }
        if(player != null && playerCharacter.die == false) // ��(�÷��̾�)�� �����ϰ� ������� �ʾ�����
        {
            Vector3 look = player.transform.position;//�÷��̾� ��ġ
            look.y = transform.position.y; //�� �Ʒ��� �Ĵٺ��� ����, ȸ���� ��
            transform.LookAt(look); //�Ĵٺ���
        }
    }

    /// <summary> �÷��̾� ����� AI ��ų �⺻������ ����� �ڷ�ƾ </summary>
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(3.0f); //3�� ���
        character.SetTarget(null); //Ÿ�� ����
        character.Casting(Define.SkillState.Combat); //�ĺ� ��ų�� ��ȯ
        character.SetOffensive(false); //�ϻ���� ��ȯ
    }

    /// <summary> 0.25�ʸ��� �ݺ�����Ǵ� �÷��̾ ã�� �ڷ�ƾ </summary>
    private IEnumerator UpdatePath()
    {
        while(!character.die)//�� ĳ����(Enemy)�� ��� ������
        {
            if((player != null && (player.transform.position - gameObject.transform.position).magnitude > 30)) //�÷��̾ �ʹ� �ָ� ��������
            {
                player = null;  //  �÷��̾� Ÿ���� Ǭ��
                playerCharacter = null; //  �÷��̾� Ÿ���� Caracter ������Ʈ�� Ǭ��
                character.Casting(Define.SkillState.Combat); //�ĺ� ��ų�� ��ȯ
                character.SetOffensive(false); //�ϻ���� ��ȯ
            }
            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, layerMask); //������ 10¥�� ���׶� �ݶ��̴� �����

            //��� �ݶ��̴��� ��ȸ�ϸ鼭 ��� �ִ� Player ã��
            for (int i = 0; i < colliders.Length; i++)
            {
                //�ݶ��̴��κ��� Character ������Ʈ ��������
                Character pCharacter = colliders[i].GetComponent<Character>();

                //Character ������Ʈ�� �����ϸ�, ���̾ �÷��̾ �����̰� ���� �����̸�
                if (pCharacter != null && pCharacter.die ==false && (pCharacter.gameObject.layer == (int)Define.Layer.Player|| pCharacter.gameObject.layer == (int)Define.Layer.Livestock) && character.tag=="Enemy")
                {
                    //���� ����� �ش� playerCharacter ����
                    player = pCharacter.gameObject;
                    playerCharacter = player.GetComponent<Character>();
                    LookAt();//Ÿ���� ��� �Ĵٺ�

                    //for �� ���� ��� ����
                    break;
                }
                //Character ������Ʈ�� �����ϸ�, ���̾ ���̰� ���� �����̸�
                if (pCharacter != null && pCharacter.die == false && (pCharacter.gameObject.layer == (int)Define.Layer.Enemy) && character.tag == "Friendly")
                {
                    //���� ����� �ش� playerCharacter ����
                    player = pCharacter.gameObject;
                    playerCharacter = player.GetComponent<Character>();
                    LookAt();//Ÿ���� ��� �Ĵٺ�

                    //for �� ���� ��� ����
                    break;
                }
            }
            yield return new WaitForSeconds(0.25f); //0.25�ʸ��� �ݺ� ����
        }
    }
}
