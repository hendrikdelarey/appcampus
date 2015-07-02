using AppCampus.Domain.Models.Enums;
using System;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.ValueObjects
{
    public class Request : IValueObject<Request>
    {
        public Guid Id { get; private set; }

        public RequestType RequestType { get; private set; }

        public RequestState State { get; private set; }

        public string Value { get; private set; }

        public Type ValueType { get; private set; }

        public DateTime CreatedDate { get; private set; }

        private static Type getTypeFromType(RequestType type)
        {
            if (type == RequestType.FontFactorUpdate)
            {
                return typeof(float);
            }
            else if (type == RequestType.SoftwareUpdate)
            {
                return typeof(string);
            }
            else
            {
                return typeof(void);
            }
        }

        public static Request From(RequestType type, string value, DateTime createdDate)
        {
            return new Request()
            {
                Id = Guid.NewGuid(),
                RequestType = type,
                State = RequestState.Created,
                Value = value,
                ValueType = getTypeFromType(type),
                CreatedDate = createdDate
            };
        }

        public static Request Hydrate(Guid id, RequestType type, string value, RequestState state, DateTime createdDate)
        {
            return new Request()
            {
                Id = id,
                RequestType = type,
                State = state,
                Value = value,
                ValueType = getTypeFromType(type),
                CreatedDate = createdDate
            };
        }

        public void Cancel()
        {
            if (State != RequestState.Processed && State != RequestState.Failed)
            {
                State = RequestState.Cancelled;
            }
        }

        public void Sent()
        {
            State = RequestState.Sent;
        }

        public void Processed()
        {
            State = RequestState.Processed;
        }

        public void Failed()
        {
            State = RequestState.Failed;
        }

        public bool Equals(Request other)
        {
            return State == other.State &&
                   RequestType == other.RequestType &&
                   Value == other.Value &&
                   ValueType == other.ValueType;
        }
    }
}