using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NumberSorter.Domain.AppColors
{
    public class ColorSet
    {
        public Guid Id { get; }
        public string Name { get; }

        public Color ReadColor { get; }
        public Color WriteColor { get; }
        public Color NormalColor { get; }

        public Color BackgroundColor { get; }

        public Color CompareEqualColor { get; }
        public Color CompareBiggerColor { get; }
        public Color CompareLesserColor { get; }

        public ColorSet()
        {
            Id = Guid.NewGuid();
            Name = "Default color set";

            ReadColor = Colors.Yellow;
            WriteColor = Colors.Purple;
            NormalColor = Colors.White;

            BackgroundColor = Colors.LightGray;

            CompareEqualColor = Colors.Blue;
            CompareBiggerColor = Colors.Red;
            CompareLesserColor = Colors.Green;
        }

        [JsonConstructor]
        public ColorSet(Guid id, string name, Color readColor, Color writeColor, Color normalColor, Color backgroundColor, Color compareEqualColor, Color compareBiggerColor, Color compareLesserColor)
        {
            Id = id;
            Name = name;
            ReadColor = readColor;
            WriteColor = writeColor;
            NormalColor = normalColor;
            BackgroundColor = backgroundColor;
            CompareEqualColor = compareEqualColor;
            CompareBiggerColor = compareBiggerColor;
            CompareLesserColor = compareLesserColor;
        }
    }
}
