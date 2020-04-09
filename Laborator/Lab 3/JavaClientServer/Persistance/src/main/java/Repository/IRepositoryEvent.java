package Repository;

import java.util.List;

public interface IRepositoryEvent<ID,E> {
    List<E> findAll();
    E find(ID id);

}
