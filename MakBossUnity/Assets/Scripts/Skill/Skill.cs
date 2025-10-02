using UnityEngine;

public class Skill : ScriptableObject
{
    public string Name;

    public virtual void Execute()
    {
        Debug.Log($"{Name} 스킬을 사용햇습니다.");
    }

}
