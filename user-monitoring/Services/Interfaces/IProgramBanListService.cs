﻿using user_monitoring.Models;

namespace user_monitoring.Services.Interfaces
{
    public interface IProgramBanListService
    {
        public bool Save(ProgramBanList programBanList);

        public bool Load();
    }
}