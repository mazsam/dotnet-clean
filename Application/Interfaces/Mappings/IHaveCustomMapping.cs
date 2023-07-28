using AutoMapper;

namespace VendorBoilerplate.Application.Interfaces.Mappings
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile profile);
    }
}