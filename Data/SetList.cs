namespace Blazor.Data
{
    public class SetList
    {
        public SetList(string code, string name)
        {
            Code = code;
            Name = name;
        }
        public string Code { get; set; }
        public string Name { get; set; }

    }
}