using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime.Internal;
using MediatR;

namespace Music.API.Application.Queries.SingleMusic
{
    public record GetSingleMusicByIdQuery(Guid Id) : IRequest<Domain.Aggregates.SingleMusic>;
}