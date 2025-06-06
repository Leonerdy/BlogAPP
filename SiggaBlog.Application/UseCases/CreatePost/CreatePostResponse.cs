﻿namespace SiggaBlog.Application.UseCases.CreatePost
{
    public record CreatePostResponse
    {
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
    }
}
