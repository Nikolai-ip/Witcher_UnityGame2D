public class RightAttack : Attack
{
    public RightAttack()
    {
        AnimationDuration = 0.3f;
        CompareIndex = 1;
        MovePlayerTime = AnimationDuration;
        AnimationName = AnimationName + CompareIndex.ToString();
    }
}