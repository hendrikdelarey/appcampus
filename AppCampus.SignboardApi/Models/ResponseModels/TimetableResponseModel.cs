using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.ResponseModels
{
    /// <summary>
    /// The Timetable Response model
    /// </summary>
    public class TimetableResponseModel
    {
        /// <summary>
        /// The Operator model
        /// </summary>
        public OperatorResponseModel Operator { get; set; }

        /// <summary>
        /// The Start Stop Name
        /// </summary>
        public string StopName { get; set; }

        /// <summary>
        /// The List of Timetable Entries
        /// </summary>
        public List<TimetableEntryResponseModel> TimetableEntry { get; set; }

    }
}