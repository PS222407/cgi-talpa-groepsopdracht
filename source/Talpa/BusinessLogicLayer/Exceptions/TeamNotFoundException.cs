﻿namespace BusinessLogicLayer.Exceptions;

public class TeamNotFoundException : Exception
{
    public TeamNotFoundException(string message) : base(message)
    {
    }
}