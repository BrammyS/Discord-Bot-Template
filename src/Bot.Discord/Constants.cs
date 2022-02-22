using System.Drawing;

namespace Bot.Discord;

public class Constants
{
    public const string ArgsSeparator = ";";
    public const string SlashPrefix = "/";
    
    public static class Colors
    {
        public static readonly Color Error = Color.Red;
        public static readonly Color Successful = Color.LawnGreen;
        public static readonly Color Neutral = Color.White;
    }
}