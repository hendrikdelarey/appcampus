using AppCampus.Domain.Interfaces.Components;
using AppCampus.Domain.Models.Enums;
using AppCampus.PortalApi.Filters;
using AppCampus.PortalApi.Models.QueryModels;
using AppCampus.PortalApi.Models.ResponseModels;
using System;
using System.Web.Http;
using System.Web.Http.Description;

namespace AppCampus.PortalApi.Controllers
{
    /// <summary>
    /// Images that are uploaded as resources for the ImageWidget.
    /// </summary>
    [RoutePrefix("api/v1/widgetcontent/images")]
    [AuthoriseRoles(RoleClassification.GeneralUser)]
    public class ImageController : ApiController
    {
        private IImageComponent ImageComponent;

        public ImageController(IImageComponent imageComponent)
        {
            ImageComponent = imageComponent;
        }

        /// <summary>
        /// Creates an Image.
        /// </summary>
        /// <param name="imageId">Image Identifier.</param>
        /// <returns>ImageModel that contains the created Image</returns>
        [Route("{imageId:Guid}", Name = "GetImage")]
        [ResponseType(typeof(ImageModel))]
        public IHttpActionResult Get(Guid imageId)
        {
            string base64Image;

            base64Image = ImageComponent.GetImageFromId(imageId);

            if (base64Image == null)
            {
                return NotFound();
            }

            string imageName = ImageComponent.GetNameFromId(imageId);

            if (imageName == null)
            {
                return NotFound();
            }

            var response = ImageModel.From(imageId, base64Image, imageName);

            return Ok(response);
        }

        /// <summary>
        /// Creates an Image.
        /// </summary>
        /// <param name="image">Base64 Image String</param>
        /// <returns>Guid: The Image Identifier</returns>
        [Route]
        [ResponseType(typeof(ImageModel))]
        public IHttpActionResult Post(ImageInputModel image)
        {
            Guid imageId = ImageComponent.AddImage(image.Base64Image, image.Name);

            var response = ImageModel.FromExcludingBase64(imageId, image.Name);

            return Created(new Uri(Url.Link("GetImage", new { imageId = imageId })), response);
        }
    }
}