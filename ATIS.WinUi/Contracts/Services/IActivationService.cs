﻿namespace ATIS.WinUi.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
