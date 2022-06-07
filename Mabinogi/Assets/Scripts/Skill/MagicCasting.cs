using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCasting : MonoBehaviour
{
    public Transform character;

    public float yPosition;
    // ������.
    public float radius = 2.0f;
    // ȸ�� �ӵ�.
    public float angularVelocity = 40.0f;
    // ��ġ.
    public float angle = 0.0f;

    void Update()
    {
        // ȸ�� ����.
        angle += angularVelocity * Time.deltaTime;
        // ������ ��ġ.
        Vector3 offset = Quaternion.Euler(0.0f, angle, 0.0f) * new Vector3(0.0f, 0.0f, radius);
        // ����Ʈ ��ġ.
        transform.position = new Vector3(character.transform.position.x, yPosition, character.transform.position.z) + offset;
    }
}
