using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> NPC ��� Ÿ�� ���� </summary>
public class Goro : NPC
{
    protected override void Start()
    {
        base.Start();
        npc = Define.NPC.Goro;
    }
}
