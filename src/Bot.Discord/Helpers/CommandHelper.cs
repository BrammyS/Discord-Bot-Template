namespace Bot.Discord.Helpers;

public static class CommandHelper
{
    public static string SanitizeCommandName(string commandName)
    {
        var paramIndex = commandName.IndexOf(';');
        return paramIndex >= 0
            ? commandName.Remove(paramIndex)
            : commandName;
    }
}