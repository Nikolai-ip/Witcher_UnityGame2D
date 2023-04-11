using Assets.Scripts.Witcher.CombatSystem.BlockType;

[System.Serializable]
public class AttackUp : AttackBase
{
    public AttackUp()
    {
        damage = 30f;
        Duration = 0.7f;
        DelayBeforeHit = 0.6f;
        AnimationName = "AttackUp";
        HitType = HitType.UpHit;
    }
    public AttackUp(float damage, float delaybeforeHit) : base(damage, delaybeforeHit)
    {
        Duration = 0.7f;
        AnimationName = "AttackUp";
        HitType = HitType.UpHit;
    }

}