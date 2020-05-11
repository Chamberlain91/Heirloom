using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Heirloom;

namespace Examples.GameInput
{
    public sealed class Emote
    {
        public readonly string Command;

        public readonly IReadOnlyList<Regex> Patterns;

        public readonly Image Image;

        public Emote(string command, Regex[] matchers)
        {
            Image = new Image($"assets/emotes/emote_{command}.png");
            Image.Origin = (Image.Width / 2, Image.Height);

            Command = command ?? throw new ArgumentNullException(nameof(command));
            Patterns = matchers ?? throw new ArgumentNullException(nameof(matchers));
        }
    }
}
