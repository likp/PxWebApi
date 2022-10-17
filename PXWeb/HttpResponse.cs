﻿namespace PxWeb
{
    public class CachedResponse
    {
        public string content { get; set; }
        public string contentType { get; set; }
        public int responseCode { get; set; }
        public CachedResponse(string content, string responseType, int responseCode)
        {
            this.content = content;
            this.contentType = responseType;
            this.responseCode = responseCode;
        }
    }
}
