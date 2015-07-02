using AppCampus.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.Entities
{
    public class Announcement : DomainEntity<Guid>, IAggregateRoot
    {
        private readonly List<Guid> signboardIds;
        private string message;
        private string name;

        public IReadOnlyCollection<Guid> SignboardIds
        {
            get
            {
                return signboardIds;
            }
        }

        public Guid CompanyId { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public Severity Severity { get; private set; }

        public string Message
        {
            get
            {
                return message;
            }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("value", "Announcement message may not be null or empty");
                }
                else
                {
                    message = value;
                }
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("value", "Announcement name may not be null or empty");
                }
                else
                {
                    name = value;
                }
            }
        }

        public bool IsActive { get; private set; }

        public bool IsDeleted { get; private set; }

        public Announcement(Guid companyId, string message, string name, Severity severity, DateTime startDate, DateTime endDate)
            : base(CombIdentityFactory.GenerateIdentity())
        {
            CompanyId = companyId;

            this.signboardIds = new List<Guid>();

            IsActive = false;

            IsDeleted = false;

            SetSeverity(severity);

            SetMessage(message);

            SetName(name);

            SetDateRange(startDate, endDate);
        }

        private Announcement(Guid id, Guid companyId, string message, string name, Severity severity, DateTime startDate, DateTime endDate, IEnumerable<Guid> signboardIds, bool isActive, bool isDeleted)
            : base(id)
        {
            CompanyId = companyId;

            this.signboardIds = signboardIds.ToList();

            IsActive = isActive;

            IsDeleted = isDeleted;

            SetSeverity(severity);

            SetMessage(message);

            SetName(name);

            SetDateRange(startDate, endDate);
        }

        public void SetSeverity(Severity severity)
        {
            Severity = severity;
        }

        public void SetSeverity(string severity)
        {
            if (String.IsNullOrWhiteSpace(severity))
            {
                throw new ArgumentNullException("severity", "Severity cannot be null or empty.");
            }

            try
            {
                Severity = (Severity)Enum.Parse(typeof(Severity), severity, true);
            }
            catch (OverflowException)
            {
                throw new ArgumentOutOfRangeException("severity", String.Format("'{0}' is not a valid severity type.", severity));
            }
        }

        public void SetMessage(string newMessage)
        {
            Message = newMessage;
        }

        public void SetName(string newName)
        {
            Name = newName;
        }

        public void SetDateRange(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
            {
                throw new ArgumentOutOfRangeException("endDate", "End date must be after Start date.");
            }

            StartDate = startDate;
            EndDate = endDate;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public void AssignToSignboard(Signboard signboard)
        {
            if (signboard == null)
            {
                throw new ArgumentNullException("signboard", "Signboard may not be null");
            }

            if (signboardIds.Contains(signboard.Id))
            {
                throw new ArgumentOutOfRangeException("signboard", "Announcement is already assigned to signboard.");
            }

            if (!signboardIds.Contains(signboard.Id))
            {
                signboardIds.Add(signboard.Id);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public void UnassignSignboard(Signboard signboard)
        {
            if (signboard == null)
            {
                throw new ArgumentNullException("signboard", "Signboard may not be null");
            }

            if (!signboardIds.Contains(signboard.Id))
            {
                throw new ArgumentOutOfRangeException("signboard", "Announcement is not assinged to Signboard.");
            }

            signboardIds.Remove(signboard.Id);
        }

        public void AssignToSignboards(IEnumerable<Signboard> signboards)
        {
            if (signboards == null || signboards.Count() == 0)
            {
                throw new ArgumentNullException("signboards", "Can not assign Announcement to a null or empty list of signboards.");
            }

            if (signboards.Select(x => x.Id).ToList().Intersect(SignboardIds).Count() != 0 || signboards.Count() != signboards.Distinct().Count())
            {
                throw new ArgumentOutOfRangeException("signboards", "Error assigning multiples of the same Announcement to the same Signboard. Signboard(s) may already contain the Announcement you are trying to assign to.");
            }

            signboardIds.AddRange(signboards.Select(x => x.Id));
        }

        public void UnassignSignboards(IEnumerable<Signboard> signboards)
        {
            if (signboards == null || signboards.Count() == 0)
            {
                throw new ArgumentNullException("signboards", "No signboards selected to un-assign.");
            }

            if (signboards.Select(x => x.Id).ToList().Intersect(SignboardIds).Count() == 0)
            {
                throw new ArgumentOutOfRangeException("signboards", "You cant un-assign an Announcement to Signboard(s) that arent assigned to the Announcement.");
            }

            foreach (Signboard signboard in signboards)
            {
                signboardIds.Remove(signboard.Id);
            }
        }

        public void DeleteAnnouncement()
        {
            IsDeleted = true;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public static Announcement Hydrate(Guid id, Guid companyId, string message, string name, Severity severity, DateTime startDate, DateTime endDate, IEnumerable<Guid> signboardIds, bool isActive, bool isDeleted)
        {
            return new Announcement(id, companyId, message, name, severity, startDate, endDate, signboardIds, isActive, isDeleted);
        }
    }
}