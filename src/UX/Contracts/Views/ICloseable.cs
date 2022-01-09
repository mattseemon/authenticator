using System;
using System.Collections.Generic;
using System.Text;

namespace Seemon.Authenticator.Contracts.Views
{
    public interface ICloseable
    {
        void CloseDialog(bool? response);
    }
}
