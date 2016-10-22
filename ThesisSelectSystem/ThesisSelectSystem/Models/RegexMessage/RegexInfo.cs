namespace ThesisSelectSystem.Models.RegexMessage
{
    public static class RegexInfo
    {
        
        public static bool IsRegexTarget(string target,string originalstring)
        {
           return System.Text.RegularExpressions.Regex.IsMatch(originalstring, target);
        }
    }
}