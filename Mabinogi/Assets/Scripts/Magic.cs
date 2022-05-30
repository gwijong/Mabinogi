using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    void Start()
    {
        //������Ʈ �Ŵ����� Update�޼��忡 �����ֱ�
        GameManager.update.UpdateMethod -= OnUpdate;
        GameManager.update.UpdateMethod += OnUpdate;
    }

    void OnUpdate()
    {
        Follow();
    }

    public void Follow()
    {
        Vector3 followPos = new Vector3(target.position.x, target.position.y, target.position.z);
        //����ٴ� �÷��̾� ������Ʈ�� �� ������Ʈ ��� �̵�
        transform.position = Vector3.Lerp(gameObject.transform.position, followPos, 4f*Time.deltaTime);

        if((followPos - gameObject.transform.position).magnitude < 2)
        {
            gameObject.SetActive(false);
        }
    }
}
