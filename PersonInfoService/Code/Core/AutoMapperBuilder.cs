using AutoMapper;
using Domain;

namespace PersonInfoService.Host.Code.Core
{
    public static class AutoMapperBuilder
    {
        public static void RegisterAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Person, PersonTransferObject>();
                cfg.CreateMap<Address, AddressTransferObject>();
            });
        }
    }
}
