using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class Skillexecutor : MonoBehaviour
{
    List<Skill> currentSkill = new();

    [SerializeField] Skill[] startSkill;

    private void Start()
    {
        for (int i = 0; i < startSkill.Length; i++)
        {
            currentSkill.Add(startSkill[i]);
        }
    }

    public void AddSkill(Skill skill)
    {
        currentSkill.Add(skill);
    }

    public void RemoveSkill(Skill skill)
    {
        currentSkill.Remove(skill);
    }

    public void ExcuteSkill(int index)
    {
        //currentSkill(index).execute();
    }
}
