package repository;

import domain.Entity;
import domain.validators.Validator;

import java.util.HashMap;
import java.util.Map;

public class InMemoryRepository<ID, E extends Entity<ID>> implements CrudRepository<ID, E> {
    private Validator<ID, E> validator;
    private Map<ID, E> entities;

    public InMemoryRepository(Validator<ID, E> validator) {
        this.validator = validator;
        entities = new HashMap<>();
    }

    @Override
    public E findOne(ID id) {
        if (id == null)
            throw new IllegalArgumentException("id is null");
        return entities.get(id);
    }

    @Override
    public Iterable<E> findAll() {
        return entities.values();
    }

    @Override
    public E save(E entity) {
        if(entity == null)
            throw new IllegalArgumentException("entity is null");
        if(entities.containsKey(entity.getId()))
            return entity;
        validator.validate(entity);
        entities.put(entity.getId(), entity);
        return null;
    }

    @Override
    public E delete(ID id) {
        if (id == null)
            throw new IllegalArgumentException("id is null");
        return entities.remove(id);
    }

    @Override
    public E update(E entity) {
        if (entity == null)
            throw new IllegalArgumentException("entity is null");
        validator.validate(entity);
        if (entities.replace(entity.getId(), entity) == null)
            return entity;
        return null;
    }
}
