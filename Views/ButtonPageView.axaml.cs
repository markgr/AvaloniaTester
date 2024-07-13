using Avalonia.Controls;

namespace AvaloniaTester.Views;

public partial class ButtonPageView : UserControl
{
    public ButtonPageView()
    {
        InitializeComponent();
    }

    public void ButtonSpinner_Spin(object sender, SpinEventArgs e)
    {
        var spinner = (ButtonSpinner)sender;

        if(spinner.Content is not TextBlock txtBox)
        {
            return;
        }

        var text = txtBox.Text;
        if (!int.TryParse(text, out int value)) return;

        if(e.Direction == SpinDirection.Increase)
        {
            if (value is int.MaxValue) return;
            value++;
        }
        else
        {
            if (value is int.MinValue) return;
            value--;
        }

        txtBox.Text = value.ToString();
    }
}