using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Highlights.Models;

namespace Highlights.Data
{
    public class HighlightsContext : DbContext
    {
        public HighlightsContext (DbContextOptions<HighlightsContext> options)
            : base(options)
        {
        }

        public DbSet<Highlights.Models.Topic> Topic { get; set; } = default!;
    }
}
