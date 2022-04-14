using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : Skill
{
    /*
    public SkillData skillData;
    protected Character character;
    protected Animator ani;
     */
    public override void SkillUse(Character enemyTarget)
    {
        if (enemyTarget.currentSkillId == Define.SkillState.Defense)  //����ڰ� ���潺�� ����� ���
        {
            enemyTarget.GetComponent<Defense>().SkillUse(character);
            return;//�ƹ��ϵ� ���� �ʰ� ������ ���潺���� ó���Ѵ�.
        }
        else if(enemyTarget.currentSkillId == Define.SkillState.Counter)
        {
            enemyTarget.GetComponent<CounterAttack>().SkillUse(character);
            return;//�ƹ��ϵ� ���� �ʰ� ������ ī���Ϳ��� ó���Ѵ�.
        }
        else
        {
            character.AniOff();
            ani.SetBool("Combat", true);
            enemyTarget.Hit(character.maxPhysicalStrikingPower, character.minPhysicalStrikingPower, 
            skillData.Coefficient, character.balance, skillData.StiffnessTime, skillData.DownGauge);
        }        
    }
}
