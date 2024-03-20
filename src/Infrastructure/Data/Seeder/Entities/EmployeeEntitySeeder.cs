using MRA.AssetsManagement.Application.Data;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Infrastructure.Data.Seeder.Entities;

public class EmployeeEntitySeeder : EntitySeeder<Employee>
{
    public EmployeeEntitySeeder(IRepository<Employee> repository) : base(repository)
    {
        
    }
    public override async Task Development()
    {
        if (await _repository.Any()) return;

        await _repository.CreateAsync(
            new Employee { FirstName = "Durdona", LastName = "Uzoqova", Email = "durdona@silkroadprofessionals.com" },
            new Employee { FirstName = "Doniyor", LastName = "Niyozov", Email = "niazovd@gmail.com" },
            new Employee { FirstName = "Zubaidullo", LastName = "Nematov", Email = "zubaidullo.nematov@silkroadprofessionals.com" },
            new Employee { FirstName = "Alisa", LastName = "Esh", Email = "alisa.esh@silkroadprofessionals.com" },
            new Employee { FirstName = "Abbos", LastName = "Kamolov", Email = "abbosk@silkroadprofessionals.com" },
            new Employee { FirstName = "Rahmatillo", LastName = "Abduqodirov", Email = "rahmatillo.abduqodirov@silkroadprofessionals.com" },
            new Employee { FirstName = "Parvina", LastName = "Zulfikorova", Email = "parvina@silkroadprofessionals.com" },
            new Employee { FirstName = "Ghanijon", LastName = "Safarov", Email = "ghanijon.safarov@silkroadprofessionals.com" },
            new Employee { FirstName = "Mirolim", LastName = "Majidov", Email = "Mirolim@silkroadprofessionals.com" },
            new Employee { FirstName = "Muhammadislom", LastName = "Ismatov", Email = "muhammadislom.ismatov@silkroadprofessionals.com" },
            new Employee { FirstName = "Abbos", LastName = "Sidiqov", Email = "abbosidiqov@silkroadprofessionals.com" },
            new Employee { FirstName = "Bohirjon", LastName = "Ahmedov", Email = "bohir@silkroadprofessionals.com" },
            new Employee { FirstName = "Usmonjon", LastName = "Nurmatov", Email = "usmonjonn@silkroadprofessionals.com" },
            new Employee { FirstName = "Tursunhuja", LastName = "Norhujaev", Email = "tursunhujan@silkroadprofessionals.com" },
            new Employee { FirstName = "Sanjar", LastName = "Akhmedov", Email = "sanjar@silkroadprofessionals.com" },
            new Employee { FirstName = "Islomjon", LastName = "Makhsudov", Email = "islomjon.makhsudov@silkroadprofessionals.com" },
            new Employee { FirstName = "Mirzodaler", LastName = "Muhsinzoda", Email = "mirzodalerm@silkroadprofessionals.com" },
            new Employee { FirstName = "Shuhrat", LastName = "Rahmonov", Email = "shuhrat@silkroadprofessionals.com" },
            new Employee { FirstName = "Samuel", LastName = "Rivera", Email = "samuel.rivera@silkroadprofessionals.com" },
            new Employee { FirstName = "Malika", LastName = "Mukhsinova", Email = "malika@silkroadprofessionals.com" },
            new Employee { FirstName = "Nizomjon", LastName = "Rahmonberdiev", Email = "nizomjon@silkroadprofessionals.com" },
            new Employee { FirstName = "Olga", LastName = "Khakimova", Email = "o.khakimova@silkroadprofessionals.com" },
            new Employee { FirstName = "Abdurasul", LastName = "Isoqov", Email = "abdurasul@silkroadprofessionals.com" },
            new Employee { FirstName = "Komiljon", LastName = "Najmitdinov", Email = "komiljon.najmitdinov@silkroadprofessionals.com" },
            new Employee { FirstName = "Azimjon", LastName = "Faizulloev", Email = "azimjon.faizulloev@silkroadprofessionals.com" },
            new Employee { FirstName = "Doniyor", LastName = "Niyozov", Email = "doniyor@silkroadprofessionals.com" },
            new Employee { FirstName = "Dalerjon", LastName = "Olimov", Email = "dalerjon.olimov@silkroadprofessionals.com" },
            new Employee { FirstName = "Muhammad", LastName = "Abdugafforov", Email = "muhammad@silkroadprofessionals.com" },
            new Employee { FirstName = "Orasta", LastName = "Mirdadoeva", Email = "orasta@silkroadprofessionals.com" },
            new Employee { FirstName = "Asliya", LastName = "Boturova", Email = "asliya@silkroadprofessionals.com" }
       
        );
    }

    public override Task Production()
    {
        return base.Production();
    }
}