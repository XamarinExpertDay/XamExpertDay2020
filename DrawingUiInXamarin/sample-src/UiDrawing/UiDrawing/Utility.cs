using System.IO;
using System.Reflection;
using SkiaSharp;

namespace UiDrawing
{
    public static class Utility
    {
        public static SKBitmap LoadSomeGuy()
        {
            var resourceID = "UiDrawing.Resources.some-guy.png";
            var assembly = typeof(Utility).Assembly;

            using (Stream stream = assembly.GetManifestResourceStream(resourceID))
            {
                return SKBitmap.Decode(stream);
            }
        }
    }
}