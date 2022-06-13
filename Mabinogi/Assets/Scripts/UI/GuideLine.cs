using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ���� ��Ʈ�� Ű ������ ������ ���̵���� </summary>
public class GuideLine : MonoBehaviour
{
    /// <summary> ���� ������ ������Ʈ </summary>
    LineRenderer line;
    /// <summary> ������ NPC </summary>
    public static Character targetCharacter;
    /// <summary> �÷��̾� ĳ���� </summary>
    Character player;
    /// <summary> ������ ������Ʈ </summary>
    Renderer rend;
    float startAngle;

    void Start()
    {
        rend = GetComponent<Renderer>(); //������ �Ҵ�
        line = GetComponent<LineRenderer>();//���η����� �Ҵ�
        line.widthMultiplier = 0.005f;//������ ����       
        line.textureMode = LineTextureMode.Tile; //���η����� ��带 Ÿ�Ϸ� �����ؼ� ���� �ݺ�
    }
    
    void LateUpdate()
    {
        Vector3[] pos = new Vector3[line.positionCount];

        if (FindObjectOfType<DialogTalk>().dark.gameObject.activeSelf == true)
        {
            line.SetPositions(pos);
            targetCharacter = null;
            return;
        }
        if (player != null)
        {
            if (player.die == true)
            {
                line.SetPositions(pos);
                targetCharacter = null;
                return;
            }
        }

        rend.material.mainTextureScale = new Vector2(Vector2.Distance(line.GetPosition(0), line.GetPosition(line.positionCount - 1)) / line.widthMultiplier, 1);
        rend.material.mainTextureScale = (rend.material.mainTextureScale * startAngle)/ rend.material.mainTextureScale*8f;

        if (Input.GetKeyDown(KeyCode.LeftControl)) // Ű���� ���� ��Ʈ�� Ű�� ������
        {

            player = PlayerController.controller.playerCharacter; //�÷��̾� ĳ���� �Ҵ�
            Character[] characters = GameObject.FindObjectsOfType<Character>();//��� ĳ���� �ܾ��
            float neardistance = 100000;//���콺 Ŀ���� ���� ����� NPC���� �Ÿ�
            Character nearCharacter = null; //���콺 Ŀ���� ���� ����� NPC Ÿ��;
            foreach(Character current in characters)
            {
                if (player.GetOffensive()) //�÷��̾ ��������̸�
                {
                    if (current.gameObject.layer == (int)Define.Layer.Enemy && current.die == false) //���̾ ���̰� ������� ���� ���
                    {
                        float currentDistance = (Input.mousePosition - Camera.main.WorldToScreenPoint(current.transform.position)).magnitude;
                        if (currentDistance < neardistance)
                        {
                            neardistance = currentDistance;//���� ����� �Ÿ� ����
                            nearCharacter = current;//���� ����� ĳ���� ����
                        }
                    }
                }
                else //�÷��̾ �ϻ����̸�
                {
                    //���̾ �����̳� NPC�̰� ������� �ʾҴٸ�
                    if (current.gameObject.layer == (int)Define.Layer.Livestock || current.gameObject.layer == (int)Define.Layer.NPC && current.die == false)
                    {
                        float currentDistance = (Input.mousePosition - Camera.main.WorldToScreenPoint(current.transform.position)).magnitude;
                        if (currentDistance < neardistance)
                        {
                            neardistance = currentDistance;//���� ����� �Ÿ� ����
                            nearCharacter = current;//���� ����� ĳ���� ����
                        }
                    }
                }
            }
            targetCharacter = nearCharacter;//���������� ����� ĳ���͸� Ÿ�� ĳ���ͷ� ����
            PlayerController.controller.target = targetCharacter;
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl)) //Ű���� ���� ��Ʈ�� Ű�� ����
        {
            targetCharacter = null; //Ÿ���� �����
        }

        if (targetCharacter == null)//Ÿ�� ĳ���Ͱ� ���� ���
        {
            line.SetPositions(pos);
            return;
        }
        Vector3 targetCenter = targetCharacter.transform.position;
        targetCenter.y += targetCharacter.nameYpos * 1f; //ĳ������ �̸� ���� Y��ǥ�� ���� ���� �߽� ����
        Vector3 standard = Camera.main.WorldToScreenPoint(targetCenter);
        
        pos[0] = Input.mousePosition;//���� ���� ���� ��ġ�� ���콺 �Էµ� ��ǥ
        startAngle = Mathf.Atan2(pos[0].x - standard.x, pos[0].y - standard.y);

        for (int i = 1; i < pos.Length; i++)
        {
            float currentValue = Mathf.PI * 2 * (i-1) / (line.positionCount - 2);
            currentValue += startAngle;
            Vector3 currentPosition = new Vector3(Mathf.Sin(currentValue), Mathf.Cos(currentValue));
            currentPosition *= 100.0f * targetCharacter.nameYpos*0.3f; //ĳ������ �̸� ���� Y��ǥ�� ����� ���� ũ�� ����
            //0�̸� 0 9�� 2PI
            pos[i] = standard + currentPosition;
        }

        for(int i = 0; i < pos.Length; i++)
        {
            pos[i].z = 1;
            pos[i] = Camera.main.ScreenToWorldPoint(pos[i]);
        }
        line.SetPositions(pos);
    }

}
