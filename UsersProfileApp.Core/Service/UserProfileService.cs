using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UsersProfileApp.Core.DTO;
using UsersProfileApp.Core.Model;

namespace UsersProfileApp.Core.Service
{
    public class UserProfileService
    {
        public static Task<List<PhotoModel>> GetUsersProfile()
        {
            var users = ServiceClient.Request<List<PhotoModel>>("https://jsonplaceholder.typicode.com/photos", HttpMethod.Get);

            return users;

        }

        // Mock to check the api flow

        //public static List<PhotoModel> GetUsersProfile()
        //{
        //    return new List<PhotoModel>()
        //    {
        //        new PhotoModel(new PhotoDTO()
        //        {
        //            Id= 1,
        //            AlbumId = 10001,
        //            Url= "www.www",
        //            ThumbnailUrl="https://via.placeholder.com/150/92c952",
        //            Title = "User 1"
        //        }),
        //        new PhotoModel(new PhotoDTO()
        //        {
        //            Id= 2,
        //            AlbumId = 10002,
        //            Url= "https://via.placeholder.com/600/24f355",
        //            ThumbnailUrl="https://via.placeholder.com/150/24f355",
        //            Title = "User 2"
        //        }),
        //        new PhotoModel(new PhotoDTO()
        //        {
        //            Id= 3,
        //            AlbumId = 10003,
        //            Url= "https://via.placeholder.com/600/24f355",
        //            ThumbnailUrl="https://via.placeholder.com/150/24f355",
        //            Title = "User 3"
        //        }),
        //        new PhotoModel(new PhotoDTO()
        //        {
        //            Id= 4,
        //            AlbumId = 10004,
        //            Url= "https://via.placeholder.com/600/24f355",
        //            ThumbnailUrl="https://via.placeholder.com/150/24f355",
        //            Title = "User 4"
        //        }),
        //        new PhotoModel(new PhotoDTO()
        //        {
        //            Id= 5,
        //            AlbumId = 10005,
        //            Url= "https://via.placeholder.com/600/24f355",
        //            ThumbnailUrl="https://via.placeholder.com/150/24f355",
        //            Title = "User 5"
        //        }),
        //    };
        //}
    }
}
