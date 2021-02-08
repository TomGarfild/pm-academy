namespace RequestProcessor.App.Models.Impl
{
    internal class Response : IResponse
    {
        public Response(int code, string content)
        {
            Code = code;
            Content = content;
            Handled = true;
        }
        public bool Handled { get; set; }
        public int Code { get; }
        public string Content { get; }
    }
}