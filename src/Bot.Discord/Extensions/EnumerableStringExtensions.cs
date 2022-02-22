namespace Bot.Discord.Extensions;

public static class EnumerableStringExtensions
{
    public static string GetFullCommandName(this IEnumerable<string> commandSegments)
    {
        return string.Join(" ", commandSegments);
    }
}