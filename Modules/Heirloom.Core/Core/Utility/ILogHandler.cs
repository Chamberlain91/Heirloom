namespace Heirloom
{
    public interface ILogHandler
    {
        void Debug(object message);

        void Warning(object message);

        void Error(object message);

        void Info(object message);
    }
}
