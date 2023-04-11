using Assets.Scripts.Witcher.CombatSystem.BlockType;

public class AttackLeftSide : AttackBase
{
    public AttackLeftSide()
    {
        Duration = 0.8f;
        damage = 20f;
        DelayBeforeHit = 0.5f;
        AnimationName = "AttackLeftSide";
        HitType = HitType.SideHit;
    }
    public AttackLeftSide(float damage, float delaybeforeHit):base(damage, delaybeforeHit)
    {
        Duration = 0.8f;
        AnimationName = "AttackLeftSide";
        HitType = HitType.SideHit;
    }
}