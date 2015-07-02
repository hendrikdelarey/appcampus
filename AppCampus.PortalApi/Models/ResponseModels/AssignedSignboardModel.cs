using System;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The Assigned Signboard response model.
    /// </summary>
    public class AssignedSignboardModel
    {
        /// <summary>
        /// The Identifier of the Signboard that is displaying the announcement.
        /// </summary>
        public Guid SignboardId { get; set; }

        public static AssignedSignboardModel From(Guid signboardId)
        {
            return new AssignedSignboardModel()
            {
                SignboardId = signboardId
            };
        }
    }
}