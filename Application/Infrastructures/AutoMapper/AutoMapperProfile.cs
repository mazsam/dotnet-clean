using AutoMapper;
using VendorBoilerplate.Application.Interfaces;
using VendorBoilerplate.Application.Misc;
using System.Reflection;

namespace VendorBoilerplate.Application.Infrastructures.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public IvendorDBContext _context { set; get; }
        public Utils _utils { set; get; }
        public AutoMapperProfile(IvendorDBContext context, Utils utils)
        {
            _context = context;
            _utils = utils;

            LoadStandardMappings();
            LoadCustomMappings();
            LoadConverters();
        }

        private void LoadConverters()
        {

        }

        private void LoadStandardMappings()
        {
            var mapsFrom = MapperProfileHelper.LoadStandardMappings(Assembly.GetExecutingAssembly());
            foreach (var map in mapsFrom)
            {
                CreateMap(map.Source, map.Destination).ReverseMap();
            }
        }

        private void LoadCustomMappings()
        {
            var mapsFrom = MapperProfileHelper.LoadCustomMappings(Assembly.GetExecutingAssembly());
            foreach (var map in mapsFrom)
            {
                map.CreateMappings(this);
            }
        }

        public string GetFullUrl(string path)
        {
            return _utils.GetValidUrl(path);
        }
    }
}
