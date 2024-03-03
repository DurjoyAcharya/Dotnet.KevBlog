﻿using Blazored.LocalStorage;
using KevBlog.Contract.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Json;
using System.Text.Json;
namespace KevBlog.WebClient.Services
{
    public class UserClientService
    {
        private HttpClient _httpClient;
        private IAuthService _authService;
        public UserClientService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }
        public async Task<(bool, IEnumerable<IdentityError>?)> RegisterNewUser(RegisterDto registerDto)
        {
            var result = await _httpClient.PostAsJsonAsync($"Account/register", registerDto);
            if (result.IsSuccessStatusCode)
                return (true, null);
            
            var responseJson = await result.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var responseData = JsonSerializer.Deserialize<IEnumerable<IdentityError>>(responseJson, options);
            return (false, responseData);
        }
    }
}
