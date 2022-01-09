using System;
using System.Collections.Generic;
using System.Text;

namespace Seemon.Authenticator.Contracts.Services
{
    public interface IApplicationInfoService
    {
        string Title { get; }
        string Version { get; }
        string Author { get; }
        string Description { get; }
        string Copyright { get; }
        string Identifier { get; }
        string ExecutablePath { get; }
        string DataPath { get; }
        string LogPath { get; }
    }
}
