﻿namespace LitHub.DB.Model
{
    public interface IIdentity
    {
        string Login { get; set; }
        byte[] Password { get; set; }
    }
}
