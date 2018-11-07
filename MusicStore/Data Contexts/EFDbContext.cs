using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MusicStore.Models;

namespace MusicStore.Data_Contexts
{
    public class EFDbContext : DbContext
    {
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }

        public EFDbContext() : base("name=musicStoreDbContext")
        {

        }
    }
}