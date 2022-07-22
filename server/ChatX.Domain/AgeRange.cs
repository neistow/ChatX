namespace ChatX.Domain;

[Flags]
public enum AgeRange
{
    BelowSeventeen = 1 << 0,
    EighteenToTwentyFour = 1 << 1,
    TwentyFiveToThirteen = 1 << 2,
    AboveThirty = 1 << 3
}