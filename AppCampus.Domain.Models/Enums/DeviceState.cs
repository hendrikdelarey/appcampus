namespace AppCampus.Domain.Models.Enums
{
    public enum DeviceState
    {
        // The device has requested approval, but no action has been taken
        Pending,

        // The device approval request was declined
        Declined,

        // The device approval request was approved
        Approved,

        // The device was blocked
        Blocked
    }
}