using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> NPC Ÿ����ũ Ÿ�� ����</summary>
public class Tarlach : NPC
{
    protected override void Start()
    {
        base.Start();
        npc = Define.NPC.Tarlach;
    }
}
