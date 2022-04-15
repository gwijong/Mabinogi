using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash_Old : Skill_Old
{
    public override void SkillUse(Character_Old enemyTarget)
    {
        if (enemyTarget.currentSkillId ==Define.SkillState.Counter)
        {
            enemyTarget.GetComponent<CounterAttack_Old>().SkillUse(character);
            return;//�ƹ��ϵ� ���� �ʰ� ������ ī���Ϳ��� ó���Ѵ�.
        }
        character.AniOff();
        ani.SetBool("Smash", true);
        enemyTarget.Groggy(skillData.StiffnessTime);
        enemyTarget.Hit(character.maxPhysicalStrikingPower, character.minPhysicalStrikingPower,
        skillData.Coefficient, character.balance);
    }
}
