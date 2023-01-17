using UnityEngine;

public class AttackIndicator : MonoBehaviour
{
    private void Start()
    {
        GetComponentInParent<AttackController>().onCanAttack += ShowAttackIndictor;
    }

    private void ShowAttackIndictor(object sender, bool playerCanAttack)
    {
        gameObject.SetActive(playerCanAttack);
    }
}