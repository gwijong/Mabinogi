using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> NPC ƾ Ÿ�� ���� </summary>
public class Tin : NPC
{
    protected override void Start()
    {
        base.Start();
        npc = Define.NPC.Tin;
    }
}
