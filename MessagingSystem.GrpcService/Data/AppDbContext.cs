using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagingSystem.GrpcService.Models;
using Microsoft.EntityFrameworkCore;

namespace MessagingSystem.GrpcService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

        public DbSet<Message> Messages {get; set;}
    }
}