using System;
using System.Collections.Generic;

namespace AMAPG4.Models.Command
{
    public interface ICommandLineService : IDisposable
    {
        public List<CommandLine> GetAllCommandLines();
    }
}
