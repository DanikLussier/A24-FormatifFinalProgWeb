﻿namespace DixCordesServeur.Models.DTOs
{
    public class PostMessageDTO
    {
        public int ChannelId { get; set; }
        public string Text { get; set; } = null!;
    }
}
