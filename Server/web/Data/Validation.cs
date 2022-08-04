namespace web.Data;

public static class Validation
{
    public static class Cat
    {
        public const int DescriptionMaxLength = 2000;
    }

    public static class User
    {
        public const int MaxNameLength = 40;
        public const int MaxBiographyLength = 150;
    }
}