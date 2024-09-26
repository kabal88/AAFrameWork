namespace Helpers
{
    public static class CurrencyExtensions
    {
        public static int ConvertToCents(this decimal price)
        {
            return (int)(price * 100);
        }
        
        public static int ConvertToCents(this int price)
        {
            return price * 100;
        }
        
        public static double ConvertFromCents(this int price)
        {
            return (double)price / 100;
        }
    }
}