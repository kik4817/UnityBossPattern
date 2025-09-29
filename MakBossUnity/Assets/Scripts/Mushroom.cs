using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mushroom : MonoBehaviour, IDamagable
{
    // Idle, Run, Stun, A1, A2, Hit, Die�� ���¿� ���� �ڵ带 ���δ� ����������Ѵ�.
    // ������ ���¸� �ڵ�� ���Ǹ� �س��� �����ϴ� ������� ����غ��� �ʹ�. ���� ���� �ӽ� finaite state machine, Behaviour tree

    BehaviorGraphAgent behaviorAgent;
    //[SerializeField] EnemyState startStats;
    [SerializeField] int MaxHealth = 100;
    Animator animator;


    [field:SerializeField]public int CurrentHealth {  get; private set; }

    public Action<bool> OnPatternStart;
    public Action<string, bool> OnSomeFucStart;
    public Action<int, int> OnHealthBarUpdate;

    [SerializeField] ParticleSystem rageVFX; // ������ ��������尡 �Ǿ��� �� �ߵ��ϴ� ����Ʈ


    private void Awake()
    {
        behaviorAgent = GetComponent<BehaviorGraphAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        behaviorAgent.SetVariableValue<EnemyState>("EnemyState", EnemyState.Idle);//Set �����ϴ�, Get ��������
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
            //�׾���
            animator.SetTrigger("Die");
            Debug.Log("�׾���.");
            behaviorAgent.SetVariableValue<EnemyState>("EnemyState", EnemyState.Die);

            // rpdla bossDeathEvent, ���� �ı�, ���� ����Ʈ �߻�
        }
    }

    // Bus<IRaiseStunEvent>.Raise()

    private bool IsStun()
    {
        // � ������ �� ������ �ɸ��°�? - ����ȭ
        // Ư�� ���� Ÿ�Կ� �´� ���·� ������ ���� �� Ȯ���� ���� ������ �ɸ� �� �ִ�.
        // ������ Ư�� ����� �����ϸ� ������ �ɸ���.

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
