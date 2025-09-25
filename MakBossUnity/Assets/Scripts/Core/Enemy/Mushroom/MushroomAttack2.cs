using UnityEngine;

public class MushroomAttack2 : ActionBehavior
{
    public override void OnStart()
    {        
        IsPatternEnd = true;
    }

    public override void OnUpdate()
    {
        
    }
    public override void OnEnd()
    {
        
    }

    public override void OnStop()
    {
        base.OnStop();
    }
}
