using NumberSorter.Core.CustomGenerators.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Core.CustomGenerators
{
    public class CustomListGenerator : ICloneable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public bool Shuffle { get; set; }
        public string Description { get; set; }
        public List<ListProcessorSet> ListProcessorSets { get; }

        public CustomListGenerator()
        {
            Id = Guid.NewGuid();
            Name = "New generator";
            Shuffle = false;
            ListProcessorSets = new List<ListProcessorSet>();
        }

        public CustomListGenerator(string name, string description, bool shuffle, List<ListProcessorSet> listProcessorSets)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Shuffle = shuffle;
            ListProcessorSets = listProcessorSets;
        }

        public CustomListGenerator(Guid id, string name, bool shuffle, string description, List<ListProcessorSet> listProcessorSets)
        {
            Id = id;
            Name = name;
            Shuffle = shuffle;
            Description = description;
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
            return new CustomListGenerator(Id, Name, Shuffle, Description, ListProcessorSets.Select(x => x.Clone()).Cast<ListProcessorSet>().ToList());
        }
    }
}
