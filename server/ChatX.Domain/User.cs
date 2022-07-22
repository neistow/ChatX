namespace ChatX.Domain;

public class User
{
    public string Id { get; set; }

    public Gender Gender { get; set; }
    public AgeRange Age { get; set; }

    public Gender PreferredGenders { get; set; }
    public AgeRange PreferredAges { get; set; }
    public DateTime SearchStartedAt { get; } = DateTime.UtcNow;

    public static User Create(
        string id,
        Gender gender,
        AgeRange age,
        Gender preferredGenders,
        AgeRange preferredAges)
    {
        return new User
        {
            Id = id,
            Gender = gender,
            Age = age,
            PreferredGenders = preferredGenders,
            PreferredAges = preferredAges,
        };
    }
}