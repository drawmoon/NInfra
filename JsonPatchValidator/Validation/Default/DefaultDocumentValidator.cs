﻿using Microsoft.AspNetCore.JsonPatch.Operations;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace JsonPatchValidator.Validation.Default
{
    public class DefaultDocumentValidator : IDocumentValidator
    {
        public virtual bool TryValid([NotNull] Operation operation, out string message)
        {
            if (operation == null) throw new ArgumentNullException(nameof(operation));

            if ((operation.value is string v && string.IsNullOrWhiteSpace(v)) || operation.value == null)
            {
                message = MessageDefaults.ValueCannotBeNull;
                return false;
            }

            if (!VerificationOp(operation.op, out message))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(operation.path))
            {
                message = MessageDefaults.PathCannotBeNull;
                return false;
            }

            message = null;
            return true;
        }

        protected static bool VerificationOp(string op, out string message)
        {
            if (string.IsNullOrWhiteSpace(op))
            {
                message = MessageDefaults.OpCannotBeNull;
                return false;
            }

            if (Enum.GetNames(typeof(OperationType)).Count(
                p => string.Equals(p, op, StringComparison.OrdinalIgnoreCase)) != 1)
            {
                message = MessageDefaults.InvalidOp;
                return false;
            }

            message = null;
            return true;
        }
    }
}