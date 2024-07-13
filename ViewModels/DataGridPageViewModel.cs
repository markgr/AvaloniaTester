using AvaloniaTester.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AvaloniaTester.ViewModels
{
    internal class DataGridPageViewModel : ViewModelBase
   {
        public ObservableCollection<Person> People { get; private set; }

        public DataGridPageViewModel()
        {
            var people = new List<Person>()
            {
                new Person() { Id = 1, FirstName = "Mark", LastName = "Greenwood" },
                new Person() { Id = 2, FirstName = "Fiona", LastName = "Greenwood" },
                new Person() { Id = 3, FirstName = "Evie", LastName = "Greenwood" },
                new Person() { Id = 4, FirstName = "Jemma", LastName = "Greenwood" }
            };

            People = new ObservableCollection<Person>(people);
        }
    }
}
