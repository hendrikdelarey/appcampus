using AppCampus.Domain.Interfaces.Components;
using AppCampus.SignboardApi.Models.ResponseModels;
using System;
using System.Reflection;
using System.Web.Http;

namespace AppCampus.SignboardApi.Controllers
{
    [RoutePrefix("signboardapi/v1/widgetcontent/images")]
    public class ImagesController : ApiController
    {
        public IImageComponent ImageComponent { get; set; }

        public ILoggingComponent LoggingComponennt { get; set; }

        public ImagesController(IImageComponent imageComponent, ILoggingComponent loggingComponent)
        {
            ImageComponent = imageComponent;
            LoggingComponennt = loggingComponent;
        }

        [Route("{imageId:Guid}")]
        public IHttpActionResult GetImage(Guid imageId)
        {
            if (!ImageComponent.IsValidImageId(imageId))
            {
                LoggingComponennt.LogWarning(MethodBase.GetCurrentMethod(), "Image with Id " + imageId + " is an invalid image.");
                return NotFound();
            }

            var responseModel = ImageResponseModel.From(ImageComponent.GetImageFromId(imageId));
            return Ok(responseModel);
        }
    }
}