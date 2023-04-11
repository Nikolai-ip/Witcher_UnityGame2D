using UnityEngine;

public abstract class InputController : MonoBehaviour
{
    protected PlayerMove playerMove;
    protected AttackController playerAttack;
    protected SignCaster signCaster;

    protected enum Mouse
    {
        LeftButton,
        RightButton,
        CenterButton
    }

    protected LeftAttack leftAttack = new LeftAttack();
    protected RightAttack rightAttack = new RightAttack();
    protected Pirouette pirouetteAttack = new Pirouette();
}