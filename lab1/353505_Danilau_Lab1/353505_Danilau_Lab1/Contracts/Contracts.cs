namespace Contracts
{
    public interface IHotelSystem
    {
        void RegisterCustomer(string name);
        bool BookRoom(string customerName, int roomNumber);
        void ShowAvailableRooms();
        decimal CalculateStayCost(string customerName);
        int GetMostPopularRoom();
    }
}