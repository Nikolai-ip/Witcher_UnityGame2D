public class Pirouette : Attack
{
    public Pirouette()
    {
        AnimationDuration = 0.3f;
        CompareIndex = 2;
        MovePlayerTime = AnimationDuration;

        AnimationName = AnimationName + CompareIndex.ToString();
    }
}