using UnityEngine;

public class Skill : ScriptableObject
{
    public string Name;

    public virtual void Execute()
    {
        Debug.Log($"{Name} ��ų�� ����޽��ϴ�.");
    }

}
