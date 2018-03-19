﻿using OnlineStore.Models;

namespace OnlineStore.Core.Contracts
{
    public interface IUserSessionService
    {
        User User { get; }

        void SetLoggedUser(User user);

        void Logout();

        bool UserIsAdmin();

        bool UserIsModerator();
    }
}