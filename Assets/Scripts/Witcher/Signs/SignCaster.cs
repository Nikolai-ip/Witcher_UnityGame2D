using Assets.Scripts.Witcher.Signs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignCaster : MonoBehaviour
{
    private Dictionary<SignType, Sign> signsMap = new Dictionary<SignType, Sign>();
    [SerializeField] private Sign[] signs;
    [SerializeField] private UnityEvent onCast;

    private void Start()
    {
        if (signs == null)
        {
            signs = GetComponentsInChildren<Sign>();
        }
        if (signs != null)
        {
            InitSignsMap();
        }
    }

    private void InitSignsMap()
    {
        for (int i = 0; i < signs.Length; i++)
        {
            signsMap.Add((SignType)i, signs[i]);
        }
    }

    public void Cast(SignType signType)
    {
        if (signsMap.ContainsKey(signType))
        {
            onCast?.Invoke();
            signsMap[signType].Execute();
        }
    }
}