using System.Windows.Controls;

namespace IntegratorJr
{
    public static class TextBoxExtensions
    {
        public static double Number(this TextBox textBox)
        {
            return double.Parse(textBox.Text);
        }
    }
}
