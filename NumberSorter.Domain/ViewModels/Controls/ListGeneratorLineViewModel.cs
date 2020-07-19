using NumberSorter.Core.CustomGenerators;

namespace NumberSorter.Domain.ViewModels
{
    public class ListGeneratorLineViewModel
    {
        #region Properties

        public string Name { get; set; }
        public bool Shuffle { get; set; }
        public string Description { get; set; }

        public CustomListGenerator ListGenerator { get; }

        #endregion

        public ListGeneratorLineViewModel(CustomListGenerator listGenerator)
        {
            Name = listGenerator.Name;
            Shuffle = listGenerator.Shuffle;
            Description = listGenerator.Description;

            ListGenerator = listGenerator;
        }
    }
}
