using System;
using System.Collections.Generic;
using System.Text;

namespace WPFUndoRedo
{
    public static class CommandManager
    {
        public static Dictionary<string, ICommandWithUndoRedo> Commands { get;} = new Dictionary<string, ICommandWithUndoRedo>();

        public static void RegistryCommand(string commandKey, ICommandWithUndoRedo command)
        {
            if (string.IsNullOrEmpty(commandKey))
            {
                throw new ArgumentNullException(nameof(commandKey));
            }

            if (Commands.ContainsKey(commandKey))
            {
                throw new ArgumentException($"Command {commandKey} already registered.");
            }

            Commands[commandKey] = command;
        }
        public static ICommandWithUndoRedo ResolveCommand(string commandKey)
        {
            if (string.IsNullOrEmpty(commandKey))
            {
                throw new ArgumentNullException(nameof(commandKey));
            }

            if (Commands.TryGetValue(commandKey, out ICommandWithUndoRedo command))
            {
                return command;
            }
            else
            {
                throw new KeyNotFoundException($"Command {commandKey} wasn't registered.");
            }

        }
    }
}
