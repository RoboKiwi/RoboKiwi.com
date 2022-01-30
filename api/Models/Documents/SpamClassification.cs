using System;

namespace RoboKiwi.Functions.Models.Documents;

[Flags]
public enum SpamClassification
{
    None = 0,
    PendingCheck = 1,
    ApiMarkedSpam = 2,
    ApiMarkedHam = 4,
    UserMarkedSpam = 8,
    UserMarkedHam = 16
}