using UnityEngine;
using UnityEngine.Rendering;

// A is B�� �ƴѵ� �������� �޴� ���� �������� ǥ���ϰ� �ʹ�. => �÷��̾�, ���� �������� �Դ�. PlayerDamage, EnemyDamage, NPC
public interface IDamagable
{
    int CurrentHealth { get; } //������Ƽ�� ������ټ� �ִ�
    void TakeDamage(int damage); //DamageFactor
}
