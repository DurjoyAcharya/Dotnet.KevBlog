﻿using Blazored.LocalStorage;
using KevBlog.Contract.DTOs;
using System.Net.Http.Json;

namespace KevBlog.WebClient.Services
{
    public interface IPostService
    {
        public Task<IEnumerable<PostDisplayDto>> GetPostDisplays();
        public Task<PostDisplayDetailsDto> GetPostDetails(int id);
        public Task<bool> CreatePost(PostCreateDto postCreateDto);
        public Task<bool> UpdatePost(PostUpdateDto postUpdateDto);
    }
    public class PostClientService : IPostService
    {
        private HttpClient _httpClient;
        private IAuthService _authService;
        public PostClientService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService; 
        }
        public async Task<bool> CreatePost(PostCreateDto postCreateDto)
        {
            try
            {
                await _authService.GetBearerToken();
                var result = await _httpClient.PostAsJsonAsync($"Posts", postCreateDto);
                return result.IsSuccessStatusCode;
            }
            catch(Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
        }
        public async Task<bool> UpdatePost(PostUpdateDto postUpdateDto)
        {
            try
            {
                await _authService.GetBearerToken();
                var result = await _httpClient.PutAsJsonAsync($"Posts", postUpdateDto);
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
        }
        public async Task<PostDisplayDetailsDto> GetPostDetails(int id) =>
            await _httpClient.GetFromJsonAsync<PostDisplayDetailsDto>($"Posts/{id}");

        public async Task<IEnumerable<PostDisplayDto>> GetPostDisplays() =>
             await _httpClient.GetFromJsonAsync<IEnumerable<PostDisplayDto>>($"Posts");


    }
}
