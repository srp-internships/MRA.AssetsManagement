using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MRA.AssetsManagement.Domain.Entities.Employee;

namespace MRA.AssetsManagement.Infrastructure.Data.Configurations;

public class EmployeeConfiguration : BaseConfiguration<Employee>
{
    public EmployeeConfiguration(IMongoDatabase database, string collectionName) : base(database, collectionName)
    {
    }

    protected override void RegisterClassMap(BsonClassMap<Employee> classMap)
    {
        base.RegisterClassMap(classMap);
        classMap.MapIdMember(x => x.Id);
        classMap.MapMember(x => x.FirstName).SetElementName("firstName");
        classMap.MapMember(x => x.LastName).SetElementName("lastName");
        classMap.MapMember(x => x.UserName).SetElementName("userName");
        classMap.MapMember(x => x.Email).SetElementName("email");
        classMap.MapMember(x => x.Password).SetElementName("password");
        classMap.MapMember(x => x.PhoneNumber).SetElementName("phoneNumber");
        classMap.MapMember(x => x.CanLogin).SetElementName("canLogin");
    }
} 