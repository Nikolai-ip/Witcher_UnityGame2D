namespace Assets.Scripts.Witcher.CombatSystem.BlockType
{
    internal class CountreAttackSide : CountreAttackBase
    {
        public CountreAttackSide()
        {
            AnimationName = "CountreAttackSide";
            AnimationExecuteDuration = 0.15f;
            HitType = HitType.SideHit;
        }
    }
}