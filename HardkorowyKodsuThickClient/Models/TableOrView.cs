namespace HardkorowyKodsuThickClient.Models
{
    public class TableOrView
    {
        public required string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
