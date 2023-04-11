using System;
using System.Collections;
using UnityEngine;

public class StaminaController : MonoBehaviour
{
    [SerializeField]private float _stamina;
    private Entity _entity;
    public bool CanRecoveryStamina {get;set;} = true;   
    public float Stamina
    {
        get => _stamina;
        set
        {
            _stamina = value;
            OnStaminaChanged?.Invoke(_stamina);
        }
    }

    [SerializeField] private float _baseRecoveryRateStamina; // increasese stamina every second by set values
    [SerializeField] private float _delayRecoveryStamina;

    public event Action<float> OnStaminaChanged;

    private void Start()
    {
        _entity = GetComponent<Entity>();
        _stamina = _entity.MaxStamina;
        StartCoroutine(RecoveryStamina());
    }

    public void SpendStamina(float stamina)
    {
        Stamina -= stamina;
        if (Stamina < _entity.MinStamina) { Stamina = _entity.MinStamina; }
    }

    private IEnumerator RecoveryStamina()
    {
        var delay = new WaitForSeconds(_delayRecoveryStamina);
        while (true)
        {
            if (CanRecoveryStamina)
            {
                float staminaRecoveryThisIneration = _baseRecoveryRateStamina * _delayRecoveryStamina;
                Stamina += staminaRecoveryThisIneration;
                if (Stamina > _entity.MaxStamina) { Stamina = _entity.MaxStamina; }
            }
            yield return delay;
        }
    }
}