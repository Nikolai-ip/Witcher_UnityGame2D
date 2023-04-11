[System.Serializable]
public class RightAttack : AttackBase
{
    public RightAttack()
    {
        Duration = 0.3f;
        AttackIndex = 1;
        MovePlayerTime = Duration;
        AnimationName = AnimationName + AttackIndex.ToString();
    }

}