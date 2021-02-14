namespace DesignPatterns.Builder
{
    public class CustomStringBuilder : ICustomStringBuilder
    {
        private string _text;
        public CustomStringBuilder()
        {
            _text = string.Empty;
        }

        public CustomStringBuilder(string text)
        {
            _text = text;
        }

        public ICustomStringBuilder Append(string str)
        {
            _text += str;
            return this;
        }

        public ICustomStringBuilder Append(char ch)
        {
            _text += ch;
            return this;
        }

        public ICustomStringBuilder AppendLine()
        {
            return Append('\n');
        }

        public ICustomStringBuilder AppendLine(string str)
        {
            return Append(str).AppendLine();
        }

        public ICustomStringBuilder AppendLine(char ch)
        {
            return Append(ch).AppendLine();
        }

        public string Build()
        {
            return _text;
        }
    }
}