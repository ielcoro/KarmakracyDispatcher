namespace KcyDispatcher.Properties
{
    public interface IKcyDispatcherSettings
    {
        string Pop3UserName { get; }

        string Pop3Password { get; }

        string Pop3Server { get; }

        int Pop3Port { get; }

        bool Pop3UseSSL { get; }

        int Pop3WaitInterval { get; }
        
        string KcyAppKey { get; }

        string KcyUserName { get; }

        string KcyUserKey { get; }

        string KcyShorterUrl { get; }

        string KcySharerUrl { get; }

        string KcyTwitterId { get; }
    }
}