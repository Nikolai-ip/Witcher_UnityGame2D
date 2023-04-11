[System.Serializable]
public class LeftAttack : AttackBase
{
    public LeftAttack()
    {
        Duration = 0.3f;
        AttackIndex = 0;
        MovePlayerTime = Duration;
        AnimationName = AnimationName + AttackIndex.ToString();
    }
}