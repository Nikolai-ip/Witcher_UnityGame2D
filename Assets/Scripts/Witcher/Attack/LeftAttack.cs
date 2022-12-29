class LeftAttack : Attack
{
    public  LeftAttack()
    {
        AnimationDuration = 0.3f;
        CompareIndex = 0;
        MovePlayerTime = AnimationDuration;
        AnimationName = AnimationName+CompareIndex.ToString();
    }
}