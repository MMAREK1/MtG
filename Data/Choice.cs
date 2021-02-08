namespace Blazor.Data
{
	public class Choice
    {
        public Choice(int code, string name)
        {
            Code = code;
            Name = name;
        }
        public int Code { get; set; }
        public string Name { get; set; }

    }
}