using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.ResponseModels
{
    /// <summary>
    /// The Timetable Entry reponse model
    /// </summary>
    public class TimetableEntryResponseModel
    {
        /// <summary>
        /// The Departure Time 
        /// </summary>
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// The Route Name 
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// The Destination Stop name
        /// </summary>
        public string Destination { get; set; }
    }
}