using System.Drawing;

namespace Bot.Discord;

public class Constants
{
    // Todo: Replace the publicKey and the botId with yours. https://discord.com/developers/applications.
    public const string PublicKey = "5d7890af1ce9b286d76e8129891e8acff77782a1b2e6f06e6023fa09557c8c1d";
    public const long BotId = 541336442979483658;

    public const string ArgsSeparator = ";";
    public const string SlashPrefix = "/";

    public static class Colors
    {
        public static readonly Color Error = Color.Red;
        public static readonly Color Successful = Color.LawnGreen;
        public static readonly Color Neutral = Color.White;
    }
}