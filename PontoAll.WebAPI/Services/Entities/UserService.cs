using AutoMapper;
using PontoAll.WebAPI.Data.Interfaces;
using PontoAll.WebAPI.Objects.Contracts;
using PontoAll.WebAPI.Objects.Dtos.Entities;
using PontoAll.WebAPI.Objects.Models;
using PontoAll.WebAPI.Objects.Utils;
using PontoAll.WebAPI.Services.Interfaces;

namespace PontoAll.WebAPI.Services.Entities;

public class UserService : GenericService<User, UserDTO>, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _userRepository = repository;
        _mapper = mapper;
    }

    public async Task<UserDTO> GetByEmail(string email)
    {
        var user = await _userRepository.GetByEmail(email);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO?> Login(Login login)
    {
        var user = await _userRepository.GetByEmail(login.Email);
        if (user == null || user.Password != login.Password)
        {
            return null;
        }
        return _mapper.Map<UserDTO>(user);
    }

    public async Task Create(UserDTO dto)
    {
        UserValidator.Validate(dto);
        await base.Create(dto);
    }

    public async Task Update(UserDTO dto, int id)
    {
        UserValidator.Validate(dto);
        await base.Update(dto, id);
    }
}