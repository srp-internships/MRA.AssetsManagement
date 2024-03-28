using MediatR;
using MRA.AssetsManagement.Domain.Entities;

namespace MRA.AssetsManagement.Application.Features.Documents.Notifications;

public record DocumentCreatedNotification(Document Document) : INotification;