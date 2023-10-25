using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Models;
using YouTubeViewers.Domain.Commands;
using YouTubeViewers.EntityFramework.DTOModels;

namespace YouTubeViewers.EntityFramework.Commands
{
    public class CreateYouTubeViewerCommand : ICreateYouTubeViewerCommand
    {
        private readonly YouTubeViewersDbContextFactory dbContextFactory;

        public CreateYouTubeViewerCommand(YouTubeViewersDbContextFactory dbContextFactory)
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

                context.YouTubeViewersSet.Add(youTubeViewerDTO);
                await context.SaveChangesAsync();
            }
        }
    }
}
