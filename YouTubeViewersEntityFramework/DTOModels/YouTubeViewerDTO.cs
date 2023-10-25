using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeViewers.Domain.Models;

namespace YouTubeViewers.EntityFramework.DTOModels
{
    public class YouTubeViewerDTO
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public bool IsSubsribed { get; set; }

        public bool IsMember { get; set; }
    }
}
