using Microsoft.SqlServer.Server;
using RDotNet;
using RDotNet.Devices;
using RDotNet.Internals;

namespace RStoredProc
{
    class SqlConsole : ICharacterDevice
    {
        public SymbolicExpression AddHistory(Language call, SymbolicExpression operation, Pairlist args, REnvironment environment) { return null; }

        public YesNoCancel Ask(string question) { return YesNoCancel.Cancel; }

        public void Busy(BusyType which) { }

        public void Callback() { }

        public string ChooseFile(bool create) { return null; }

        public void CleanUp(StartupSaveAction saveAction, int status, bool runLast) { }

        public void ClearErrorConsole() { }

        public void EditFile(string file) { }

        public void FlushConsole() { }

        public SymbolicExpression LoadHistory(Language call, SymbolicExpression operation, Pairlist args, REnvironment environment) { return null; }

        public string ReadConsole(string prompt, int capacity, bool history) { return null; }

        public void ResetConsole() { }

        public SymbolicExpression SaveHistory(Language call, SymbolicExpression operation, Pairlist args, REnvironment environment) { return null; }

        public bool ShowFiles(string[] files, string[] headers, string title, bool delete, string pager) { return false; }

        public void ShowMessage(string message) { }

        public void Suicide(string message) { }

        public void WriteConsole(string output, int length, ConsoleOutputType outputType)
        {
            SqlContext.Pipe.Send(output);
        }
    }
}
