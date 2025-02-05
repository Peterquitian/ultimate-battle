using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private string _nombre;
    [SerializeField] private int _id;
    [SerializeField] private int _defaultHealth;
    private int _maxHealth;
    [SerializeField] private int _health;
    [SerializeField] private int _defaultDamage;
    private int _maxDamage;
    [SerializeField] private int _Damage;
    [SerializeField] private int _movement;
    [SerializeField] int _layerMask;
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private List<Node> _node;

    [Header("UI")]
    [SerializeField] Sprite _imgCharacter;
    [SerializeField] string _history;
    [SerializeField] private CharacterUI _ui;

    [Header("VFX")]
    [SerializeField] private ParticleSystem _dust;
    [SerializeField] ParticleSystem _particlePrefab;
    private Character _target;
    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public ParticleSystem ParticleDust => _dust;
    public LayerMask LMask => _layerMask;
    public string Nombre => _nombre;
    public int Id => _id;
    public int DefaultHealth => _defaultHealth;
    public int DefaulAttack => _defaultDamage;
    public int Health => _health;
    public int Damage => _Damage;

    public CharacterMovement Movement => _characterMovement;

    public bool IsAlive { get; set; }

    public Animator Animator => _animator;

    public List<Node> Node => _node;
    public Sprite ImgCharacter => _imgCharacter;
    public string History => _history;
    public int RemainingMovement => _movement;

    private void Awake() {
        _layerMask = LayerMask.GetMask("InteractableLayer");
    }

    public void Setup(int addHealth, int addAttack)
    {
        _maxHealth = addHealth + _defaultHealth;
        _maxDamage = addAttack + _defaultDamage;

        _health = _maxHealth;
        _Damage = _maxDamage;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        IsAlive = true;
        OutOfTurn();
    }

    public void Setup()
    {
        _maxHealth = _defaultHealth;
        _maxDamage = _defaultDamage;

        _health = _maxHealth;
        _Damage = _maxDamage;
        IsAlive = true;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _characterMovement.Setup(_movement);
        UpdateMovementUI();
        OutOfTurn();
    }

    // Método para aplicar daño
    public void TakeDamage(int damage)
    {

        _health = Mathf.Clamp(_health - damage, 0, _maxHealth);

        _animator.Play("Damage");

        EffectAttack();

        float currenthealth = (float)_health/_maxHealth;
        Debug.Log(currenthealth);
        _ui.TakeDamage(currenthealth);

        Debug.Log(gameObject.name + $" Daño x ¡¡{damage}!!");
        if (_health < 1)
        {
            IsAlive = false;
            Die();

        }
    }
    public void IncreaseHealth(int value)
    {
        _health = Mathf.Clamp(_health + value, 0, _maxHealth);
    }

    public void InTurn()
    {
        foreach (var item in _node)
        {
            item.Inicializate();
        }
    }

    public void OutOfTurn()
    {
        foreach (var item in _node)
        {
            item.Hiden();
        }
    }


    private void Die()
    {
        Debug.Log(gameObject.name + " ha muerto.");

        GameTurnManager.Instance.RemoveCharacter(this);
        Destroy(gameObject); 
    }

    public void Attack(Character character)
    {
        if(_characterMovement.RemainingMovement > 0)
        {
            _target = character;
            Debug.Log($"{Nombre} ataca a {character.Nombre}");

            Animator.Play("Attack");
            _characterMovement.Attack();
        }
    }

    public void ApplyDamage()
    {
        if (_target != null)
        {
            Vector3Int cellPos = GameTurnManager.Instance.Tilemap.WorldToCell(_target.transform.position);

            _target.TakeDamage(Damage);
            
            Debug.Log($"{gameObject.name} está infligiendo daño a {_target.name}");

            if(!_target.IsAlive || _target == null)
            {
                Movement.MoveToCell(cellPos, GameTurnManager.Instance.Tilemap, 0);
            }
            _target = null;
        }
    }
    
    public void UpdateMovementUI()
    {
        _ui.UpdateMovement(_characterMovement.RemainingMovement);
    }

    public void EffectAttack()
    {
        ParticleSystem particleInstance = Instantiate(_particlePrefab, transform.position, Quaternion.identity);
        particleInstance.GetComponent<ParticleSystemRenderer>().sortingOrder = 10; 
    }

}
