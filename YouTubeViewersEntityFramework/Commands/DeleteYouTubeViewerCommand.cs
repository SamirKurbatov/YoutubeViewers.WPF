using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTubeViewers.Domain.Commands;

namespace YouTubeViewers.EntityFramework.Commands
{
    public class DeleteYouTubeViewerCommand : IDeleteYouTubeViewerCommand
    {
        private readonly YouTubeViewersDbContextFactory dbContextFactory;

        public DeleteYouTubeViewerCommand(YouTubeViewersDbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task Execute(Guid id)
        {
            using (YouTubeViewersDbContext context = dbContextFactory.Create())
            {
                await Task.Delay(2000);
                var youTubeViewer = context.YouTubeViewersSet.FirstOrDefault(y => y.Id == id);
                if (youTubeViewer != null)
                {
                    context.YouTubeViewersSet.Remove(youTubeViewer);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
