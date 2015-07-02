using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.Entities
{
    public class DeviceLog : DomainEntity<Guid>, IAggregateRoot
    {
        public string FileName { get; private set; }

        public DeviceLog(String filename)
            : this(CombIdentityFactory.GenerateIdentity(), filename)
        {
        }

        private DeviceLog(Guid id, string filename)
            : base(id)
        {
            FileName = filename;
        }

        public static DeviceLog Hydrate(Guid id, string filename)
        {
            return new DeviceLog(id, filename);
        }
    }
}
