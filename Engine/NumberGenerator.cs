namespace Engine;

public static class RandomNumber
{
    public static int Between(int minimumValue, int maximumValue) =>
        RandomNumberGenerator.GetInt32(minimumValue, maximumValue + 1);
}
