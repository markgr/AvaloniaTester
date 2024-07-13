using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.LogicalTree;
using Avalonia.Metadata;

namespace AvaloniaTester.Template;

public class ContentExample : TemplatedControl
{

    /// <summary>
    /// IsCapable StyledProperty definition
    /// </summary>
    public static readonly StyledProperty<object?> ContentProperty = ContentControl.ContentProperty.AddOwner<Panel>();

    /// <summary>
    /// Gets or sets the IsCapable property. This StyledProperty
    /// indicates ....
    /// </summary>
    [Content]
    public object? Content
    {
        get => this.GetValue(ContentProperty);
        set
        {
            if(GetValue(ContentProperty) is ILogical oldLogical)
            {
                LogicalChildren.Remove(oldLogical);
            }
            SetValue(ContentProperty, value);
            if (value is ILogical newLogical) 
            {
                LogicalChildren.Add(newLogical);
            }
        }
    }


}