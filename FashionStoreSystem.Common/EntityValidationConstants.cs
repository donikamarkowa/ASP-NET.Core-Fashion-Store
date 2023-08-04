namespace FashionStoreSystem.Common
{
    public class EntityValidationConstants
    {
        public static class Category
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class Product
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;

            public const int ImageUrlMaxLength = 2048;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 200;

            public const int SizeMinLength = 1;
            public const int SizeMaxLength = 10;

            public const string PriceMinLength = "2.00";
            public const string PriceMaxLength = "2000";
        }

        public static class Size
        {
            public const int NameMinLength = 1;
            public const int NameMaxLength = 10;
        }

        public static class Seller
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 50;

            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 50;

            public const int PhoneMinLength = 4;
            public const int PhoneMaxLength = 20;
        }

        public static class User
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 50;

            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 50;

            public const string WalletMinLength = "5";
            public const string WalletMaxLength = "500";
        }
    }
}
