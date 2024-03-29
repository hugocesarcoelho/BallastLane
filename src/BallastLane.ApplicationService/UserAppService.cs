﻿using AutoMapper;
using BallastLane.ApplicationService.Interface;
using BallastLane.Domain.Model;
using BallastLane.Infrastructure.Data.Repository.Interface;
using Domain.ValueObjects;
using BallastLane.ApplicationService.Dto.User;
using BallastLane.ApplicationService.Base;
using BallastLane.ApplicationService.Service;
using BallastLane.Domain.Settings;

namespace BallastLane.ApplicationService
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectSettings _projectSettings;
        private readonly IMapper _mapper;

        public UserAppService(IUserRepository userRepository, IProjectSettings projectSettings, IMapper mapper)
        {
            _userRepository = userRepository;
            _projectSettings = projectSettings;
            _mapper = mapper;
        }

        public async Task<Result<UserOutputDto>> CreateAsync(UserCreateInputDto inputDto)
        {
            var user = await _userRepository.GetByUsernameAsync(inputDto.Username);

            if (user is not null)
            {
                return new Result<UserOutputDto>(MessageUtils.RecordAlreadyExistsCode, MessageUtils.RecordAlreadyExistsMessage);
            }

            var model = _mapper.Map<UserCreateInputDto, User>(inputDto);

            model.Password = PasswordHasher.HashPassword(model.Password, _projectSettings.PasswordSalt);

            var response = await _userRepository.CreateAsync(model);

            var outputDto = _mapper.Map<User, UserOutputDto>(response);

            return new Result<UserOutputDto>(outputDto);
        }

        public async Task DeleteAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserOutputDto>> GetAllAsync(int offset, int fetch)
        {
            var response = await _userRepository.GetAllAsync(offset, fetch);
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserOutputDto>>(response);
        }

        public async Task<UserOutputDto> GetByIdAsync(string id)
        {
            var response = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<User, UserOutputDto>(response);
        }

        public async Task<UserOutputDto> GetByUsernameAsync(string username)
        {
            var response = await _userRepository.GetByUsernameAsync(username);
            return _mapper.Map<User, UserOutputDto>(response);
        }

        public async Task<Result> UpdateAsync(string id, UserUpdateInputDto inputDto)
        {
            var model = _mapper.Map<UserUpdateInputDto, User>(inputDto);

            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                return new Result(MessageUtils.NotFoundCode, MessageUtils.NotFoundMessage);
            }

            await _userRepository.UpdateAsync(id, model);

            return new Result();
        }

        public async Task<bool> ValidateAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user is null)
            {
                return false;
            }
            return user.Password == PasswordHasher.HashPassword(password, _projectSettings.PasswordSalt);
        }
    }
}
