using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProiectDAW.Models
{
    public class Painting
    {
        [Key, Column("Painting_id")]
        public int PaintingId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public DateTime LoadedFromDatabase { get; set; }
        // one to many
        //[Column("Curator_id")]
        public int CuratorId { get; set; }
        [ForeignKey ("Curator")]
        public virtual Curator Curator { get; set; }
        //many to many
        public virtual ICollection<Technique> Techniques { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> CuratorsList { get; set; }

    }
    public class DbCtx : DbContext
    {
        public DbCtx() : base("DbConnectionString")
        {
            Database.SetInitializer<DbCtx>(new Initp());
            //Database.SetInitializer<DbCtx>(new CreateDatabaseIfNotExists<DbCtx>());
            //Database.SetInitializer<DbCtx>(new DropCreateDatabaseIfModelChanges<DbCtx>());
            //Database.SetInitializer<DbCtx>(new DropCreateDatabaseAlways<DbCtx>());
        }
        public DbSet<Painting> Paintings { get; set; }
        
        public DbSet<Curator> Curators { get; set; }
        public DbSet<Technique> Techniques { get; set; }
        //public DbSet<ContactInfo> ContactsInfo { get; set; }
    }

    public class Initp : DropCreateDatabaseIfModelChanges<DbCtx>
    { // custom initializer
        protected override void Seed(DbCtx ctx)
        {
            Curator curator1 = new Curator(CuratorId = 98, Name = "nume1");
            Curator curator2 = new Curator(CuratorId = 99, Name = "nume2");
            ctx.Curators.Add(curator1);
            ctx.Curators.Add(curator2);
            ctx.Paintings.Add(new Painting
            {
                PaintingId = 1,
                Title = "Stary Night",
                Artist = "Van Gogh",
                CuratorId = curator1.CuratorId,
                Techniques = new List<Technique> {
                    new Technique { Name = "Oil on canvas"}
 }
            });
            ctx.Paintings.Add(new Painting
            {
                PaintingId = 2,
                Title = "Starty Night2",
                Artist = "Vang Gogh2",
                CuratorId = curator2.CuratorId,
                Techniques = new List<Technique> {
                    new Technique { Name = "Oil onf canvas"}
 }
            });
            //ctx.Paintings.Add(new Painting { Title = "Data curenta", Artist = DateTime.Now.ToString() });
            ctx.SaveChanges();
            base.Seed(ctx);
        }
    }
    /*
    public class DbCtx : DbContext
    {
        public DbCtx() : base("DbConnectionString")
        {
            
            Database.SetInitializer<DbCtx>(new Initp());
            //Database.SetInitializer<DbCtx>(new CreateDatabaseIfNotExists<DbCtx>());
            //Database.SetInitializer<DbCtx>(new DropCreateDatabaseIfModelChanges<DbCtx>());
            //Database.SetInitializer<DbCtx>(new DropCreateDatabaseAlways<DbCtx>());
        }

     public DbSet<Painting> Paintings { get; set; }
    }
    */
}
