using System;
using System.Collections.Generic;
using System.Diagnostics;
using Android.Text;
using Gallery.Droid.DependencyService;
using Xamarin.Forms;
using static Android.Provider.MediaStore;

[assembly: Dependency(typeof(ImageSearchService))]
namespace Gallery.Droid.DependencyService
{
    public class ImageSearchService : IImageSearchService
    {
        public IList<string> GetImagePathsAsync()
        {
            var result = new List<string>();
            var uri = Android.Provider.MediaStore.Images.Media.ExternalContentUri;

            string[] projection = { MediaColumns.Data, MediaColumns.DisplayName };

            var cursor = Android.App.Application.Context.ContentResolver.Query(uri, projection, null, null, MediaColumns.DateAdded + " desc");
            int columnIndex = cursor.GetColumnIndexOrThrow(MediaColumns.Data);
            int columnDisplayname = cursor.GetColumnIndexOrThrow(MediaColumns.DisplayName);

            int lastIndex;
            while (cursor.MoveToNext())
            {
                String absolutePathOfImage = cursor.GetString(columnIndex);
                String nameOfFile = cursor.GetString(columnDisplayname);
                lastIndex = absolutePathOfImage.LastIndexOf(nameOfFile);
                lastIndex = lastIndex >= 0 ? lastIndex : nameOfFile.Length - 1;

                if (!TextUtils.IsEmpty(absolutePathOfImage))
                {
                    result.Add(absolutePathOfImage);
                }
            }

            foreach (var str in result)
            {
                Debug.WriteLine($"@@@@PhotoSelectActivity.java | getPathOfAllImages | : {str}");
            }
            return result;
        }
    }
}
