using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pacagroup.Ecommerce.Application.Main
{
    public class UsersApplication : IUsersApplication
    {
        #region Fields
        private readonly IUsersDomain _usersDomain;
        private readonly IMapper _mapper;
        #endregion
        #region Ctor
        public UsersApplication(IUsersDomain usersDomain,IMapper mapper)
        {
            _usersDomain = usersDomain;
            _mapper = mapper;
        }
        #endregion
        public Response<UsersDTO> Authenticate(string username, string password)
        {
            var response = new Response<UsersDTO>();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                response.Message = "Los parametros de login no pueden estar vacios";
                return response;
            }
            try
            {
                response.Data = _mapper.Map<UsersDTO>(_usersDomain.Authenticate(username, password));
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Autenticacion correcta";
                }
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = true;
                response.Message = "El usuario y contraseña no existen";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
