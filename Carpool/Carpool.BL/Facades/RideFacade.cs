using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Carpool.BL.Models;
using Carpool.DAL.Entities;
using Carpool.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

//Concrete facade of Ride
namespace Carpool.BL.Facades
{
    public class RideFacade : CRUDFacade<RideEntity, RideListModel, RideDetailModel>
    {
        public RideFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
        {

        }


        public async Task<RideListModel[]> FilterAsync(string? departureLoc = null, string? arrivalLoc = null, 
                                                       DateTime? departureTime = null, DateTime? arrivalTime = null, 
                                                       bool mustBeAvailable = false)
        {
            await using var uow = UnitOfWorkFactory.Create();
            var query = uow
                .GetRepository<RideEntity>()
                .Get()
                .Where(e => 
                    (e.DepartureL == departureLoc || departureLoc == null) &&
                    (e.ArrivalL == arrivalLoc || arrivalLoc == null) &&
                    (e.DepartureT == departureTime || departureTime == null) &&
                    (e.ArrivalT == arrivalTime || arrivalTime == null) &&
                    (e.Capacity > 0 || !mustBeAvailable));

            return await Mapper.ProjectTo<RideListModel>(query)
                .ToArrayAsync().ConfigureAwait(false);
        }


        public async Task<RideListModel[]> GetByDriverIdAsync(Guid driverId)
        {
            await using var uow = UnitOfWorkFactory.Create();
            var query = uow
                .GetRepository<RideEntity>()
                .Get()
                .Where(e => e.DriverId == driverId);

            return await Mapper.ProjectTo<RideListModel>(query)
                .ToArrayAsync().ConfigureAwait(false);
        }


        public async Task<RideListModel[]> GetByParticipantIdAsync(Guid participantId)
        {
            await using var uow = UnitOfWorkFactory.Create();
            var query = uow
                .GetRepository<RideEntity>()
                .Get().Include(i => i.Participants)
                .Where(e => e.Participants
                    .Any(p => p.UserId == participantId));

            return await Mapper.ProjectTo<RideListModel>(query)
                .ToArrayAsync().ConfigureAwait(false);
        }
    }
}
