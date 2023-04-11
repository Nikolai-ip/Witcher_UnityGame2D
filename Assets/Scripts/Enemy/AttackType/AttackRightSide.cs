using Assets.Scripts.Witcher.CombatSystem.BlockType;

[System.Serializable]
public class AttackRightSide : AttackBase
{
    public AttackRightSide()
    {
        damage = 20f;
        Duration = 0.8f;
        DelayBeforeHit = 0.5f;
        AnimationName = "AttackRightSide";
        HitType = HitType.SideHit;
    }
    public AttackRightSide(float damage, float delaybeforeHit) : base(damage, delaybeforeHit) 
    {
        Duration = 0.8f;
        AnimationName = "AttackRightSide";
        HitType = HitType.SideHit;
    }

}