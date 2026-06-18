using PdfSharp.Fonts;
using System;
using System.IO;

namespace WPF_LoginForm.Helpers
{
    public class PdfFontResolver : IFontResolver
    {
        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (isBold)
                return new FontResolverInfo("ArialBold");

            return new FontResolverInfo("ArialRegular");
        }

        public byte[] GetFont(string faceName)
        {
            switch (faceName)
            {
                case "ArialBold":
                    return File.ReadAllBytes(@"C:\Windows\Fonts\arialbd.ttf");

                case "ArialRegular":
                default:
                    return File.ReadAllBytes(@"C:\Windows\Fonts\arial.ttf");
            }
        }
    }
}
