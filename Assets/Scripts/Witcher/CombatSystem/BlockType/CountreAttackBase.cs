namespace Assets.Scripts.Witcher.CombatSystem.BlockType
{
    public abstract class CountreAttackBase
    {
        public HitType HitType { get; protected set; }
        public string AnimationName { get; protected set; }
        public float AnimationExecuteDuration { get; protected set; }
    }
}