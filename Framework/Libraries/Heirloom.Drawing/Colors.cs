using System.Collections.Generic;

namespace Heirloom.Drawing
{
    public static class Colors
    {
        /// <summary>
        /// Colors sourced from https://flatuicolors.com/palette/defo
        /// </summary>
        /// todo: rename for bright/dark
        public static class FlatUI
        {
            // Turquoise
            public static Color Turquoise = Color.Parse("1ABC9C");
            public static Color GreenSea = Color.Parse("16A085");

            // Yellow
            public static Color Sunflower = Color.Parse("F1C40F");
            public static Color Orange = Color.Parse("F39C12");

            // Green
            public static Color Emerald = Color.Parse("2ECC71");
            public static Color Nephritis = Color.Parse("27AE60");

            // Orange
            public static Color Carrot = Color.Parse("E67E22");
            public static Color Pumpkin = Color.Parse("D35400");

            // Blue
            public static Color PeterRiver = Color.Parse("3498DB");
            public static Color BelizeHole = Color.Parse("2980B9");

            // Red
            public static Color Alizarin = Color.Parse("E74C3C");
            public static Color Pomegranate = Color.Parse("C0392B");

            // Purple
            public static Color Amethyst = Color.Parse("9B59B6");
            public static Color Wisteria = Color.Parse("8E44AD");

            // Blue Gray
            public static Color WetAshphalt = Color.Parse("34495E");
            public static Color MidnightBlue = Color.Parse("2C3E50");

            // Gray
            public static Color Clouds = Color.Parse("ECF0F1");
            public static Color Silver = Color.Parse("BDC3C7");
            public static Color Concrete = Color.Parse("95A5A6");
            public static Color Asbestos = Color.Parse("7F8C8D");

            public static Color[] All = new Color[]
            {
                // todo: reorder so its bright then dark variants
                Turquoise, GreenSea, Sunflower, Orange, Emerald, Nephritis,
                Carrot, Pumpkin, PeterRiver, BelizeHole, Alizarin, Pomegranate,
                Amethyst, Wisteria, WetAshphalt, MidnightBlue,
                Clouds, Silver, Concrete, Asbestos
            };
        }

        public static class Base16
        {
            // Grayscale
            public static Color Gray0 = Color.Parse("181818");
            public static Color Gray1 = Color.Parse("282828");
            public static Color Gray2 = Color.Parse("383838");
            public static Color Gray3 = Color.Parse("585858");
            public static Color Gray4 = Color.Parse("B8B8B8");
            public static Color Gray5 = Color.Parse("D8D8D8");
            public static Color Gray6 = Color.Parse("E8E8E8");
            public static Color Gray7 = Color.Parse("F8F8F8");

            // Colors
            public static Color Red = Color.Parse("AB4642");    // red
            public static Color Orange = Color.Parse("DC9656"); // orange
            public static Color Yellow = Color.Parse("F7CA88"); // yellow
            public static Color Green = Color.Parse("A1B56C");  // green
            public static Color Teal = Color.Parse("86C1B9");   // teal
            public static Color Blue = Color.Parse("7CAFC2");   // blue
            public static Color Pink = Color.Parse("BA8BAF");   // pink
            public static Color Brown = Color.Parse("A16946");  // brown / dark orange?

            public static Color[] All = new Color[]
            {
                Gray0, Gray1, Gray2, Gray3, Gray4, Gray5, Gray6, Gray7,
                Red, Orange, Yellow, Green, Teal, Blue, Pink, Brown
            };
        }
    }
}
