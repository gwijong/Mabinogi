using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideLine : MonoBehaviour
{
    LineRenderer line;
    public static Character targetCharacter;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.widthMultiplier = 0.01f;      
    }
    
    // �ϻ� ������� �����ϰ� ���̾� ����ũ ����� �÷��̾� ĳ������ ĳ���� �ڽ��� üũ ���ϰ�
    
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Character[] characters = GameObject.FindObjectsOfType<Character>();
            float neardistance = 100000;
            Character nearCharacter = null;
            foreach(Character current in characters)
            {
                //���⼭ ���� ���¿� ���� ���� �±� Ȯ���ϰ� �ʿ� ������ ����, �÷��̾� ĳ���͸� ���� �����ؼ� ��Ƽ���� �ѱ�
                float currentDistance = (Input.mousePosition - Camera.main.WorldToScreenPoint(current.transform.position)).magnitude;
                if(currentDistance< neardistance)
                {
                    neardistance = currentDistance;
                    nearCharacter = current;                
                }
            }
            targetCharacter = nearCharacter;
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            targetCharacter = null;
        }

        Vector3[] pos = new Vector3[line.positionCount];
        if (targetCharacter == null)
        {
            line.SetPositions(pos);
            return;
        }
        Vector3 targetCenter = targetCharacter.transform.position;
        targetCenter.y += targetCharacter.nameYpos * 1f;
        Vector3 standard = Camera.main.WorldToScreenPoint(targetCenter);
        
        pos[0] = Input.mousePosition;
        float startAngle = Mathf.Atan2(pos[0].x - standard.x, pos[0].y - standard.y);

        for (int i = 1; i < pos.Length; i++)
        {
            float currentValue = Mathf.PI * 2 * (i-1) / (line.positionCount - 2);
            currentValue += startAngle;
            Vector3 currentPosition = new Vector3(Mathf.Sin(currentValue), Mathf.Cos(currentValue));
            currentPosition *= 100.0f * targetCharacter.nameYpos*0.45f;
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
