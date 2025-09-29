using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mushroom : MonoBehaviour, IDamagable
{
    // Idle, Run, Stun, A1, A2, Hit, Die이 상태에 따른 코드를 전부다 정의해줘야한다.
    // 각각의 상태를 코드로 정의를 해놓고 조립하는 방식으로 사용해보고 싶다. 유한 상태 머신 finaite state machine, Behaviour tree

    BehaviorGraphAgent behaviorAgent;
    //[SerializeField] EnemyState startStats;
    [SerializeField] int MaxHealth = 100;
    Animator animator;


    [field:SerializeField]public int CurrentHealth {  get; private set; }

    public Action<bool> OnPatternStart;
    public Action<string, bool> OnSomeFucStart;
    public Action<int, int> OnHealthBarUpdate;

    [SerializeField] ParticleSystem rageVFX; // 보스가 레이지모드가 되었을 때 발동하는 이펙트


    private void Awake()
    {
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        behaviorAgent.SetVariableValue<EnemyState>("EnemyState", EnemyState.Idle);//Set 세팅하다, Get 가져오다
        //behaviorAgent.SetVariableValue<Boolean>("IsPatternTrigger", true);
        CurrentHealth = MaxHealth;

        OnHealthBarUpdate?.Invoke(CurrentHealth, MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        OnHealthBarUpdate?.Invoke(CurrentHealth, MaxHealth);

        if(CurrentHealth < MaxHealth * 0.5f)
        {
            OnPatternStart?.Invoke(true);
        }

        if(IsStun())
        {
            StunRaise();
        }

        if (CurrentHealth <= 0)
        {
            //죽었다
            animator.SetTrigger("Die");
            Debug.Log("죽었다.");
            behaviorAgent.SetVariableValue<EnemyState>("EnemyState", EnemyState.Die);

            // rpdla bossDeathEvent, 몬스터 파괴, 폭발 이펙트 발생
        }
    }

    // Bus<IRaiseStunEvent>.Raise()

    private bool IsStun()
    {
        // 어떤 조건일 때 스턴이 걸리는가? - 무력화
        // 특정 무기 타입에 맞는 형태로 공격을 했을 때 확률에 따라서 스턴이 걸릴 수 있다.
        // 보스의 특정 기믹을 성공하면 스턴이 걸린다.

        int rand = UnityEngine.Random.Range(0, 101); //0 ~ 100

        if (rand <= 50)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    private void StunRaise()
    {
        animator.SetTrigger("Stun");
        behaviorAgent.SetVariableValue<EnemyState>("EnemyState", EnemyState.Stun);
    }

    private void Update()
    {
        if(Keyboard.current.tKey.isPressed)
        {
            TakeDamage(10);
        }
    }
    private void OnEnable()
    {
        OnPatternStart += HandlePatternStart;
        OnSomeFucStart += HandleSomeFucStart;
    }


    private void OnDisable()
    {
        OnPatternStart -= HandlePatternStart;
        OnSomeFucStart -= HandleSomeFucStart;
    }

    private void HandlePatternStart(bool enable)
    {
        behaviorAgent.SetVariableValue<Boolean>("IsPatternTrigger", enable);        

        if (rageVFX.isPlaying) {  return; }

        rageVFX.Play();
    }

    private void HandleSomeFucStart(string methodName, bool enable)
    {
        behaviorAgent.SetVariableValue<Boolean>(methodName, enable);
    }
}
