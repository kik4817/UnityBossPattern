using UnityEngine;
using UnityEngine.Rendering;

// A is B가 아닌데 데미지를 받는 것을 공통으로 표현하고 싶다. => 플레이어, 몬스터 데미지를 입다. PlayerDamage, EnemyDamage, NPC
public interface IDamagable
{
    int CurrentHealth { get; } //프로퍼티로 만들어줄수 있다
    void TakeDamage(int damage); //DamageFactor
}
