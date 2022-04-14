using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : Skill
{
    public override void SkillUse(Character enemyTarget)
    {
        if (enemyTarget.currentSkillId ==Define.SkillState.Counter)
        {
            enemyTarget.GetComponent<CounterAttack>().SkillUse(character);
            return;//�ƹ��ϵ� ���� �ʰ� ������ ī���Ϳ��� ó���Ѵ�.
        }
        character.AniOff();
        ani.SetBool("Smash", true);
        enemyTarget.Groggy(skillData.StiffnessTime);
        enemyTarget.Hit(character.maxPhysicalStrikingPower, character.minPhysicalStrikingPower,
        skillData.Coefficient, character.balance);
    }
}
