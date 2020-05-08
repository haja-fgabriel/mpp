package Services;

import Domain.Child;

public interface IObserver {
    void childSaved(Child c) throws Error;
}
