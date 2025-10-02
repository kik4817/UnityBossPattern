using UnityEngine;

[CreateAssetMenu(fileName = "StrikeSkill", menuName = "ScriptableObject/Skill/StrikeSkill")]
public class StrikeSkill : Skill
{
    public override void Execute()
    {
        base.Execute();

        Debug.Log("Deal 2d6 damage to the target");
    }
}
