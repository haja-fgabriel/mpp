package dto;

import Domain.Admin;
import Domain.Child;
import Domain.Event;

public class DtoUtils {
    public static Child getFromDTO(ChildDto childDto){
        int id=childDto.getId();
        String name=childDto.getName();
        int age=childDto.getAge();
        int idEv1=childDto.getIdEvent1();
        int idEv2=childDto.getIdEvent2();
        return new Child(id,name,age,idEv1,idEv2);

    }
    public static ChildDto getDTO(Child c){
        int id=c.getId();
        String name=c.getName();
        int age=c.getAge();
        int idEv1=c.getIdEvent1();
        int idEv2=c.getIdEvent2();
        return new ChildDto(id,name,age,idEv1,idEv2);
    }

    public static Event getFromDTO(EventDto eventDto)
    {
        return new Event(eventDto.getId(),eventDto.getAgeMin(),eventDto.getAgeMax(),eventDto.getDistance(),eventDto.getNo());
    }
    public static EventDto getDTO(Event e)
    {
        return new EventDto(e.getId(),e.getAgeMin(),e.getAgeMax(),e.getDistance(),e.getNo());
    }

    public static Admin getFromDTO(AdminDto adminDto)
    {
        return new Admin(adminDto.getId(),adminDto.getName(),adminDto.getPass());
    }
    public static  AdminDto getDTO(Admin a)
    {
        return new AdminDto(a.getId(),a.getPassword(),a.getName());
    }
    public static EventDto[] getDTO(Event[] events)
    {
        EventDto[] friends=new EventDto[events.length];
        for(int i=0;i<events.length;i++){
            friends[i]=getDTO(events[i]);
        }
        return friends;
    }
    public static Event[] getFromDTO(EventDto[] events)
    {
        Event[] friends=new Event[events.length];
        for(int i=0;i<events.length;i++){
            friends[i]=getFromDTO(events[i]);
        }
        return friends;
    }
    public static Child[] getFromDTO(ChildDto[] events)
    {
        Child[] friends=new Child[events.length];
        for(int i=0;i<events.length;i++){
            friends[i]=getFromDTO(events[i]);
        }
        return friends;
    }
    public static ChildDto[] getDTO(Child[] events)
    {
        ChildDto[] friends=new ChildDto[events.length];
        for(int i=0;i<events.length;i++){
            friends[i]=getDTO(events[i]);
        }
        return friends;
    }
}
