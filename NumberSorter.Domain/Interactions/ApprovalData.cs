namespace NumberSorter.Domain.Interactions
{
    public class YesNoQuestionData
    {
        public string Header { get; }
        public string Text { get; }

        public YesNoQuestionData(string header, string text)
        {
            Header = header;
            Text = text;
        }
    }
}
