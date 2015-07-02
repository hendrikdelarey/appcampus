using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCampus.Domain.Interfaces.Components
{
    public interface IImageComponent
    {
        bool IsValidImageId(Guid imageId);
        string GetImageFromId(Guid imageId);
        string GetNameFromId(Guid imageId);
        Guid AddImage(string base64Image, string imageName);
    }
}
