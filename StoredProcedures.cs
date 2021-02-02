using Microsoft.SqlServer.Server;
using RDotNet;
using System.Data;
using System.Data.SqlTypes;

namespace RStoredProc
{
    public class StoredProcedures
    {
        private static object _lock = new object();

        private static REngine _engine;

        private static SqlConsole _console;

        [Microsoft.SqlServer.Server.SqlProcedure]
        public static void RunRScript(SqlString script)
        {
            lock (_lock)
            {
                // initialize R engine
                if (_engine == null)
                {
                    SqlContext.Pipe.Send("Starting R engine");
                    REngine.SetEnvironmentVariables();
                    _console = new SqlConsole();
                    _engine = REngine.GetInstance(null, true, null, _console);
                    SqlContext.Pipe.Send("R engine has been started");
                }

                // Execute R script
                SqlContext.Pipe.Send("Starting R script execution");
                SymbolicExpression res = _engine.Evaluate(script.ToString());

                // Parse and return results if any
                if (res != null && res.IsDataFrame())
                {
                    DataFrame df = res.AsDataFrame();

                    SqlMetaData[] cols = new SqlMetaData[df.ColumnCount];
                    for (int i = 0; i < df.ColumnCount; i++)
                    {
                        cols[i] = new SqlMetaData(df.ColumnNames[i], SqlDbType.Variant);
                    }

                    SqlDataRecord rec = new SqlDataRecord(cols);

                    SqlContext.Pipe.SendResultsStart(rec);

                    var rows = df.GetRows();

                    foreach (DataFrameRow row in rows)
                    {
                        for (int i = 0; i < df.ColumnCount; i++)
                        {
                            rec.SetValue(i, row[i]);
                        }
                        SqlContext.Pipe.SendResultsRow(rec);
                    }

                    SqlContext.Pipe.SendResultsEnd();
                }

                SqlContext.Pipe.Send("R script execution completed");
            }
        }

    }
}
