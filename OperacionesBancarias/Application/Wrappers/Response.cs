﻿namespace Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {

        }
        public Response(T data, string? messages = null)
        {
            Succeeded = true;
            Messages = messages;
            Data = data;


        }

        public Response(string messages)
        {
            Succeeded = false;
            Messages = messages;
        }

        public bool Succeeded { get; set; }
        public string? Messages { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }

    }
}
