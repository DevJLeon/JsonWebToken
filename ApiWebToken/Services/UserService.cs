using System;
using ApiWebToken.Helpers;
using Dominio.Interfaces;
using Dominio.Entities;
using ApiWebToken.Dtos;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiWebToken.Services;

public class UserService : IUserService
{
    private readonly JWT _jwt;
    public readonly IUnitOfWork _unitOfWork;
    public readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt,IPasswordHasher<User>passwordHasher)
    {
        _jwt = jwt.Value;
        _unitOfWork=unitOfWork;
        _passwordHasher=passwordHasher;
    }

    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        var usuario = new User
        {
            Email=registerDto.Email,
            UserName=registerDto.Username,
        };
        usuario.Password=_passwordHasher.HashPassword(usuario,registerDto.Password);
        var usuarioExiste=_unitOfWork.Users
                                        .Find(u=>u.UserName.ToLower()==registerDto.Username.ToLower())
                                        .FirstOrDefault();
        if(usuarioExiste==null)
        {
            try 
            {
                _unitOfWork.Users.Add(usuario);
                await _unitOfWork.SaveAsync();

                return $"El usuario {registerDto.Username} ha sido registrado exitosamente";
            }
            catch (Exception ex)
            {
                var message=ex.Message;
                return $"Error:{message}";
            }
        }
        else
        {
            return $"El Usuario con {registerDto.Username} ya se encuentra registrado. ";
        }
    }

    public async Task<string> AddRoleAsync(AddRoleDto model)
    {
        var usuario = await _unitOfWork.Users
                                .GetByUsernameAsync(model.Username);
        
        if(usuario == null)
        {
            return $"No existe algun usuario registrado con la cuenta olvido algun caracter?{model.Username}";
        }

        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);

        if (resultado == PasswordVerificationResult.Success)
        {
            var rolExiste = _unitOfWork.Rols
                                            .Find(u=>u.Nombre.ToLower()==model.Rol.ToLower())
                                            .FirstOrDefault();
            if(rolExiste != null)
            {
                var usuarioTieneRol= usuario.Rols
                                            .Any(u=>u.Id == rolExiste.Id);
                if(usuarioTieneRol == false)
                {
                    usuario.Rols.Add(rolExiste);
                    _unitOfWork.Users.Update(usuario);
                    await _unitOfWork.SaveAsync();
                }
                return $"{model.Rol} agregado a la cuenta {model.Username} de forma exitosa.";
            }
            return $"Rol {model.Rol} no encontrado. ";
        }
        return $"Credenciales incorrectas para el usuario {usuario.UserName}. ";
    }


    public async Task<DataUserDto> GetTokenAsync(LoginDto model)
    {
        DataUserDto dataUserDto= new DataUserDto();

        var usuario = await _unitOfWork.Users
                                .GetByUsernameAsync(model.Username);
        
        if(usuario == null)
        {
            dataUserDto.EstaAutenticado = false;
            dataUserDto.Mensaje=$"No existe ningun usuario con el username{model.Username}.";
            return dataUserDto;
        }

        var result= _passwordHasher.VerifyHashedPassword(usuario,usuario.Password,model.Password);
        if(result == PasswordVerificationResult.Success)
        {
            dataUserDto.Mensaje = "Ok";
            dataUserDto.EstaAutenticado=true;
            if(usuario != null)
            {
                JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
                dataUserDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                dataUserDto.Username = usuario.UserName;
                dataUserDto.Email=usuario.Email;
                dataUserDto.Roles = usuario.Rols
                                            .Select(p=>p.Nombre)
                                            .ToList();
                return dataUserDto;
            }
            else
            {
                dataUserDto.EstaAutenticado = false;
                dataUserDto.Mensaje=$"Credenciales incorrectas para el usuario{usuario.UserName}";
                return dataUserDto;
            }
        }
        
        dataUserDto.EstaAutenticado=false;
        dataUserDto.Mensaje=$"Credenciales incorrectas para el usuario {usuario.UserName}.";

        return dataUserDto;

    }

    private JwtSecurityToken CreateJwtToken(User usuario)
    {
        if(usuario = null)
        {
            throw new ArgumentNullException(nameof(usuario), "El usuario no puede ser nulo");
        }
        var roles = usuario.Rols;
        var roleClaims = new List<Claim>();

        foreach (var role in roles) 
        {
            
        }
    }
}