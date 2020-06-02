using System.Windows.Controls;

namespace IntegratorJr
{
    public static class TextBoxExtensions
    {
        public static double Number(this TextBox textBox)
        {
            var textWithDots = textBox.Text.Replace(',', '.');

            return double.Parse(textWithDots);
        }
    }
}