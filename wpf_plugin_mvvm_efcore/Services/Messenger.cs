using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_plugin_mvvm_efcore.Messages;
using wpf_plugin_mvvm_efcore.Tools;

namespace wpf_plugin_mvvm_efcore.Services
{
    public class Messenger : IMessenger
    {
        public static Dictionary<KeyValuePair<Type, ViewModelTokens.Token>, List<Action<object>>> Subscribers { get; private set; }
        public Messenger()
        {
            Subscribers = new();
        }

        public void Subscribe<TMessage>(Action<object> action) where TMessage : IMessage
        {
            Type messageType = typeof(TMessage);
            ViewModelTokens.Token @enum = ViewModelTokens.Token.All;

            KeyValuePair<Type, ViewModelTokens.Token> pair = new(messageType, @enum);


            if (!Subscribers.ContainsKey(pair))
                Subscribers[pair] = new List<Action<object>>();

            Subscribers[pair].Add(action);
        }
       
        public void Subscribe<TMessage>(Action<object> action, ViewModelTokens.Token token) where TMessage : IMessage
        {
            Type messageType = typeof(TMessage);
            ViewModelTokens.Token @enum = token;

            KeyValuePair<Type, ViewModelTokens.Token> pair = new(messageType, @enum);


            if (!Subscribers.ContainsKey(pair))
                Subscribers[pair] = new List<Action<object>>();

            Subscribers[pair].Add(action);
        }

        public void Send<TMessage>(TMessage message) where TMessage : IMessage
        {
            Type messageType = typeof(TMessage);
            ViewModelTokens.Token @enum = ViewModelTokens.Token.All;

            KeyValuePair<Type, ViewModelTokens.Token> pair = new(messageType, @enum);          

            if (Subscribers.ContainsKey(pair))
                foreach (KeyValuePair<KeyValuePair<Type, ViewModelTokens.Token>, List<Action<object>>> item in Subscribers)
                {
                    if (item.Key.Value == pair.Value&&item.Key.Key==pair.Key)
                        foreach (Action<object> action in item.Value)
                        {
                            action.Invoke(message);
                        }

                }

        }
        public void Send<TMessage>(TMessage message, ViewModelTokens.Token token ) where TMessage : IMessage
        {
            Type messageType = typeof(TMessage);
            ViewModelTokens.Token @enum = token;

            KeyValuePair<Type, ViewModelTokens.Token> pair = new(messageType, @enum);


            if (Subscribers.ContainsKey(pair))
                foreach (KeyValuePair<KeyValuePair<Type, ViewModelTokens.Token>, List<Action<object>>> item in Subscribers)
                {
                    if ((item.Key.Value == pair.Value) && (item.Key.Key == pair.Key))
                        foreach (Action<object> action in item.Value)
                        {
                            action.Invoke(message);
                        }

                }
        }
    }
}
