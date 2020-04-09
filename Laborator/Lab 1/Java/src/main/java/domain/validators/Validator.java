package domain.validators;

import domain.Entity;

public interface Validator<ID, E extends Entity<ID>> {
    void validate(E entity) throws ValidationException;
}
