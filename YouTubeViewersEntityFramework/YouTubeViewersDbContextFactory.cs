﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeViewers.EntityFramework
{
    public class YouTubeViewersDbContextFactory
    {
        private readonly DbContextOptions options;

        public YouTubeViewersDbContextFactory(DbContextOptions options)
        {
            this.options = options;
        }


        public YouTubeViewersDbContext Create()
        {
            return new YouTubeViewersDbContext(options);
        }
    }
}
