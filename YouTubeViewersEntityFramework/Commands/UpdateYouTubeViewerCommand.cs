using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Models;
using YouTubeViewers.Domain.Commands;
using YouTubeViewers.EntityFramework.DTOModels;

namespace YouTubeViewers.EntityFramework.Commands
{
    public class UpdateYouTubeViewerCommand : IUpdateYouTubeViewerCommand
    {
        private readonly YouTubeViewersDbContextFactory dbContextFactory;

        public UpdateYouTubeViewerCommand(YouTubeViewersDbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task Execute(YouTubeViewer youTubeViewer)
        {
            using (YouTubeViewersDbContext context = dbContextFactory.Create())
            {
                YouTubeViewerDTO youTubeViewerDTO = new YouTubeViewerDTO()
                {
                    Id = youTubeViewer.Id,
                    UserName = youTubeViewer.UserName,
                    IsSubsribed = youTubeViewer.IsSubsribed,
                    IsMember = youTubeViewer.IsMember,
                };

                context.YouTubeViewersSet.Update(youTubeViewerDTO);
                await context.SaveChangesAsync();
            }
        }
    }
}
