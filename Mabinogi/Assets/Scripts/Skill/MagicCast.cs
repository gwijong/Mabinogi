using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ���� ���� �� ĳ���� ������ ��� ���� ���� </summary>
public class MagicCast : MonoBehaviour
{
    /// <summary> ��� �� ������ </summary>
    public Transform character;
    /// <summary> ���� </summary>
    public float yPosition;
    /// <summary> ������ </summary>
    public float radius = 2.0f;
    /// <summary> ȸ�� �ӵ� </summary>
    public float angularVelocity = 40.0f;
    /// <summary> ȸ�� ���� </summary>
    public float angle = 0.0f;

    void Update()
    {
        // ȸ�� ����.
        angle += angularVelocity * Time.deltaTime;
        // ������ ��ġ.
        Vector3 offset = Quaternion.Euler(0.0f, angle, 0.0f) * new Vector3(radius, 0.0f, 0.0f); //Y���� �߽����� ���ӿ�����Ʈ�� ȸ��, ��� ���µ� ĳ���Ϳ��� ������ radius��ŭ ��;
        // ����Ʈ ��ġ.
        transform.position = new Vector3(character.transform.position.x, yPosition, character.transform.position.z) + offset;
    }
}
