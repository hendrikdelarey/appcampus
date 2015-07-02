using AppCampus.Domain.Interfaces.Components;
using AppCampus.Domain.Models.ValueObjects;
using AppCampus.SignboardApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace AppCampus.SignboardApi.Controllers
{
    [RoutePrefix("signboardapi/v1/timetables")]
    public class TimetablesController : ApiController
    {
        private ITimetableComponent TimetableComponent;

        private ILoggingComponent LoggingComponent;

        public TimetablesController(ITimetableComponent timetableComponent, ILoggingComponent loggingComponent)
        {
            TimetableComponent = timetableComponent;
            LoggingComponent = loggingComponent;
        }

        [HttpGet]
        [Route]
        public IHttpActionResult GetTimetable(string operatorName, string stopIdentifier, int numResults)
        {
            IEnumerable<TimetableSchedule> timetableSchedule;

            try
            {
                timetableSchedule = TimetableComponent.GetTimetableSchedule(operatorName, stopIdentifier, numResults);
            }
            catch (InvalidOperationException e)
            {
                LoggingComponent.LogWarning(MethodBase.GetCurrentMethod(), e.Message);
                return BadRequest();
            }

            var responseModel =
                timetableSchedule.FirstOrDefault() == null ?
                    new TimetableResponseModel()
                :
                    new TimetableResponseModel()
                    {
                        Operator = new OperatorResponseModel()
                        {
                            IconId = Guid.Empty,
                            Name = operatorName
                        },
                        StopName = timetableSchedule.First().FirstStop,
                        TimetableEntry = timetableSchedule.Select(x =>
                            new TimetableEntryResponseModel()
                            {
                                DepartureTime = x.DepartureTime,
                                Destination = x.LastStop,
                                RouteName = x.CorridorName
                            }).ToList()
                    };

            return Ok(responseModel);
        }
    }
}