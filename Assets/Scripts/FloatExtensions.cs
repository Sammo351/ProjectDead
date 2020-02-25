public static class FloatExtensions
{
    public static float DeadZone(this float f, float deadZone)
    {
        float fAbs = f;
        if (fAbs < 0)
            fAbs *= -1;

        if (fAbs < deadZone)
            return 0f;

        return f;
    }
}