using MediatR;
using TeamPlan.Application.DTOs.Enterprises;
using TeamPlan.Domain.BackOffice.Commum;

namespace TeamPlan.Application.UseCases.Enterprises.Query.Request;

public record DashBoardEnterprise(Guid EnterpriseId,Guid OwnerId) : IRequest<Result<EnterpriseDashBoardDTO>>;