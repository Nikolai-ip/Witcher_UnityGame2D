using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : MonoBehaviour
{
    protected PlayerMove playerMove;
    protected AttackController playerAttack;

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
