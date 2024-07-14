﻿using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace App1
{
    public static class HttpService
    {
        public static object httpClientLocker;
        public static HttpClient httpClient;
        static HttpService()
        {
            httpClientLocker = new object();
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
        }
        public static class Post
        {
            public static async Task<Models.Post> GetPost(int postId)
            {
                bool lockWasTaken = false;

                try
                {
                    System.Threading.Monitor.Enter(httpClientLocker, ref lockWasTaken);
                    HttpResponseMessage responseMessage = await httpClient.GetAsync($"posts/{postId}").ConfigureAwait(false);
                    if (responseMessage.IsSuccessStatusCode == false)
                    {
                        throw new Exception($"Task<Post> GetPost(postId={postId}). Status code: {responseMessage.StatusCode}");
                    }
                    Models.Post post = responseMessage.Content.ReadFromJsonAsync<Models.Post>().Result;
                    return post;
                }
                finally
                {
                    if (lockWasTaken) System.Threading.Monitor.Exit(httpClientLocker);
                }
            }
        }
    }
}