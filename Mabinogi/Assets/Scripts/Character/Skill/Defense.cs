using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : Skill
{
    /*
    public SkillData skillData;
    protected Character character;
    protected Animator ani;
    */
    public override void SkillUse(Character enemyTarget)
    {
        if (enemyTarget.currentSkillId == (int)Character.SkillId.combat)  //�� ������ ���
        {
            character.AniOff();
            ani.SetBool("Defense", true);
            enemyTarget.Freeze(skillData.StiffnessTime);
        }
    }
}
