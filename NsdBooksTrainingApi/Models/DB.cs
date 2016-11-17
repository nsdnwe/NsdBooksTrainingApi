using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NsdBooksTrainingApi.Models {
    public class DB : DbContext {
        public DbSet<Book> Books { get; set; }
    }
}