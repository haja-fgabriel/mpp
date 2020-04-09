package Repository;

public interface IRepositoryAdmin<ID,E> {
    public E find(ID username);
}
