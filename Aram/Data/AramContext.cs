﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Aram.Models;

namespace Aram.Data
{
    public class AramContext : DbContext
    {
        public AramContext (DbContextOptions<AramContext> options)
            : base(options)
        {
        }

        public DbSet<Aram.Models.CuaHang> CuaHang { get; set; } = default!;
    }
}
