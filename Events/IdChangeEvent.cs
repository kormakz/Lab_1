namespace Events
{
    public sealed class IdChangeEventArgs : EventArgs
    {
        public int OldId { get;}
        public int NewId { get;}
        public IdChangeEventArgs(int oldId, int newId)
        {
            OldId = oldId;
            NewId = newId;
        }
    }
}
