using System;
using UsersProfileApp.Core.DTO;

namespace UsersProfileApp.Core.Model
{
    public class PhotoModel
    {
        public int AlbumId { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string ThumbnailUrl { get; set; }

        public PhotoModel(PhotoDTO dto)
        {
            if (dto != null)
            {
                this.AlbumId = dto.AlbumId;
                this.Id = dto.Id;
                this.Title = dto.Title;
                this.Url = dto.Url;
                this.ThumbnailUrl = dto.ThumbnailUrl;
            }
        }
    }
}
