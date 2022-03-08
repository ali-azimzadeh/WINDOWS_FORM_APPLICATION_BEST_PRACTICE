using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public enum ActionLogType
    {
        Add = 0,
        Update = 1,
        Delete = 2,
        Search = 3,
        Print = 4,
        ExitForm = 5,
        OpenForm = 6,
        OpenSystem = 7,
        ExitSystem = 8,
        CardRead = 9,
        CardWrite = 10,
        ThrowError = 11,
        Refresh = 12,
        LockNotFound = 13,
        Help = 14,
        Play = 15,
        CreditCardRead = 16,
        AttendanceCard = 17,
        Test = 18
    }


}
