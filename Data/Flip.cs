namespace Blazor.Data
{
	public class Flip
    {
        public Flip(string name, bool flipped)
        {
            Flipped = flipped;
            Name = name;
        }
        public bool Flipped { get; set; }
        public string Name { get; set; }

    }
}