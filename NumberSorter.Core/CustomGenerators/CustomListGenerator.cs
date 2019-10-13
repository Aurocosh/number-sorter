using NumberSorter.Core.CustomGenerators.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.CustomGenerators
{
    public class CustomListGenerator : ICloneable
    {
        public string Name { get; set; }
        public bool Shuffle { get; set; }
        public string Description { get; set; }
        public List<ListProcessorSet> ListProcessorSets { get; }

        public CustomListGenerator()
        {
            Name = "New generator";
            Shuffle = false;
            ListProcessorSets = new List<ListProcessorSet>();
        }

        public CustomListGenerator(string name, string description, bool shuffle, List<ListProcessorSet> listProcessorSets)
        {
            Name = name;
            Description = description;
            Shuffle = shuffle;
            ListProcessorSets = listProcessorSets;
        }

        public int[] GenerateList(IConverterContext context)
        {
            var generatedLists = ListProcessorSets.SelectMany(x => x.GenerateLists(context)).ToList();
            if (Shuffle)
                generatedLists.Shuffle(context.Random);
            return ArrayUtility.JoinArrays(generatedLists);
        }

        public object Clone()
        {
            return new CustomListGenerator(Name, Description, Shuffle, ListProcessorSets.Select(x => x.Clone()).Cast<ListProcessorSet>().ToList());
        }
    }
}
