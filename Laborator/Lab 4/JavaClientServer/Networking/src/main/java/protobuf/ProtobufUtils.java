package protobuf;

import Domain.Child;
import Domain.Event;
import utils.Parser;

import java.security.DomainCombiner;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

public class ProtobufUtils {

    public static Domain.Admin getAdmin(AthletesRequestOuterClass.AthletesRequest request) {
        return new Domain.Admin(request.getUser().getId(), "", request.getUser().getPassword());
    }

    public static Domain.Child getChild(Child c) {
        return new Child(c.getId(), c.getName(), c.getAge(), c.getIdEvent1(), c.getIdEvent2());
    }

    public static AthletesResponseOuterClass.AthletesResponse createOkResponse() {
        return AthletesResponseOuterClass.AthletesResponse.newBuilder()
                .setType(AthletesResponseOuterClass.AthletesResponse.ResponseType.Ok)
                .build();
    }

    public static AthletesResponseOuterClass.AthletesResponse createErrorResponse(String message) {
        return AthletesResponseOuterClass.AthletesResponse.newBuilder()
                .setType(AthletesResponseOuterClass.AthletesResponse.ResponseType.Error)
                .setError(message)
                .build();
    }

    public static AthletesResponseOuterClass.AthletesResponse createNewChildResponse(Domain.Child c) {
        return AthletesResponseOuterClass.AthletesResponse.newBuilder()
                .setType(AthletesResponseOuterClass.AthletesResponse.ResponseType.NewChild)
                .setChild(createProtobufChild(c))
                .build();
    }

    private static AthletesModel.Child createProtobufChild(Domain.Child c) {
        return AthletesModel.Child.newBuilder()
                .setId(c.getId())
                .setAge(c.getAge())
                .setName(c.getName())
                .setIdEvent1(c.getIdEvent1())
                .setIdEvent2(c.getIdEvent2())
                .build();
    }

    private static AthletesModel.Admin createProtobufAdmin(Domain.Admin user) {
        return AthletesModel.Admin.newBuilder()
                .setId(user.getId())
                .setPassword(user.getPassword())
                .build();
    }

    /*
     * Currently implemented request builder functions
     */
    public static AthletesRequestOuterClass.AthletesRequest createLoginRequest(Domain.Admin user) {
        return AthletesRequestOuterClass.AthletesRequest.newBuilder()
                .setType(AthletesRequestOuterClass.AthletesRequest.RequestType.Login)
                .setUser(createProtobufAdmin(user))
                .build();
    }

    public static AthletesRequestOuterClass.AthletesRequest createLogoutRequest(Domain.Admin user) {
        return AthletesRequestOuterClass.AthletesRequest.newBuilder()
                .setType(AthletesRequestOuterClass.AthletesRequest.RequestType.Logout)
                .setUser(createProtobufAdmin(user))
                .build();
    }

    public static AthletesRequestOuterClass.AthletesRequest createGetFilteredChildrenRequest(String content) {
        return AthletesRequestOuterClass.AthletesRequest.newBuilder()
                .setType(AthletesRequestOuterClass.AthletesRequest.RequestType.FilterChildren)
                .setData(content)
                .build();
    }

    public static AthletesRequestOuterClass.AthletesRequest createGetAllEventsRequest() {
        return AthletesRequestOuterClass.AthletesRequest.newBuilder()
                .setType(AthletesRequestOuterClass.AthletesRequest.RequestType.GetEvents)
                .build();
    }

    public static AthletesRequestOuterClass.AthletesRequest createCountOfChildrenRequest(int eventID) {
        return AthletesRequestOuterClass.AthletesRequest.newBuilder()
                .setType(AthletesRequestOuterClass.AthletesRequest.RequestType.CountChildren)
                .setEventID(eventID)
                .build();
    }

    public static AthletesRequestOuterClass.AthletesRequest createSaveChildRequest(Domain.Child c) {
        return AthletesRequestOuterClass.AthletesRequest.newBuilder()
                .setType(AthletesRequestOuterClass.AthletesRequest.RequestType.SaveChild)
                .setChild(createProtobufChild(c))
                .build();
    }


    /*
     * Useful methods for creating entities
     */
    public static Child getChild(AthletesResponseOuterClass.AthletesResponse response) {
        AthletesModel.Child childDTO = response.getChild();
        return new Child(childDTO.getId(), childDTO.getName(), childDTO.getAge(), childDTO.getIdEvent1(), childDTO.getIdEvent2());
    }

    public static List<Domain.Child> getListOfChildren(AthletesResponseOuterClass.AthletesResponse response) {
        return StreamSupport.stream(response.getListOfChildrenList().spliterator(), false)
                .map(c -> new Child(c.getId(), c.getName(), c.getAge(), c.getIdEvent1(), c.getIdEvent2()))
                .collect(Collectors.toList());
    }

    public static List<Domain.Event> getListOfEvents(AthletesResponseOuterClass.AthletesResponse response) {
        return StreamSupport.stream(response.getListOfEventsList().spliterator(), false)
                .map(e -> new Event(e.getId(), e.getAgeMin(), e.getAgeMax(), e.getDistance(), e.getNo()))
                .collect(Collectors.toList());
    }


}
