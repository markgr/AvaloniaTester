using AvaloniaTester.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaTester.ViewModels
{
    internal partial class ShapePageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ShapeType _selectedShape;

        private ShapeType[] AvailableShapes { get; set; } = Enum.GetValues<ShapeType>();
    }
}
