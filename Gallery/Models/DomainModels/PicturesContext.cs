namespace Gallery.Models.DomainModels
{
    using System.Data.Entity;

    public partial class PicturesContext : DbContext
    {
        public PicturesContext()
            : base("name=PicturesContext")
        {
        }

        public virtual DbSet<Picture> Pictures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
