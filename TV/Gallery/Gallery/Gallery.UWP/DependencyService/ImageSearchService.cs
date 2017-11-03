using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Gallery.UWP.DependencyService;
using Windows.ApplicationModel;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageSearchService))]
namespace Gallery.UWP.DependencyService
{
    public class ImageSearchService : IImageSearchService
    {
        public IList<string>  GetImagePathsAsync()
        {
            var filters = new string[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" };
            List<FileInfo> filesFound = new List<FileInfo>();
            var searchOption = SearchOption.TopDirectoryOnly;

            DirectoryInfo info = new DirectoryInfo(Package.Current.InstalledLocation.Path);

            foreach (var filter in filters)
            {
                filesFound.AddRange(info.GetFiles(string.Format("*.{0}", filter), searchOption));
            }

            var result = filesFound.Select((i, s) => i.Name).ToList();
            result.Sort();

            return result;
        }
    }
}
