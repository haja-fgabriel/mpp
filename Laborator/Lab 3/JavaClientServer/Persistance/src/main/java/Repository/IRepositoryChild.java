package Repository;

import java.util.List;

public interface IRepositoryChild<ID,E> {
    public E save(E entity);
    public List<E> filter(int idEv, int min, int max);
    public int countByEvent(int idEv);
}
