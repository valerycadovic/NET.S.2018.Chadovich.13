namespace Gallery.Models.DomainModels
{
    public partial class Picture
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Path { get; set; }

        public string Description { get; set; }
    }
}
