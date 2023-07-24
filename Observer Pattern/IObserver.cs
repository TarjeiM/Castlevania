public interface IObserver
{
    // subject uses this method to communicate with the observer(s)
    public void OnNotify();
}
