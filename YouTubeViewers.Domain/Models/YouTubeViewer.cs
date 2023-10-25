using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeViewers.Domain.Models
{
    public class YouTubeViewer
    {
        public YouTubeViewer(Guid id,string userName, bool isSubsribed, bool isMember)
        {
            Id = id;
            UserName = userName;
            IsSubsribed = isSubsribed;
            IsMember = isMember;
        }

        public Guid Id { get; set; }

        public string UserName { get; }

        public bool IsSubsribed { get; }

        public bool IsMember { get; }
    }
}
