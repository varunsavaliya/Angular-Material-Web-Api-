﻿using Demo.DataModels;

namespace Demo.Repositories.JWT
{
    public interface IJwtHelperRepository
    {
        public string GetJwtToken(User user);
    }
}
