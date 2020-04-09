import domain.AgeCategory;
import domain.Probe;
import domain.validators.ProbeValidator;
import repository.db.ProbeDatabaseRepository;
import utils.config.DatabaseProperties;

public class Main {

    public static void main(String[] args) {
        ProbeDatabaseRepository repository = new ProbeDatabaseRepository(new ProbeValidator());

        Probe probe = repository.findOne("DornaSuperSprint");
        AgeCategory ageCategory = probe.getAgeCategory();
        assert(ageCategory != AgeCategory.AGE_9_TO_11);
        assert(false);
    }
}
