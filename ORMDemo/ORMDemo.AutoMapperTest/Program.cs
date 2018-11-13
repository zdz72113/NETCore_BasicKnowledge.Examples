using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace ORMDemo.AutoMapperTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddAutoMapper();
            var provider = services.BuildServiceProvider();
            using (var scope = provider.CreateScope())
            {
                var mapper = scope.ServiceProvider.GetService<IMapper>();

                foreach (var typeMap in mapper.ConfigurationProvider.GetAllTypeMaps())
                {
                    Console.WriteLine($"{typeMap.SourceType.Name} -> {typeMap.DestinationType.Name}");
                }

                var user = new UserEntity
                {
                    Id = 2,
                    FirstName = "First",
                    LastName = "Last",
                    BirthDate = DateTime.Today,
                    //AddressList = new List<AddressEntity>
                    //{
                    //    new AddressEntity { City = "ShangHai", Country = "China"},
                    //    new AddressEntity { City = "BeiJing", Country = "China"}
                    //}
                };
                //var userList = new List<UserEntity> {
                //    new UserEntity { Id = 1, Name="Test1" },
                //    new UserEntity { Id = 2, Name="Test2" },
                //};

                var userDTO = mapper.Map<UserDTO>(user);
                var user2 = mapper.Map<UserDTO>(userDTO);

                //mapper.ConfigurationProvider.AssertConfigurationIsValid();
                Console.WriteLine(userDTO.Id + " - " + userDTO.Name + " - " + userDTO.BirthYear + " - " + userDTO.BirthMonth + " - " + userDTO.NickName);
            }



            //var config = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>());
            //var mapper = config.CreateMapper();

            //Mapper.Initialize(cfg => cfg.CreateMap<UserEntity, UserDTO>());

            //var user = new UserEntity
            //{
            //    Id = 1,
            //    Name = "Test"
            //};
            ////var userDTO = mapper.Map<UserDTO>(user);
            //var userDTO = Mapper.Map<UserDTO>(user);

            //Console.WriteLine(userDTO.Id + " - " + userDTO.Name);

            Console.ReadKey();
        }
    }

    public class UserEntity
    {
        public int Id { get; set; }
        //public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        //public List<AddressEntity> AddressList { get; set; }
    }

    public class AddressEntity
    {
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string BirthYear { get; set; }
        public string BirthMonth { get; set; }

        public string NickName { get; set; }
        //public string AddressCity { get; set; }
        //public string AddressCountry { get; set; }
        //public List<AddressDTO> AddressList { get; set; }
    }

    public class AddressDTO
    {
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //CreateMap<AddressEntity, AddressDTO>();
            CreateMap<UserEntity, UserDTO>()
                .ForMember(d => d.Name, o => o.ResolveUsing<UserNameResolver>());
                //.ForMember(d => d.NickName, o => o.Ignore())
                //.ForMember(d => d.BirthYear, o => o.MapFrom(s => s.BirthDate.Year))
                //.ForMember(d => d.BirthMonth, o => o.MapFrom(s => s.BirthDate.Month))
                //.ReverseMap();

            //.BeforeMap((s, d) => s.BirthDate = s.BirthDate.AddYears(-12))
            //.AfterMap((s, d) => d.BirthMonth = "July");
            //.ForMember(d => d.Name, o => o.NullSubstitute("Default Name"))
            //.ForMember(d => d.Name, o => o.AddTransform(val => string.Format("Name: {0}", val)));
        }
    }

    public class UserNameResolver : IValueResolver<UserEntity, UserDTO, string>
    {
        public string Resolve(UserEntity source, UserDTO destination, string destMember, ResolutionContext context)
        {
            if (source != null && !string.IsNullOrEmpty(source.FirstName) && !string.IsNullOrEmpty(source.LastName))
            {
                return string.Format("{0} {1}", source.FirstName, source.LastName);
            }

            return string.Empty;
        }
    }
}
