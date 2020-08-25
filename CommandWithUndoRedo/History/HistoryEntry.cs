using System;
using System.Collections.Generic;
using System.Text;

namespace WPFUndoRedo
{
    public class HistoryEntry
    {
        public HistoryEntry(string commandKey, object parameter, object result)
        {
            CommandKey = commandKey;
            Parameter = parameter;
            Result = result;
        }
        public object Parameter { get; set; }
        public object Result { get; set; }
        public string CommandKey { get; }
    }
}
