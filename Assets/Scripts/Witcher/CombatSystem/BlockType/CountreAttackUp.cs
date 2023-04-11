namespace Assets.Scripts.Witcher.CombatSystem.BlockType
{
    internal class CountreAttackUp : CountreAttackBase
    {
        public CountreAttackUp()
        {
            AnimationName = "CountreAttackUp";
            AnimationExecuteDuration = 0.1f;
            HitType = HitType.UpHit;
        }
    }
}