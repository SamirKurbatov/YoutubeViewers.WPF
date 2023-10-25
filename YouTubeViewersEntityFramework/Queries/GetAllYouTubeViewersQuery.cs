using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Models;
using YouTubeViewers.Domain.Queries;
using YouTubeViewers.EntityFramework.DTOModels;

namespace YouTubeViewers.EntityFramework.Queries
{
    public class GetAllYouTubeViewersQuery : IGetAllYouTubeViewersQuery
    {
        private readonly YouTubeViewersDbContextFactory dbContextFactory;

        public GetAllYouTubeViewersQuery(YouTubeViewersDbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<YouTubeViewer>> Execute()
        {
            using(YouTubeViewersDbContext context = dbContextFactory.Create())
            {
                IEnumerable<YouTubeViewerDTO> youTubeViewerDTOs = await context.YouTubeViewersSet.ToListAsync();

                return youTubeViewerDTOs.Select(y => new YouTubeViewer(y.Id, y.UserName, y.IsSubsribed, y.IsMember));
            }
        }
    }
}
