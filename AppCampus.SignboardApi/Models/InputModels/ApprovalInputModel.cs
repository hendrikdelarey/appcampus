using System;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.SignboardApi.Models.QueryModels
{
    /// <summary>
    /// The Approva; input model.
    /// </summary>
    public class ApprovalInputModel
    {
        /// <summary>
        /// The CompanyId of the signboard
        /// </summary>
        [Required]
        public Guid CompanyId { get; set; }

        /// <summary>
        /// The Comment of the signboard. 
        /// It is nullable
        /// </summary>
        public string Comment { get; set; }
    }
}