using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Networking.Protocols.Protobuf
{
    using AthletesResponseType = AthletesResponse.Types.ResponseType;

    public class ProtobufUtils
    {
        public static Model.Admin GetAdmin(AthletesRequest request)
        {
            return new Model.Admin(request.User.Id, request.User.Password);
        }

        public static Model.Child GetChild(AthletesRequest request)
        {
            Child c = request.Child;
            return new Model.Child(c.Id, c.Name, c.Age, c.IdEvent1, c.IdEvent2);
        }

        public static AthletesResponse CreateOkResponse()
        {
            return new AthletesResponse
            {
                Type = AthletesResponseType.Ok
            };
        }

        public static AthletesResponse CreateErrorResponse(string message)
        {
            return new AthletesResponse
            {
                Type = AthletesResponseType.Error,
                Error = message
            };
        }

        public static AthletesResponse CreateSavedChildResponse(Model.Child c)
        {
            return new AthletesResponse
            {
                Type = AthletesResponseType.SavedChild,
                Child = CreateProtobufChild(c)
            };
        }

        public static AthletesResponse CreateCounterOfChildrenResponse(int count)
        {
            return new AthletesResponse
            {
                Type = AthletesResponseType.CounterOfChildren,
                ChildCount = count
            };
        }

        public static AthletesResponse CreateListOfEventsResponse(Model.Event[] events)
        {
            AthletesResponse response = new AthletesResponse
            {
                Type = AthletesResponseType.ListOfEvents
            };
            foreach (Model.Event ev in events)
            {
                response.ListOfEvents.Add(CreateProtobufEvent(ev));
            }
            return response;
        }

        public static AthletesResponse CreateListOfChildrenResponse(Model.Child[] children)
        {
            AthletesResponse response = new AthletesResponse
            {
                Type = AthletesResponseType.ListOfEvents
            };
            foreach (Model.Child c in children)
            {
                response.ListOfChildren.Add(CreateProtobufChild(c));
            }
            return response;
        }

        private static Child CreateProtobufChild(Model.Child c)
        {
            return new Child
            {
                Id = c.ID,
                Age = c.Age,
                IdEvent1 = c.IdEvent1,
                IdEvent2 = c.IdEvent2,
                Name = c.Name
            };
        }

        private static Event CreateProtobufEvent(Model.Event ev)
        {
            return new Event
            {
                Id = ev.ID,
                AgeMin = ev.AgeMin,
                AgeMax = ev.AgeMax,
                Distance = ev.Distance,
                No = ev.No
            };
        }

        public static AthletesResponse CreateNewChildResponse(Model.Child c)
        {
            return new AthletesResponse
            {
                Type = AthletesResponseType.NewChild,
                Child = new Child { Id = c.ID, Age = c.Age, IdEvent1 = c.IdEvent1, IdEvent2 = c.IdEvent2, Name = c.Name }
            };
        }
    }
}
