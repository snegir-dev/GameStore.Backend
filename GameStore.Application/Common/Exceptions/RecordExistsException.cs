﻿namespace GameStore.Application.Common.Exceptions;

public class RecordExistsException : Exception
{
    public RecordExistsException(string name, object record)
        : base($"Record '{record.ToString()}' of type '{name}' already exists")
    {
    }
}