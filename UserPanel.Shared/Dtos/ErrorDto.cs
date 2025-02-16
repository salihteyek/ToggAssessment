﻿namespace UserPanel.Shared.Dtos
{
    public class ErrorDto
    {
        public List<String> Errors { get; set; } = new List<string>();

        public bool IsShow { get; set; }

        public ErrorDto(string error, bool isShow)
        {
            Errors.Add(error);
            IsShow = isShow;
        }

        public ErrorDto(List<string> errors, bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }
    }
}
