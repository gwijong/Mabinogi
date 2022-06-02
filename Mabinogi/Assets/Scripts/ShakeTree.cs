using UnityEngine;
using System.Collections;
/// <summary>���� ���� �������� ����߸��� ��ũ��Ʈ</summary>
public class ShakeTree : Hitable
{
    public Define.Item[] items; //������ �����۵�
    
    public override bool TakeDamage(Character from)
    {
        StopCoroutine("Shake");//���� ���� ���� �ڷ�ƾ ����
        StartCoroutine("Shake");//���� ���� �ڷ�ƾ ����

        //������ ��� ������ �� �ϳ��� �������� ��� ���� ��ġ�� ����
        GameManager.itemManager.DropItem(items[Random.Range(0, items.Length)], 1);

        return true;      
    }

    /// <summary>���� ����</summary>
    IEnumerator Shake()
    {
        GameManager.soundManager.PlaySfxPlayer(Define.SoundEffect.punch_hit, transform.position) ;
        for (int i = 0; i<10; i++)
        {
            if(i%2 == 0)
            {
                transform.position = transform.position + new Vector3(0.05f, 0, 0.05f);
            }
            else
            {
                transform.position = transform.position + new Vector3(-0.05f, 0,-0.05f);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}


