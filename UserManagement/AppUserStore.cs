using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Repositories.Interfaces;
using DtoModels;

namespace UserManagement
{
	public class AppUserStore : IUserStore<AppUser>, IUserPasswordStore<AppUser>
	{
		private readonly IUserRepository _usersRepository;

		public AppUserStore(IUserRepository userRepository)
			=>_usersRepository = userRepository;

		public async Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
		{
			var newUser = await _usersRepository.Add(ToDto(user));

			if (newUser != null)
				return IdentityResult.Success;

			return IdentityResult.Failed(new IdentityError { Code = "400", Description = "fail to create " });
		}

		public async Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
		{
			var removedSuccesfully = await _usersRepository.Remove(user.UserId);

			if (removedSuccesfully)
				return IdentityResult.Success;

			return IdentityResult.Failed(new IdentityError { Code = "404", Description = "already deleted" });
		}

		public async Task<AppUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
		{
			var userDto = await _usersRepository.GetById(userId);
			return ToModel(userDto);
		}

		public async Task<AppUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
		{
			var userDto = await _usersRepository.GetByName(normalizedUserName);
			return ToModel(userDto);
		}

		public Task<string> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.UserName.ToUpper());

		public Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.UserId);

		public Task<string> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.UserName);

		public Task SetNormalizedUserNameAsync(AppUser user, string normalizedName, CancellationToken cancellationToken)
		{
			user.UserName = normalizedName;
			return Task.CompletedTask;
		}

		public Task SetUserNameAsync(AppUser user, string userName, CancellationToken cancellationToken)
		{
			user.UserName = userName;
			return Task.CompletedTask;
		}

		public async Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
			=> await _usersRepository.Update(user.UserId, ToDto(user))
						? IdentityResult.Success
						: IdentityResult.Failed(new IdentityError { Description = "fail to update the user" });

		public void Dispose()
		{
		}

		public Task SetPasswordHashAsync(AppUser user, string passwordHash, CancellationToken cancellationToken)
		{
			user.Password = passwordHash;
			return Task.CompletedTask;
		}

		public Task<string> GetPasswordHashAsync(AppUser user, CancellationToken cancellationToken)
			=> Task.FromResult(user.Password);

		public Task<bool> HasPasswordAsync(AppUser user, CancellationToken cancellationToken)
			=> Task.FromResult(!string.IsNullOrWhiteSpace(user.Password));

		private UserDTO ToDto(AppUser appUser)
			=> new UserDTO
			{
				Id = appUser.UserId,
				Username = appUser.UserName,
				Password = appUser.Password,
				CreationDate = appUser.CreationDate,
				Email = appUser.Email
			};

		private AppUser ToModel(UserDTO userDto)
			=> userDto== null ? null: new AppUser
			{
				UserId = userDto?.Id,
				UserName = userDto.Username,
				Password = userDto.Password,
				CreationDate = userDto.CreationDate,
				Email = userDto.Email
			};
	}
}
