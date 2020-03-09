using System;
using System.Net;
using System.Threading.Tasks;
using Android.Graphics;

namespace UsersProfileApp.Android.Helper
{
    public class ImageHelper
    {
        public static Task<Bitmap> GetBitmapFromUrl(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                byte[] bytes = webClient.DownloadData(url);
                if (bytes != null && bytes.Length > 0)
                {
                    return BitmapFactory.DecodeByteArrayAsync(bytes, 0, bytes.Length);
                }
            }
            return null;
        }
    }
}
