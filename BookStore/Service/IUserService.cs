﻿namespace BookStore.Service
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAthunticated();
    }
}