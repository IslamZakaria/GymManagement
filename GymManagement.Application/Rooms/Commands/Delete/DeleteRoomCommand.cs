using ErrorOr;
using MediatR;

namespace GymManagement.Application.Rooms.Commands.Delete
{
    public record DeleteRoomCommand(Guid GymId, Guid RoomId)
        : IRequest<ErrorOr<Deleted>>;
}
