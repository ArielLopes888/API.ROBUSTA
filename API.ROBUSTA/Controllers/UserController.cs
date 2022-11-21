using API.ROBUSTA.Utilities;
using API.ROBUSTA.ViewModels;
using AutoMapper;
using Cor.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Interfaces;

namespace API.ROBUSTA.Controllers
{
    [ApiController]
    public class UserController :ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService uservice, IMapper mapper)
        {
            _userService = uservice;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/api/v1/users/create")]
        public async Task<IActionResult> Create([FromBody]CreateUserViewModel userViewModel)
        {
            try
            {
                var userDTO = _mapper.Map<UserDto>(userViewModel);

                var userCreated = await _userService.Create(userDTO);

                return Ok( new ResultViewModel
                {
                    Success = true,
                    Data = userCreated,
                    Message = "Usuário Criado com Sucesso"
                });

            }
            catch (DomainException ex)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

    }
}
