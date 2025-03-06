using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace mypass.View
{
    public enum MarkColors
    {
        Red,
        Blue,
        Green,
        Yellow,
        Purple,
        White
    }

    public class MarkManager
    {
        public static Brush GetColor(MarkColors color)
        {
            switch (color) 
            {
                case MarkColors.Red:
                    return (SolidColorBrush)new BrushConverter().ConvertFromString("#ec6969");
                case MarkColors.Blue:
                    return (SolidColorBrush)new BrushConverter().ConvertFromString("#6892ed");
                case MarkColors.Green:
                    return (SolidColorBrush)new BrushConverter().ConvertFromString("#3dff7f");
                case MarkColors.Yellow:
                    return (SolidColorBrush)new BrushConverter().ConvertFromString("#ebe175");
                case MarkColors.Purple:
                    return (SolidColorBrush)new BrushConverter().ConvertFromString("#ac75eb");
                case MarkColors.White:
                    return (SolidColorBrush)new BrushConverter().ConvertFromString("#ffffff");
                default: 
                    return (SolidColorBrush)new BrushConverter().ConvertFromString("#ffffff");
            }
        }

        public static MarkColors GetMark(string name)
        {
            MarkColors targetColor = MarkColors.White;
            Enum.TryParse(name, true, out targetColor);

            return targetColor;
        }
    }
}
