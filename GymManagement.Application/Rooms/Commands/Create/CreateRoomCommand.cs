using ErrorOr;
using GymManagement.Domain.Rooms;
using MediatR;

namespace GymManagement.Application.Rooms.Commands.Create
{
    public record CreateRoomCommand(
        Guid GymId,
        string RoomName) : IRequest<ErrorOr<Room>>;
}
