using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> NPC ���� Ÿ�� ���� </summary>
public class Nao : NPC
{
    protected override void Start()
    {
        base.Start();
        npc = Define.NPC.Nao;
    }
}
