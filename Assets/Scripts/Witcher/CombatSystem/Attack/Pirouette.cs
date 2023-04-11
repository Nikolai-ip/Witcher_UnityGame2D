[System.Serializable]
public class Pirouette : AttackBase
{
    public Pirouette()
    {
        Duration = 0.3f;
        AttackIndex = 2;
        MovePlayerTime = Duration;

        AnimationName = AnimationName + AttackIndex.ToString();
    }
}