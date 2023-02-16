using System.Xml.Linq;

public class UserInChat
{
    public string Name;
    public ChatRoom Room;
    private List<string> _chatLog = new List<string>();

    public UserInChat(string name) => Name = name;

    public void Receive(string sender, string message)
    {
        string s = $"{sender}: '{message}'";
        Console.WriteLine($"[{Name}'s chat session] {s}");
        _chatLog.Add(s);
    }

    public void Say(string message) => Room.Broadcast(Name, message);
    public void PrivateMessage(string who, string message)
    {
        Room.Message(Name, who, message);
    }
}

public class ChatRoom //Mediator
{
    private List<UserInChat> users = new List<UserInChat>();

    public void Broadcast(string source, string message)
    {
        foreach (var p in users)
            if (p.Name != source)
                p.Receive(source, message);
    }

    public void Join(UserInChat p)
    {
        string joinMsg = $"{p.Name} joins the chat";
        Broadcast("room", joinMsg);
        p.Room = this;
        users.Add(p);

    }

    public void Message(string source, string destination,string message)
    {
        users.FirstOrDefault(p => p.Name == destination).Receive(source, message);
    }

}
namespace Maediator
{
    class Program
    {
        static void Main(string[] args)
        {


            var room = new ChatRoom();
            var adam = new UserInChat("Adam");
            var ewa = new UserInChat("Ewa");

            room.Join(adam);
            room.Join(ewa);

            adam.Say("Cześć, co tam ? Jak leci wam ten poniedziałek?");
            ewa.Say("Kod nie działa tak jak chce");
            var darek = new UserInChat("Darek");
            room.Join(darek);
            darek.Say("Co nowego?");
            ewa.PrivateMessage("Darek", "Cieszę się, że dołączyłeś");

            Console.ReadLine();
        }
    }
}





