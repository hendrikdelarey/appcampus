using AppCampus.Domain.Interfaces.Components;
using AppCampus.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Infrastructure.Components
{
    public class ImageComponent : IImageComponent
    {
        public string GetImageFromId(Guid imageId) 
        {
            using (var context = new AppCampusContext()) 
            {
                var image = context.Images.Find(imageId);

                if (image == null) 
                {
                    throw new ArgumentNullException("imageId", String.Format("No Image with Id = {0} exists.", imageId));
                }

                return image.Base64Image;
            }
        }

        public string GetNameFromId(Guid imageId)
        {
            using (var context = new AppCampusContext())
            {
                var image = context.Images.Find(imageId);

                if (image == null)
                {
                    throw new ArgumentNullException("imageId", String.Format("No Image with Id = {0} exists.", imageId));
                }

                return image.Name;
            }
        }

        public bool IsValidImageId(Guid imageId) 
        {
            using (var context = new AppCampusContext())
            {
                return context.Images.Any(i => i.Id == imageId);
            }
        }

        public Guid AddImage(string base64Image, string imageName) 
        {
            using (var context = new AppCampusContext())
            {
                ImageTable imageTable = context.Images.Add(
                    new ImageTable() 
                    {
                        Id = CombIdentityFactory.GenerateIdentity(),
                        Base64Image = base64Image,
                        CreatedDate = DateTime.Now,
                        Name = imageName
                    });
                context.SaveChanges();
                return imageTable.Id;
            }
        }
    }
}
