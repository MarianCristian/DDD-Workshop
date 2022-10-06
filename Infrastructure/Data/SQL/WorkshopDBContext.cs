using System;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.SQL
{
    public class WorkshopDBContext : DbContext
    {
        public WorkshopDBContext(DbContextOptions<WorkshopDBContext> options) : base(options) { }
    }
}