namespace DinDigitaleVerden
{
    public static class InputValidator
    {
        static char[] whitelist = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        public static bool ValidateString(string input)
        {
            char[] chars = input.ToCharArray();
            foreach (char c in chars)
            {
                if (!whitelist.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
