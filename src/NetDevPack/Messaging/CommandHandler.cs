﻿using System.Threading.Tasks;
using FluentValidation.Results;
using NetDevPack.Data;

namespace NetDevPack.Messaging
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected async Task<ValidationResult> Commit(IUnitOfWork uow, string message = null)
        {
            if (message is null) message = "There was an error saving data";
            if (!await uow.Commit()) AddError(message);

            return ValidationResult;
        }
    }
}