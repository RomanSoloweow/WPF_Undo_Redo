using System;
using System.Collections.Generic;
using System.Text;

namespace WPFUndoRedo
{
    public static class HistoryManager
    {
        private static Dictionary<string, IHistory> Histories { get; } = new Dictionary<string, IHistory>();
        public static Dictionary<string, HistoryContext> HistoryContexts { get; } = new Dictionary<string, HistoryContext>();
        static HistoryManager()
        {
            RegistryHistory("", new History());
        }
        public static void RegistryHistory(string historyKey, IHistory history)
        {
            if (historyKey==null)
            {
                throw new ArgumentNullException(nameof(historyKey));
            }

            if (Histories.ContainsKey(historyKey))
            {
                throw new ArgumentException($"History {historyKey} already registered.");
            }

            Histories[historyKey] = history;
            HistoryContexts[historyKey] = new HistoryContext(history);
        }
        public static IHistory ResolveHistory(string historyKey)
        {
            if (historyKey == null)
            {
                throw new ArgumentNullException(nameof(historyKey));
            }

            if (Histories.TryGetValue(historyKey, out IHistory history))
            {
                return history;
            }
            else
            {
                throw new KeyNotFoundException($"History {historyKey} wasn't registered.");
            }

        }

        public static HistoryContext GetHistoryContext(string historyKey="")
        {
            if (HistoryContexts.TryGetValue(historyKey, out HistoryContext historyContext))
            {
                return HistoryContexts[historyKey];
            }
            else
            {
                throw new KeyNotFoundException($"History {historyKey} wasn't registered.");
            }
        }
    }
}
