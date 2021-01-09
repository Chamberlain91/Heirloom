using Heirloom;

namespace Examples.GameInput
{
    internal static class Palette
    {
        public static readonly Color ChatboxFade = new Color(1F, 1F, 1F, 0.5F);
        public static readonly Color ChatboxBackground = Color.Parse("DD2C3E50");
        public static readonly Color ChatboxBorder = Color.Parse("34495E");
        public static readonly Color ChatboxUserText = Color.Parse("F1C40F");
        public static readonly Color ChatboxText = Color.Parse("ECf0F1");

        public static readonly Color Background = Color.Parse("2E86DE");
        public static readonly (Color, Color)[] BoxColors = new[] {
            (Color.Parse("8E44AD"), Color.Parse("9B59B6")), // purple
            (Color.Parse("c0392b"), Color.Parse("e74c3c")), // red
            (Color.Parse("27ae60"), Color.Parse("2ecc71")), // green
        };
    }
}
