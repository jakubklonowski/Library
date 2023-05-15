namespace Library.services.validators
{
    public class Validators
    {
        public static void StringShortOrNull(int len, params string[] values)
        {
            foreach (string value in values)
            {
                if (value == null || value.Length < len)
                {
                    throw new ArgumentException();
                }
            }
        }

        public static void Atoi(params string[] numbers)
        {
            foreach (string number in numbers)
            {
                if (!int.TryParse(number, out _))
                {
                    throw new ArgumentException();
                }
            }
        }

        public static void IsNaturalNumber(params int[] numbers)
        {
            foreach (int number in numbers)
            {
                if (!(number > 0 && number % 1 == 0))
                {
                    throw new ArgumentException();
                }
            }
        }
    }
}
